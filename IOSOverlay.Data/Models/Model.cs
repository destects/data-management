using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace IOSOverlay.Data.Models {
	[DataContract]
	public abstract class Model:INotifyPropertyChanged, IKeyedObject<Guid>, IKeyedObject<int>, IEditableObject, INotifyDataErrorInfo, ICloneable, IRevertibleChangeTracking, IComparable, IComparable<Guid>, IComparable<int>, IComparable<Model> {
		// Disable update notifications
		// Useful for initialization because all those notifications are quite wasteful.
		private static bool DisableUpdates = false;
		internal Action DisableUpdatesSync;
		private ModelStates _State = ModelStates.Immutable;
		private Guid _UID = Guid.Empty;
		private int _ID = -1;
		private int _CreatorID = -1;
		private Guid _CreatorUID = Guid.Empty;
		private PackageTag _PackageReference = PackageTag.None;
		private object _Tag = null;
		private object _MasterData;
		private bool _IsChanged = false;
		private event PropertyChangedEventHandler _PropertyChanged;
		private event EventHandler<DataErrorsChangedEventArgs> _ErrorsChanged;

		private ConcurrentDictionary<string, List<string>> _Errors = new ConcurrentDictionary<string, List<string>>();

		#region Invocation
		protected static bool InvokeRequired {
			get {
				if(System.Windows.Threading.Dispatcher.FromThread(Thread.CurrentThread) == null) {
					if(Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA && Thread.CurrentThread.IsBackground && Thread.CurrentThread.IsThreadPoolThread) {
						return false;
					}
					return true;
				}
				return false;
			}
		}

		protected static void InvokeDispatcher(Action action) {
			System.Windows.Application.Current.Dispatcher.Invoke(action);
		}

		protected static async void InvokeDispatcherAsync(Action action) {
			await System.Windows.Application.Current.Dispatcher.InvokeAsync(action);
		}
		#endregion

		/// <summary>
		/// The front facing copy of this models data structure
		/// </summary>
		[IgnoreDataMember]
		protected abstract object _View { get; set; }
		/// <summary>
		/// The fileName this model serializes to and from.
		/// </summary>
		protected internal string _FileName;
		/// <summary>
		/// The parent collection serialize callback
		/// </summary>
		protected internal Action ParentCollectionSerializeCallback;
		protected internal Action SubModelChangedCallback;
		/// <summary>
		/// Indicates if this model is a member of a collection
		/// </summary>
		/// <remarks>
		/// Model does not handle serialization When <see langword="true" />
		/// </remarks>
		[DataMember]
		protected internal bool CollectionMember;
		/// <summary>
		/// Indicates if this collection is a child member of another model.
		/// </summary>
		[DataMember]
		protected internal bool IsSubModel;

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		[IgnoreDataMember, Obsolete("Use GUID Key")]
		int IKeyedObject<int>.Key {
			get {
				return ID;
			}
		}
		Guid IKeyedObject<Guid>.Key {
			get {
				return UID;
			}
		}
		/// <summary>
		/// Gets or sets the master data.
		/// </summary>
		/// <value>
		/// The master data.
		/// </value>
		[IgnoreDataMember]
		public virtual object MasterData {
			get {
				return _MasterData;
			}
			protected set {
				_View = _MasterData = value;
				RaiseAllDataPropertyChanges();
			}
		}
		/// <summary>
		/// Gets an edit token for this model.
		/// </summary>
		/// <value>
		/// The edit token.
		/// </value>
		[IgnoreDataMember]
		internal ModelEditToken EditToken {
			get {
				return new ModelEditToken(this);
			}
		}
		/// <summary>
		/// Occurs when [property changed].
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged {
			add {
				_PropertyChanged += value;
			}
			remove {
				_PropertyChanged -= value;
			}
		}
		/// <summary>
		/// Occurs when [errors changed].
		/// </summary>
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged {
			add {
				_ErrorsChanged += value;
			}
			remove {
				_ErrorsChanged -= value;
			}
		}
		/// <summary>
		/// Gets the mutability of this Model.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if <see cref="State"/> == <seealso cref="ModelStates.Mutable"/> || <seealso cref="ModelStates.New"/>;
		/// otherwise, <see langword="false" />.
		/// </value>
		public virtual bool EditMode {
			get {
				return _State == ModelStates.Mutable || _State == ModelStates.New;
			}
			protected internal set {
				if(_State == ModelStates.ReadOnly || _State == ModelStates.Archived || _State == ModelStates.Hidden) {
					if(!ModelManager.Initializing) {
						return;
					} else {
						System.Diagnostics.Debug.Print("Return ignored because modelmanager initializing");
					}
				}

				_State = value ? ModelStates.Mutable : ModelStates.Immutable;
				RaisePropertyChanged();
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Model{T}"/> is archived.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if archived; otherwise, <see langword="false" />.
		/// </value>
		public virtual bool Archived {
			get {
				return _State == ModelStates.Archived;
			}
			protected internal set {
				if(!EditMode) {
					_State = value ? ModelStates.Archived : ModelStates.Immutable;
					RaisePropertyChanged();
				}
			}
		}
		public virtual bool Hidden {
			get {
				return _State == ModelStates.Hidden;
			}
			protected internal set {
				if(!EditMode) {
					_State = value ? ModelStates.Hidden : ModelStates.Immutable;
					RaisePropertyChanged();
				}
			}
		}
		/// <summary>
		/// Gets or sets the uid.
		/// </summary>
		/// <value>
		/// The uid.
		/// </value>
		[DataMember]
		public Guid UID {
			get {
				return _UID;
			}
			protected internal set {
				_UID = value;
				RaisePropertyChanged();
			}
		}
		/// <summary>
		/// Gets the model identifier.
		/// </summary>
		/// <value>
		/// The model identifier.
		/// </value>
		[DataMember, Obsolete("Use UID")]
		public int ID {
			get {
				return _ID;
			}
			protected internal set {
				_ID = value;
				RaisePropertyChanged();
			}
		}
		/// <summary>
		/// Gets the model state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		[DataMember]
		public virtual ModelStates State {
			get {
				return _State;
			}
			protected internal set {
				_State = value;
				RaisePropertyChanged();
				if(value == ModelStates.New) OnCreate();
			}
		}
		/// <summary>
		/// Gets or sets the creator identifier.
		/// </summary>
		/// <value>
		/// The creator identifier.
		/// </value>
		[DataMember]
		public virtual int CreatorID {
			get {
				return _CreatorID;
			}
			protected internal set {
				_CreatorID = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(CreatorUID));
			}
		}
		/// <summary>
		/// Gets or sets the creator uid.
		/// </summary>
		/// <value>
		/// The creator uid.
		/// </value>
		[DataMember]
		public virtual Guid CreatorUID {
			get {
				return _CreatorUID;
			}
			protected internal set {
				_CreatorUID = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(CreatorID));
			}
		}
		[DataMember]
		public virtual PackageTag PackageReference {
			get {
				return _PackageReference;

			}
			protected internal set {
				_PackageReference = value;
				RaisePropertyChanged();
			}
		}
		/// <summary>
		/// Gets a value indicating whether this instance has errors.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if this instance has errors; otherwise, <see langword="false" />.
		/// </value>
		public bool HasErrors {
			get {
				return !_Errors.IsEmpty; //_Errors.Count > 0;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is changed.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if this instance is changed; otherwise, <see langword="false" />.
		/// </value>
		public bool IsChanged {
			get {
				return _IsChanged;
			}
			protected internal set {
				_IsChanged = value;
				RaisePropertyChanged();
				if(_IsChanged && IsSubModel && SubModelChangedCallback != null) {
					SubModelChangedCallback();
				}
			}
		}
		/// <summary>
		/// Gets a value indicating whether this instance is changed.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if this instance is changed; otherwise, <see langword="false" />.
		/// </value>
		bool IChangeTracking.IsChanged {
			get {
				return IsChanged;
			}
		}
		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		[IgnoreDataMember]
		public object Tag {
			get {
				return _Tag;
			}
			set {
				_Tag = value;
				RaisePropertyChanged();
			}
		}
		/// <summary>
		/// Gets the grouping tag for this model.
		/// </summary>
		/// <value>
		/// The grouping.
		/// </value>
		[IgnoreDataMember]
		public virtual ModelGroupingOptions Grouping => AccountManager.IsOwner(this) ? ModelGroupingOptions.Mine : ModelGroupingOptions.Other;

		/// <summary>
		/// Initializes a new instance of the <see cref="Model"/> class.
		/// </summary>
		protected Model() {
			MasterData = new object();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Model"/> class.
		/// </summary>
		/// <param name="master">The master.</param>
		protected Model(object master) {
			MasterData = master;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Model{T}"/> class.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		protected Model(string fileName) {
			_FileName = fileName;
		}

		/// <summary>
		/// Gets the errors for the specified property
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		public IEnumerable GetErrors(string propertyName) {
			var errors = new List<string>();
			_Errors.TryGetValue(propertyName ?? string.Empty, out errors);
			return errors;
		}
		/// <summary>
		/// Enter an edit session, overrides the <see cref="_View"/> with the <see cref="Master"/> to ensure the view is up
		/// to date.
		/// </summary>
		public virtual void BeginEdit() {
			if(State == ModelStates.ReadOnly) return;
			_View = MasterData;
			EditMode = true;
			IsChanged = false;
			RaiseAllDataPropertyChanges();
		}
		/// <summary>
		/// Ends the edit session and overrides <see cref="_View"/> with the <see cref="Master"/>, canceling out all changes.
		/// </summary>
		public virtual void CancelEdit() {
			if(State == ModelStates.ReadOnly) return;
			_View = MasterData;
			EditMode = false;
			IsChanged = false;
			_Tag = null;
			RaiseAllDataPropertyChanges();
		}
		/// <summary>
		/// Ends the edit session and overrides the <see cref="Master"/> with the <see cref="_View"/>, recording all changes.
		/// </summary>
		public virtual void EndEdit() {
			if(State == ModelStates.ReadOnly) return;
			MasterData = _View;
			EditMode = false;
			IsChanged = false;
			_Tag = null;
			Save();
		}

		/// <summary>
		/// Serialize this model into it's persistence file.
		/// <para>
		/// This function is exposed for Model management purposes. 
		/// </para>
		/// </summary>
		internal void Save() {
#if DEBUG_DISABLE_SAVING
			Debug.WriteLine("Information: Saving to disk skipped by DEBUG_DISABLE_SAVING");
			return;
#endif
			Serialize(_FileName);
		}
		public virtual void Generalize() {
			using(EditToken) {
				CreatorID = -1;
				CreatorUID = Guid.Empty;
			}
		}
		public void Save(string fileName) {
#if DEBUG_DISABLE_SAVING
			Debug.WriteLine("Information: Saving to disk skipped by DEBUG_DISABLE_SAVING");
			return;
#endif
			Serialize(fileName);
		}
		public void Save(Stream stream) {
#if DEBUG_DISABLE_SAVING
			Debug.WriteLine("Information: Saving to disk skipped by DEBUG_DISABLE_SAVING");
			return;
#endif
			Serialize(stream);
		}
		/// <summary>
		/// Loads the new data.
		/// </summary>
		/// <param name="data">The data.</param>
		protected void LoadNewData(object data) {
			if(EditMode) {
				_View = data;
				_IsChanged = true;
				RaiseAllDataPropertyChanges();
			}
		}
		/// <summary>
		/// Serializes this object and saves it under the specified file name.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		protected virtual void Serialize(string fileName) {
			System.Diagnostics.Debug.Assert(State != ModelStates.ReadOnly, "ERROR: Model is readonly.");
			if(State != ModelStates.ReadOnly) {
				// Members of a collection are serialized by the collection.
				if(!CollectionMember && !IsSubModel) {
					SerializeInternal(fileName);
				} else {
					if(ParentCollectionSerializeCallback != null) {
						ParentCollectionSerializeCallback.Invoke();
					}
					System.Diagnostics.Debug.Assert(ParentCollectionSerializeCallback != null || IsSubModel, "ERROR: Model is not a submodel but collection serializer was null.");
				}
			}
		}
		/// <summary>
		/// Serializes this object and writes it to the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		protected virtual void Serialize(Stream stream) {
			System.Diagnostics.Debug.Assert(State != ModelStates.ReadOnly, "ERROR: Model is readonly.");
			if(State != ModelStates.ReadOnly) {
				// Members of a collection are serialized by the collection.
				if(!CollectionMember && !IsSubModel) {
					SerializeInternal(stream);
				} else {
					if(ParentCollectionSerializeCallback != null) {
						ParentCollectionSerializeCallback.Invoke();
					}
					System.Diagnostics.Debug.Assert(ParentCollectionSerializeCallback != null || IsSubModel, "ERROR: Model is not a submodel but collection serializer was null.");
				}
			}
		}
		/// <summary>
		/// Serializes this object and saves it under the specified file name.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		protected internal virtual void SerializeInternal(string fileName) {
			System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(fileName), "fileName Must be a valid fileName [Serialize Model: " + this.GetType().Name + "]");
			if(!string.IsNullOrWhiteSpace(fileName)) {
#if DEBUG
#if PLAIN_TEXT
				bool pt = true;
#else
				bool pt = false;
#endif
#if DISABLE_ENCODING
				bool ec = false;
#else
				bool ec = true;
#endif
				Extensions.DataContractSerialization.SerializeContractToFile(this.GetType(), this, fileName, plainText: pt, encode: ec);
#else
				Extensions.DataContractSerialization.SerializeContractToFile(this.GetType(), this, fileName, plainText: false, encode: true);
#endif
			}
		}
		/// <summary>
		/// Serializes this object and saves it under the specified file name.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		protected internal virtual void SerializeInternal(Stream stream) {
			System.Diagnostics.Debug.Assert(stream != null, "Null stream");
			System.Diagnostics.Debug.Assert(stream.CanWrite, "stream is not writable");

			if(stream != null && stream.CanWrite) {
#if DEBUG
#if PLAIN_TEXT
				bool pt = true;
#else
				bool pt = false;
#endif
#if DISABLE_ENCODING
				bool ec = false;
#else
				bool ec = true;
#endif
				//Extensions.DataContractSerialization.SerializeContractToFile(this.GetType(), this, fileName, plainText: pt, encode: ec);
				var buffer = Extensions.DataContractSerialization.SerializeContract(this.GetType(), this, plainText: pt, encode: ec);
				stream.Write(buffer, 0, buffer.Length);
#else
				//Extensions.DataContractSerialization.SerializeContractToFile(this.GetType(), this, fileName, plainText: false, encode: true);
				var buffer = Extensions.DataContractSerialization.SerializeContract(this.GetType(), this, plainText: false, encode: true);
				stream.Write(buffer, 0, buffer.Length);
#endif
			}
		}
		/// <summary>
		/// Serializes the export.
		/// </summary>
		/// <returns></returns>
		protected internal virtual byte[] SerializeExport() {
			return Extensions.DataContractSerialization.SerializeContract(this.GetType(), this, true);
		}
		/// <summary>
		/// When overrode in a derived class, called when a new Model is created.
		/// </summary>
		protected internal virtual void OnCreate() {
			//CreatorID = AccountManager.ActiveID;
			CreatorUID = AccountManager.ActiveUID;
		}
		/// <summary>
		/// Determines if this instance can be removed from a parent collection.
		/// </summary>
		/// <returns></returns>
		protected internal virtual bool Removable() {
			return true;
		}
		/// <summary>
		/// Called by collection when model is about to be deleted.
		/// </summary>
		protected internal virtual void OnDelete() {
		}

		[OnSerializing]
		private void SerializationCleanup(StreamingContext context) {
			if(EditMode)
				CancelEdit(); // -- Revert any lingering changes that result in a mutable model out of scope.
								  // update missing UID's
			if(UID == Guid.Empty) {
				UID = Guid.NewGuid();
			}
		}

		protected async virtual void ValidateModel() {
			await Task.Run(() => {
				this.GetType().GetFields().ToList().ForEach(p => Validate(p.Name));
			});
		}
		/// <summary>
		/// Validates the specified property
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected async void Validate([CallerMemberName]string propertyName = "") {
			if(string.IsNullOrWhiteSpace(propertyName)) return;

			await Task.Run(() => {
				//Console.WriteLine("Warning: " + propertyName);
				List<string> currentErrors;
				_Errors.TryRemove(propertyName, out currentErrors);

				var results = ValidateProperty(propertyName);
				if(results != null && results.Count > 0) {
					_Errors.AddOrUpdate(propertyName, results, (key, currentValue) => results);
				}
				//if((results == null ^ currentErrors == null) || ((results != null && currentErrors != null) && !currentErrors.SequenceEqual(results)))
				RaiseErrorsChanged(propertyName);
			});
		}
		/// <summary>
		/// Validates the property.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>List of errors for the specified property, or null if none</returns>
		protected virtual List<string> ValidateProperty(string propertyName) {
			return null;
		}

		/// <summary>
		/// Raises the errors changed event.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected void RaiseErrorsChanged([CallerMemberName] string propertyName = "") {
			//if(DisableUpdates) return;
			if(_ErrorsChanged != null) {
				//if(InvokeRequired) {
				//InvokeDispatcher(() => {
				// have to do the error check again because we can't guarantee _ErrorsChanged since the
				// Invocation can happen out of sync.
				if(_ErrorsChanged != null && !string.IsNullOrWhiteSpace(propertyName)) _ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
				//});
				//} else {
				//	_ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
				//}
			}
		}
		/// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "") {
			if(DisableUpdates) return;
			if(_PropertyChanged != null) {
				if(InvokeRequired) {
					InvokeDispatcher(() => {
						// have to do the error check again because we can't guarantee _PropertyChanged since the
						// Invocation can happen out of sync.
						if(_PropertyChanged != null) _PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
					});
				} else {
					_PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				}

				if(!string.IsNullOrWhiteSpace(propertyName)) {
					RaiseLinkedPropertyNotifications(propertyName);
					Validate(propertyName);
				}
			}
		}
		/// <summary>
		/// Raises all data property changes.
		/// </summary>
		protected void RaiseAllDataPropertyChanges() {
			if(DisableUpdates) return;
			//RaisePropertyChanged("_View");
			// Using reflection to get the names of all public fields and raise their property changed events.
			//typeof(T).GetFields().ToList().ForEach(p => RaisePropertyChanged(p.Name));
			//RaisePropertyChanged(null); // <-- according to documentation, this should do the same as above.
			AsyncRaiseAllDataPropertyChanges();
			//ValidateModel();
		}
		private async void AsyncRaiseAllDataPropertyChanges() {
			if(DisableUpdates) return;
			await Task.Run(() => {
				var x = this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
				//Parallel.ForEach(this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public), (a) => {
				//	RaisePropertyChanged(a.Name);
				//});
				foreach(var a in x) {
					RaisePropertyChanged(a.Name);
				}
			});

		}
		/// <summary>
		/// Raises the linked property notifications.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		private void RaiseLinkedPropertyNotifications(string propertyName) {
			if(_PropertyChanged != null) {
				foreach(var prop in PropertyNotificationLinker.GetLinkedProperties(this.GetType(), propertyName)) {
					if(InvokeRequired) {
						InvokeDispatcher(() => {
							// have to do the error check again because we can't guarantee _PropertyChanged since the
							// Invocation can happen out of sync.
							if(_PropertyChanged != null) _PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
						});
					} else {
						_PropertyChanged(this, new PropertyChangedEventArgs(prop));
					}
				}
			}
		}
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public abstract object Clone();
		/// <summary>
		/// Compares to.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public abstract int CompareTo(Model other);

		/// <summary>
		/// Compares to.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns></returns>
		public int CompareTo(object obj) {
			return UID.CompareTo(obj);
		}
		/// <summary>
		/// Compares to.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		[Obsolete]
		public int CompareTo(int other) {
			return ID.CompareTo(other);
		}
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
		/// </returns>
		public int CompareTo(Guid other) {
			return UID.CompareTo(other);
		}

		/// <summary>
		/// Rejects the changes.
		/// </summary>
		void IRevertibleChangeTracking.RejectChanges() {
			CancelEdit();
		}
		/// <summary>
		/// Accepts the changes.
		/// </summary>
		void IChangeTracking.AcceptChanges() {
			EndEdit();
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode() {
			return UID.GetHashCode();
		}
		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString() {
			return "Model UID: " + UID + ", Data: " + MasterData.ToString();
		}
		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			if(obj is Model) {
				return (obj as Model).UID == this.UID;
			}
			return base.Equals(obj);
		}

		public static void BeginInit() {
			DisableUpdates = true;
		}
		public static void EndInit() {
			DisableUpdates = false;
		}
	}
}
