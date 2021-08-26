using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using System.Diagnostics;

namespace IOSOverlay.Data.Models {
	//IRaiseItemChangedEvents
	//ISurrogateSelector
	[DataContract]
	public abstract class ModelCollection:INotifyPropertyChanged {
		/// <summary>
		/// Provides a set of <see cref="Predicate{T}"/>s for some basic standard filtering tasks.
		/// </summary>
		public static class ModelFilters {
			/// <summary>
			/// Gets a <see cref="Predicate{T}"/> that filters out models that are hidden or archived
			/// </summary>
			/// <value>
			/// The no archived.
			/// </value>
			public static Predicate<object> NO_ARCHIVED => (a) => (AccountManager.ActiveUserIsMaster || !(a as Model).Hidden) && (AccountManager.ActiveUserIsAdmin || !(a as Model).Archived);
			/// <summary>
			/// Gets a <see cref="Predicate{T}"/> that filters inclusively models grouped as built-in or template
			/// </summary>
			/// <value>
			/// The builtin or template groups.
			/// </value>
			public static Predicate<object> BUILTIN_OR_TEMPLATE_GROUPS => (a) => (a as Model).Grouping == ModelGroupingOptions.BuiltIn || (a as Model).Grouping == ModelGroupingOptions.Template;
			/// <summary>
			/// Gets a <see cref="Predicate{T}"/> that filters models owned by the active user
			/// </summary>
			/// <value>
			/// The active owned.
			/// </value>
			public static Predicate<object> ACTIVE_OWNED => (a) => NO_ARCHIVED(a) && AccountManager.IsOwner(a as Model);
			/// <summary>
			/// Gets a <see cref="Predicate{T}"/> that filters models owned by the active user or built-in
			/// </summary>
			/// <value>
			/// The active owned and defaults.
			/// </value>
			public static Predicate<object> ACTIVE_OWNED_AND_DEFAULTS => (a) => ACTIVE_OWNED(a) || BUILTIN_OR_TEMPLATE_GROUPS(a);
		}

		private Guid _CollectionUID;
		[DataMember]
		internal int CollectionUpdateVersion = 0;
		private static readonly Predicate<object> _ArchiveFilter = new Predicate<object>((a) => !(a as Model).Archived);
		protected ActiveSelectionChangedEventHandler _ActiveSelectionChanged;
		protected static bool DisableUpdates = false;

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		protected volatile object _Lock = new object();
		protected string _FileName;
		[DataMember]
		protected int _NextIndex;
		protected Guid _ActiveUID = Guid.Empty;
		protected CollectionViewSource _CollectionView = new CollectionViewSource();
		protected Model _ActiveSelection = null;

		/// <summary>
		/// Gets or sets the collection universal identifier.
		/// </summary>
		/// <value>
		/// The collection universal identifier.
		/// </value>
		[DataMember]
		public Guid CollectionUID {
			get { return _CollectionUID; }
			protected set { _CollectionUID = value; }
		}
		/// <summary>
		/// Gets the collection view.
		/// </summary>
		/// <value>
		/// The collection view.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public abstract ListCollectionView CollectionView {
			get;
		}
		/// <summary>
		/// Gets or sets the active uid.
		/// </summary>
		/// <value>
		/// The active uid.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public virtual Guid ActiveUID {
			get {
				return _ActiveSelection?.UID ?? Guid.Empty;
			}
			set {

			}
		}
		/// <summary>
		/// Gets or sets the active selection.
		/// </summary>
		/// <value>
		/// The active selection.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public virtual Model ActiveSelection {
			get {
				return _ActiveSelection;
			}
			set {

			}
		}
		/// <summary>
		/// Gets the index value.
		/// </summary>
		/// <value>
		/// The index value.
		/// </value>
		[IgnoreDataMember]
		internal int IndexValue {
			get { return _NextIndex; }
		}
		/// <summary>
		/// Gets a value indicating whether the <see cref="ActiveSelection"/> property as well as the <see cref="ActiveUID"/>
		/// Are mutable.
		/// <para>
		/// When <see langword="true" />, <see cref="ActiveSelection"/> and <see cref="ActiveUID"/> properties are non-mutable,
		/// and the exposed <typeparam name="TModel"><see cref="ActiveSelection"/></typeparam> is marked as mutable.
		/// </para>
		/// </summary>
		/// <value>
		/// <see langword="true" /> if <see cref="ActiveSelection"/> is mutable; otherwise, <see langword="false" />.
		/// </value>
		/// <remarks>
		/// The <see cref="ActiveSelection"/> and <see cref="ActiveUID"/> properties become immutable to prevent version collisions
		/// and loss of changes, or unexpected behavior, resulting from modifications to the <see cref="ActiveSelection"/>'s exposed
		/// <typeparamref name="TModel"/>.
		/// </remarks>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public virtual bool ActiveSelectionLocked {
			get {
				return !ActiveSelectionNull && ActiveUID == _ActiveSelection.UID && _ActiveSelection.EditMode;
			}
		}
		/// <summary>
		/// Gets a value indicating whether [active selection null].
		/// </summary>
		/// <value>
		/// <see langword="true" /> if [active selection null]; otherwise, <see langword="false" />.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public virtual bool ActiveSelectionNull {
			get {
				return _ActiveSelection == null;
			}
		}
		/// <summary>
		/// Gets the ID of the first <typeparam name="TModel"/> in this collection or -1 if collection has no members.
		/// </summary>
		/// <value>
		/// The ID of the first member of this collection.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true), Obsolete("Use FirstUID")]
		public abstract int FirstID {
			get;
		}
		/// <summary>
		/// Gets the first uid.
		/// </summary>
		/// <value>
		/// The first uid.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public abstract Guid FirstUID {
			get;
		}
		/// <summary>
		/// Occurs when [active selection changed].
		/// </summary>
		public event ActiveSelectionChangedEventHandler ActiveSelectionChanged {
			add {
				lock(_Lock) {
					_ActiveSelectionChanged += value;
				}
			}
			remove {
				lock(_Lock) {
					_ActiveSelectionChanged -= value;
				}
			}
		}

		/// <summary>
		/// Adds a new <typeparamref name="TModel"/> to this collection, sets it as the <see cref="ActiveSelection"/>,
		/// and puts it in edit mode.
		/// </summary>
		public abstract void AddNew();
		/// <summary>
		/// Creates a copy of the <see cref="ActiveSelection"/>, adds the copy to the collection,
		/// and sets that copy as the <see cref="ActiveSelection"/>.
		/// <para>
		/// If the <see cref="ActiveSelection"/> at the time calling is currently mutable,
		/// <see cref="CancelEditActiveSelection()"/> will be called.
		/// </para>
		/// <para>
		/// The new copy will be marked as mutable.
		/// </para>
		/// </summary>
		public abstract void DuplicateCurrent();
		/// <summary>
		/// Removes the Model with id <paramref name="id" /></summary>
		/// <param name="id">The ID of the model to be removed.</param>
		/// <param name="archive">
		/// if set to <see langword="true" /> marks the model as archived instead of removing it. 
		/// <para>Note: This does not apply to models in the New state.</para>
		/// </param>
		[Obsolete("Use RemoveUID", true)]
		public virtual void RemoveID(int id, bool archive = false) {
			// Cancel edit if active selection is locked
			if(!ActiveSelectionLocked) {
				CancelEditActiveSelection();
			}
			// In cases where the active selection was removed by the cancel edit, the ID will no longer be valid.
			if(ContainsID(id)) {
				RemoveActiveSelection(archive);
			}
		}
		/// <summary>
		/// Removes the uid.
		/// </summary>
		/// <param name="uid">The uid.</param>
		/// <param name="archive">if set to <c>true</c> [archive].</param>
		public virtual void RemoveUID(Guid uid, bool archive = false) {
			// Cancel edit if active selection is locked
			if(!ActiveSelectionLocked) {
				CancelEditActiveSelection();
			}
			// In cases where the active selection was removed by the cancel edit, the UID will no longer be valid.
			if(ContainsUID(uid)) {
				// set active selection before we try to remove anything
				ActiveUID = uid;
				RemoveActiveSelection(archive);
			}
		}
		/// <summary>
		/// Forces the deletion of the model with the specified GUID.
		/// </summary>
		/// <param name="uid">The uid.</param>
		public virtual void ForceDeleteGuid(Guid uid) {
			// Cancel edit if active selection is locked
			if(!ActiveSelectionLocked) {
				CancelEditActiveSelection();
			}
			// In cases where the active selection was removed by the cancel edit, the ID will no longer be valid.
			if(ContainsUID(uid)) {
				// set the active UID before we try to delete
				ActiveUID = uid;
				ForceDeleteActiveSelection();
			}
		}
		/// <summary>
		/// Removes the <see cref="ActiveSelection" /> from this collection.
		/// </summary>
		/// <param name="archive">
		/// if set to <see langword="true" /> marks the model as archived instead of removing it. 
		/// <para>Note: This does not apply to models in the New state.</para>
		/// </param>
		public abstract void RemoveActiveSelection(bool archive = false);
		public abstract void ForceDeleteActiveSelection();
		/// <summary>
		/// Puts the <see cref="ActiveSelection"/> into edit mode, locking the <see cref="ActiveSelection"/>.
		/// </summary>
		public abstract void BeginEditActiveSelection();
		/// <summary>
		/// Cancels all changes to the <see cref="ActiveSelection"/> and unlocks it.
		/// </summary>
		public abstract void CancelEditActiveSelection();
		/// <summary>
		/// Ends the edit session for the <see cref="ActiveSelection"/> and unlocks it.
		/// </summary>
		public abstract void EndEditActiveSelection();

		/// <summary>
		/// When overrode in a derived class, clears the <see cref="CollectionView"/>'s filtering and sorting properties
		/// and resets the see <see cref="ActiveSelection"/> and <see cref="ActiveUID"/>.
		/// </summary>
		public virtual void ResetViewState() {
			CollectionView.Filter = null;
			CollectionView.CustomSort = null;
			CollectionView.SortDescriptions.Clear();
			CollectionView.GroupDescriptions.Clear();
		}
		/// <summary>
		/// Determines whether the specified ID exists in this collection.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public abstract bool ContainsID(int id);
		/// <summary>
		/// Determines whether the specified uid contains uid.
		/// </summary>
		/// <param name="uid">The uid.</param>
		/// <returns>
		///   <c>true</c> if the specified uid contains uid; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool ContainsUID(Guid uid);

		/// <summary>
		/// Gets a private collectionView that is not influenced by filter/grouping changes made to the default 
		/// CollectionView for this collection.
		/// </summary>
		/// <returns>a new ListCollectionView for this collection.</returns>
		public virtual ListCollectionView GetPrivateView() {
			var x = new CollectionViewSource();
			if(_CollectionView != null || CollectionView != null) x.Source = _CollectionView.Source;

			return (ListCollectionView)x.View;
		}
		/// <summary>
		/// Gets the next available index ID
		/// </summary>
		/// <returns></returns>
		protected int GetNextIndex() {
			return _NextIndex++;
		}
		protected void SetModelGuid(Model m) {
			if(m.UID == Guid.Empty) {
				m.UID = Guid.NewGuid();
			}
		}
		public abstract IEnumerator<Model> GetEnumerator();

		/// <summary>
		/// Handles the PropertyChanged event of the <see cref="ActiveSelection"/> control.
		/// <para>
		/// This function is used to forward/bubble-up change notification from the <see cref="ActiveSelection"/>, as well as
		/// provide change notifications for this collections <see cref="ActiveSelectionLocked"/> state.
		/// </para>
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected virtual void ActiveSelection_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			if(DisableUpdates) return;
			if(e.PropertyName == "EditMode"/* || e.PropertyName == "State"*/) {
				RaisePropertyChanged("ActiveSelectionLocked");
			}

			RaisePropertyChanged(e.PropertyName, sender);
			// keeping this here till Monday (sep 14, 2015) because otherwise shit will be bork'd
			//RaisePropertyChanged(e.PropertyName);

		}
		/// <summary>
		/// Raises the active selection changed event.
		/// </summary>
		protected virtual void RaiseActiveSelectionChangedEvent() {
			_ActiveSelectionChanged?.Invoke(this, ActiveSelectionChangedEventArgs.Empty);
		}
		/// <summary>
		/// Members the serialize callback.
		/// </summary>
		protected virtual void MemberSerializeCallback() {
			Save();
		}
		/// <summary>
		/// Refreshes the serialization callbacks.
		/// </summary>
		protected abstract void RefreshSerializationCallbacks();
		/// <summary>
		/// Serializes this collection to its persistence file.
		/// </summary>
		internal virtual void Save() {
#if DEBUG_DISABLE_SAVING
			Debug.WriteLine("Information: Saving to disk skipped by DEBUG_DISABLE_SAVING");
			return;
#endif
			Serialize(_FileName);
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
		/// Performs DataContract serialization on this collection, saving it to it's persistence file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		protected abstract void Serialize(string fileName);
		protected abstract void Serialize(Stream stream);
		protected internal abstract void ExportCollection(string fileName);
		[Obsolete]
		protected internal abstract void ExportSubCollection(string fileName, IEnumerable<int> ids);
		[Obsolete]
		protected internal abstract void ImportCollection(string fileName, bool overwrite);
		//protected abstract void ImportSubCollection(string fileName);

		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "", object sender = null) {
			if(DisableUpdates) return;
			if(PropertyChanged != null) {
				if(InvokeRequired) {
					InvokeDispatcher(() => {
						if(PropertyChanged != null) PropertyChanged.Invoke(sender ?? (this), new PropertyChangedEventArgs(propertyName));
					});
				} else {
					PropertyChanged(sender ?? (this), new PropertyChangedEventArgs(propertyName));
				}
			}
		}
		public static void BeginInit() {
			DisableUpdates = true;
		}
		public static void EndInit() {
			DisableUpdates = false;
		}
		[Obsolete("Use TryUpdateCollectionState instead as this doesn't update UIDs")]
		internal void UpdateCollectionState(int nextIndexBase, int collectionVersion) {
			if(collectionVersion >= CollectionUpdateVersion) {
				CollectionUpdateVersion = collectionVersion;
				if(_NextIndex < nextIndexBase) {
					_NextIndex = nextIndexBase;
				}
			}
		}
		/// <summary>
		/// Attempts to update this colleciton with data from the <paramref name="candidateUpdate"/>.
		/// </summary>
		/// <param name="candidateUpdate">The candidate update.</param>
		/// <returns>True if the collection was able to be updated. False if the current version is up to date or otherwise.</returns>
		internal bool TryUpdateCollectionState(ModelCollection candidateUpdate) {
			if(candidateUpdate.CollectionUpdateVersion > CollectionUpdateVersion) {
				if(_NextIndex < candidateUpdate._NextIndex) {
					_NextIndex = candidateUpdate._NextIndex;
				}
				CollectionUID = candidateUpdate.CollectionUID;
				CollectionUpdateVersion = candidateUpdate.CollectionUpdateVersion;
				return true;
			}
			return false;
		}
		internal abstract void UpdateModelUIDs();
		internal abstract void UpdateCreatorUIDs();
		internal abstract void UpdateAddModel(Model model);
		internal abstract void UpdateAddModel(Model model, PackageTag packTag);
		internal abstract IEnumerable<Guid> EnumeratePackageMatches(PackageTag packTag);
		internal abstract void RemoveModelsByPackageTag(PackageTag packTag);
		protected internal virtual void ProcessOnLoadCollection() { }
		#region Invocation
		protected static bool InvokeRequired {
			get {
				return System.Windows.Threading.Dispatcher.FromThread(System.Threading.Thread.CurrentThread) == null;
			}
		}

		protected static void InvokeDispatcher(Action action) {
			try {
				System.Windows.Application.Current.Dispatcher.Invoke(action);
			} catch(TaskCanceledException) {
				if(!System.Threading.Thread.CurrentThread.ThreadState.HasFlag(System.Threading.ThreadState.AbortRequested)) {
#if DEBUG
					throw;
#endif
				}
			}
		}

		protected static async void InvokeDispatcherAsync(Action action) {
			await System.Windows.Application.Current.Dispatcher.InvokeAsync(action);
		}
		#endregion
	}

	/// <summary>
	/// Functionality and wrapping for a collection of <typeparamref name="TModel"/>'s to provide persistence, state management,
	/// observability, indexing, data versioning, and selection persistence.
	/// <para>
	/// This class is marked as abstract and should be used as such, an implementing class should at the minimum provide
	/// <typeparamref name="TModel"/> and <typeparamref name="T"/>, where T is the type of structure implemented by TModel.
	/// It should also implement a single static method for factory creation of ModelCollections via
	/// the exposed ModelCollection deserialization method.
	/// </para>
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	/// <typeparam name="T">The type of data structure implemented by <typeparamref name="TModel"/>.</typeparam>
	[DataContract]
	public class ModelCollection<TModel, T>:ModelCollection
		where T : struct
		where TModel : Model<T>, new() {

		[DataMember]
		private ObservableKeyedDictionary<int, TModel> _Collection = new ObservableKeyedDictionary<int, TModel>();

		/// <summary>
		/// Gets the collection view.
		/// </summary>
		/// <value>
		/// The collection view.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public override ListCollectionView CollectionView {
			get {
				if(_CollectionView.Source == null) _CollectionView.Source = _Collection.ObservableValues;
				return (ListCollectionView)_CollectionView.View;
			}
		}
		/// <summary>
		/// Gets the <see cref="TModel" /> with the specified identifier.
		/// <para>Will return Null if not found.</para>
		/// </summary>
		/// <value>
		/// The <see cref="TModel" />.
		/// </value>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[IgnoreDataMember, Obsolete("Use Guid/UID lookup")]
		public TModel this[int id] {
			get {
				if(_Collection.ContainsKey(id)) {
					return (TModel)_Collection[id];
				}
				return null;
			}
		}
		public TModel this[Guid uid] {
			get {
				return GetByUID(uid);
			}
		}
		/// <summary>
		/// Gets the models.
		/// </summary>
		/// <value>
		/// The models.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public ReadOnlyCollection<TModel> Models {
			get {
				return new ReadOnlyCollection<TModel>(_Collection.Values.ToList());
			}
		}
		public override Guid ActiveUID {
			get {
				return base.ActiveUID;
			}
			set {
				//var current = _ActiveUID;
				//var val = value;
				if(!ActiveSelectionLocked) {
					if(_Collection != null && ContainsUID(value)) {
						_ActiveUID = value;
						if(_ActiveSelection == null || _ActiveSelection.UID != _ActiveUID) {
							ActiveSelection = this[_ActiveUID];
						} else {
							RaisePropertyChanged(nameof(ActiveSelectionLocked));
							RaisePropertyChanged(nameof(ActiveSelectionNull));
						}
						RaisePropertyChanged();
					}
				}
			}
		}
		/// <summary>
		/// Gets or sets the active selection.
		/// </summary>
		/// <value>
		/// The active selection.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public new virtual TModel ActiveSelection {
			get {
				return (TModel)_ActiveSelection;
			}
			set {
				if(!ActiveSelectionLocked) {
					if(_ActiveSelection != null) _ActiveSelection.PropertyChanged -= ActiveSelection_PropertyChanged;

					_ActiveSelection = value;

					if(_ActiveSelection != null) {
						if(_ActiveUID != _ActiveSelection.UID) {
							ActiveUID = _ActiveSelection.UID;
						}
						_ActiveSelection.PropertyChanged += ActiveSelection_PropertyChanged;
					}
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(ActiveSelectionLocked));
					RaisePropertyChanged(nameof(ActiveSelectionNull));
					RaiseActiveSelectionChangedEvent();
				}
			}
		}
		/// <summary>
		/// Gets the first <typeparam name="TModel"/> in this collection or null if collection has no members.
		/// </summary>
		/// <value>
		/// The first member of this collection.
		/// </value>
		[IgnoreDataMember, NotifyParentProperty(true)]
		public virtual TModel First {
			get {
				if(_Collection.Count > 0) return _Collection.First().Value;
				return null;
			}
		}
		/// <summary>
		/// Gets the first identifier.
		/// </summary>
		/// <value>
		/// The first identifier.
		/// </value>
		[Obsolete("Use FirstUID", false)]
		public override int FirstID {
			get {
				return IndexOf(FirstUID);
			}
		}
		/// <summary>
		/// Gets the first uid.
		/// </summary>
		/// <value>
		/// The first uid.
		/// </value>
		public override Guid FirstUID {
			get {
				return First?.UID ?? Guid.Empty;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelCollection{TModel, T}"/> class.
		/// </summary>
		public ModelCollection() {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ModelCollection{TModel, T}"/> class.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public ModelCollection(string fileName) : base() {
		}

		/// <summary>
		/// Adds a new <typeparamref name="TModel"/> to this collection, sets it as the <see cref="ActiveSelection"/>,
		/// and puts it in edit mode.
		/// </summary>
		public override void AddNew() {
			if(!ActiveSelectionLocked) {
				AddNew(default(T));
			}
		}
		/// <summary>
		/// Adds a new <typeparamref name="TModel"/> with <paramref name="modelData"/> to this collection, sets it as
		/// the <see cref="ActiveSelection"/>, and puts it in edit mode.
		/// </summary>
		/// <param name="modelData">The model data for the new <typeparamref name="TModel"/>.</param>
		public virtual void AddNew(T modelData) {
			if(!ActiveSelectionLocked) {
				AddNew(new TModel() { Master = modelData });
			}
		}
		/// <summary>
		/// Adds the <paramref name="model"/> to this collection, marks it as a collection member with a new ID, sets it
		/// as the <see cref="ActiveSelection"/>, and puts it in edit mode.
		/// </summary>
		/// <param name="model">The model.</param>
		public virtual void AddNew(TModel model) {
			if(!ActiveSelectionLocked) {
				if(model != null) {
					if(model.ID == -1 || _Collection.Keys.Contains(model.ID)) {
						SetModelGuid(model);
						var lp = 0;
						var nid = GetNextIndex();
						while(_Collection.Keys.Contains(nid) && lp < 150) {
							lp++;
							nid = GetNextIndex();
							//Debug.Assert(lp > 100, "LONG RUNNING OPERATION");
						}
						if(_Collection.Keys.Contains(nid)) {
							nid = _Collection.Keys.Max() + 1;
							_NextIndex = nid + 1;
						}
						model.ID = nid;

						if(_Collection.Keys.Contains(model.ID)) {
							Debug.Fail("IDS ARE BULLSHIT");
						}
					}
					model.CollectionMember = true;
					if(model.State != ModelStates.New) model.State = ModelStates.New;
					model.ParentCollectionSerializeCallback = MemberSerializeCallback;

					if(InvokeRequired) {
						InvokeDispatcher(() => {
							_Collection.Add(model.ID, model);
						});
					} else {
						_Collection.Add(model.ID, model);
					}

					ActiveUID = model.UID;
				} else {
					Console.WriteLine("Null model");
				}
			}
		}
		protected virtual void InsertNew(TModel model) {
			if(!ActiveSelectionLocked) {
				if(model != null) {
					model.ParentCollectionSerializeCallback = MemberSerializeCallback;

					if(InvokeRequired) {
						InvokeDispatcher(() => {
							_Collection.Add(model.ID, model);
						});
					} else {
						_Collection.Add(model.ID, model);
					}
					ActiveUID = model.UID;
				} else {
					Console.WriteLine("Null model");
				}
			}
		}
		//protected virtual void InsertNewAt(TModel model, int id) {
		//
		//}
		/// <summary>
		/// Creates a copy of the <see cref="ActiveSelection"/>, adds the copy to the collection,
		/// and sets that copy as the <see cref="ActiveSelection"/>.
		/// <para>
		/// If the <see cref="ActiveSelection"/> at the time calling is currently mutable,
		/// <see cref="CancelEditActiveSelection()"/> will be called.
		/// </para>
		/// <para>
		/// The new copy will be marked as mutable.
		/// </para>
		/// </summary>
		public override void DuplicateCurrent() {
			if(!ActiveSelectionLocked && !ActiveSelectionNull) {
				AddNew(ActiveSelection.Clone() as TModel);
			}
		}
		/// <summary>
		/// Removes the <see cref="ActiveSelection" /> from this collection.
		/// </summary>
		/// <param name="archive">
		/// if set to <see langword="true" /> marks the model as archived instead of removing it. 
		/// <para>Note: This does not apply to models in the New state.</para>
		/// </param>
		public override void RemoveActiveSelection(bool archive = false) {
			if(!ActiveSelectionNull) {
				var isnew = ActiveSelection.State == ModelStates.New;
				// because the active model is new and not committed to the collection, it's ID could be recovered and reused.
				if(isnew && _NextIndex - 1 == ActiveSelection.ID) _NextIndex--;

				ActiveSelection.State = ((archive || !ActiveSelection.Removable()) && !isnew) ? ModelStates.Archived : ModelStates.Removed;
				ActiveSelection.CancelEdit();
				ActiveSelection.State = ((archive || !ActiveSelection.Removable()) && !isnew) ? ModelStates.Archived : ModelStates.Removed;
				ActiveSelection.OnDelete();
				if(ActiveSelection.State == ModelStates.Removed) {
					var id = ActiveSelection.ID;
					ActiveSelection = null;
					if(InvokeRequired) {
						InvokeDispatcher(() => {
							//if()
							_Collection.Remove(id);
						});
					} else {
						_Collection.Remove(id);
					}
				}
				ActiveSelection = null;
				ActiveUID = Guid.Empty;

				Save();
			}
		}
		public override void ForceDeleteActiveSelection() {
			if(!ActiveSelectionNull) {
				var isnew = ActiveSelection.State == ModelStates.New;
				// because the active model is new and not committed to the collection, it's ID could be recovered and reused.
				if(isnew && _NextIndex - 1 == ActiveSelection.ID) _NextIndex--;

				ActiveSelection.State = ModelStates.Removed;
				ActiveSelection.CancelEdit();
				ActiveSelection.State = ModelStates.Removed;
				ActiveSelection.OnDelete();
				if(ActiveSelection.State == ModelStates.Removed) {
					var id = ActiveSelection.ID;
					ActiveSelection = null;
					if(InvokeRequired) {
						InvokeDispatcher(() => {
							//if()
							_Collection.Remove(id);
						});
					} else {
						_Collection.Remove(id);
					}
				}
				ActiveSelection = null;
				ActiveUID = Guid.Empty;

				Save();
			}
		}
		/// <summary>
		/// Puts the <see cref="ActiveSelection"/> into edit mode, locking the <see cref="ActiveSelection"/>.
		/// </summary>
		public override void BeginEditActiveSelection() {
			if(!ActiveSelectionNull) {
				ActiveSelection.BeginEdit();
			}
		}
		/// <summary>
		/// Cancels all changes to the <see cref="ActiveSelection"/> and unlocks it.
		/// </summary>
		public override void CancelEditActiveSelection() {
			if(!ActiveSelectionNull) {
				if(ActiveSelection.State == ModelStates.New) {
					RemoveActiveSelection();
				} else {
					ActiveSelection.CancelEdit();
				}
			}
		}
		/// <summary>
		/// Ends the edit session for the <see cref="ActiveSelection"/> and unlocks it.
		/// </summary>
		public override void EndEditActiveSelection() {
			if(!ActiveSelectionNull) {
				ActiveSelection.EndEdit();
			}
			Serialize(_FileName);
		}
		/// <summary>
		/// Determines whether the specified identifier contains identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public override bool ContainsID(int id) {
			return _Collection.ContainsKey(id);
		}
		/// <summary>
		/// Determines whether the specified uid contains uid.
		/// </summary>
		/// <param name="uid">The uid.</param>
		/// <returns>
		///   <c>true</c> if the specified uid contains uid; otherwise, <c>false</c>.
		/// </returns>
		public override bool ContainsUID(Guid uid) {
			return _Collection.Any(kvp => kvp.Value.UID == uid);
		}
		/// <summary>
		/// Returns the model with the given UID
		/// </summary>
		/// <param name="uid">The uid.</param>
		/// <returns></returns>
		public TModel GetByUID(Guid uid) {
			if(ContainsUID(uid)) {
				return _Collection.Values.First(m => m.UID == uid);
			}
			return null;
		}
		/// <summary>
		/// Returns the Index of the model with the given UID
		/// </summary>
		/// <param name="uid">The uid.</param>
		/// <returns></returns>
		public int IndexOf(Guid uid) {
			if(ContainsUID(uid)) {
				return GetByUID(uid).ID;
			}
			return -1;
		}
		/// <summary>
		/// Translates an model Index/ID into a Model UID.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[Obsolete()]
		public Guid IndexToUID(int id) {
			if(ContainsID(id)) {
				return _Collection[id].UID;
			}
			return Guid.Empty;
		}
		/// <summary>
		/// Gets a private collectionView that is not influenced by filter/grouping changes made to the default 
		/// CollectionView for this collection.
		/// </summary>
		/// <returns>a new ListCollectionView for this collection.</returns>
		public override ListCollectionView GetPrivateView() {
			var x = new CollectionViewSource {
				Source = _Collection.ObservableValues
			};
			return (ListCollectionView)x.View;
		}
		/// <summary>
		/// Updates a model with a matching UID in this collection to <paramref name="model"/>, or adds <paramref name="model"/> to the collection.
		/// </summary>
		/// <param name="model">The model.</param>
		internal override void UpdateAddModel(Model model) {
			// ensure model is of the correct type
			if(model is TModel) {
				// find a UID match
				if(_Collection.Any(kvp => kvp.Value.UID == model.UID)) {
					Debug.Print($">>> Updating Model (Type: {typeof(TModel).Name}) with UID: {model.UID} VIA UID MATCH in collection: {this.GetType().Name}");
					var s = GetByUID(model.UID);
					using(s.EditToken) {
						s.LoadNewData((T)model.MasterData);
					}

					// if the UID match fails for legacy reasons, and the reference model has a false UID, try an ID match
				} else if(_Collection.ContainsKey(model.ID) && model.UID == Guid.Empty) {
					Debug.Print($">>> Updating Model (Type: {typeof(TModel).Name}) with ID: {model.ID} in collection: {this.GetType().Name}");
					using(var e = _Collection[model.ID].EditToken) {
						_Collection[model.ID].LoadNewData((T)model.MasterData);
					}

					// try another ID match, check if the UID of the target is valid
				} else if(_Collection.ContainsKey(model.ID) && _Collection[model.ID].UID == Guid.Empty) {
					using(_Collection[model.ID].EditToken) {
						_Collection[model.ID].UID = model.UID;
						_Collection[model.ID].LoadNewData((T)model.MasterData);
					}
					Debug.Print($">>> Reconfiguring Model (Type: {typeof(TModel).Name}) with UID: {model.UID} to collection: {this.GetType().Name}");

					// legacy state: model has an ID match, the UID's are valid but don't match, 
					// no valid creatorID on the reference model, and the reference model ID is outside the user range
				} else if(_Collection.ContainsKey(model.ID) && model.UID != Guid.Empty && _Collection[model.ID].UID != Guid.Empty && _Collection[model.ID].UID != model.UID && model.CreatorID == -1 && model.ID >= 100) {
					var newID = -1;
					// Try to find an ID between 0 and 100
					for(int i = 0; i < 100; i++) {
						if(_Collection.ContainsKey(i)) continue;
						newID = i;
						break;
					}
					// Find any new id, regardless of number
					if(newID == -1) {
						_NextIndex = _Collection.Keys.Max() + 1;
						newID = GetNextIndex();
					}
					model.ID = newID;
					InsertNew(model as TModel);

					// model is probably new
				} else {
					// if the ID is outside of the userrange and has a match
					// this is a case that should be covered above, but can happen supposedly.
					if(model.ID < 100 && _Collection.ContainsKey(model.ID)) {
						using(_Collection[model.ID].EditToken) {
							_Collection[model.ID].UID = model.UID;
							_Collection[model.ID].LoadNewData((T)model.MasterData);
						}
					} else {
						// definitely a new model we don't have.
						InsertNew(model as TModel);
					}
					Debug.Print($">>> Reconfiguring Model (Type: {typeof(TModel).Name}) with UID: {model.UID} to collection: {this.GetType().Name}");
				}
				EndEditActiveSelection();
			}
		}
		/// <summary>
		/// Updates a model with a model in the collection using a reference model imported from a package
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="packTag">The pack tag.</param>
		internal override void UpdateAddModel(Model model, PackageTag packTag) {
			// set the reference model's package-reference 
			model.PackageReference = packTag;

			// ensure model is of the matching type
			if(model is TModel) {
				// find a UID match
				if(_Collection.Any(kvp => kvp.Value.UID == model.UID)) {
					Debug.Print($">>> Updating Model (Type: {typeof(TModel).Name}) with UID: {model.UID} VIA UID MATCH in collection: {this.GetType().Name}");
					var s = GetByUID(model.UID);
					using(s.EditToken) {
						s.LoadNewData((T)model.MasterData);
						s.PackageReference = packTag;
					}

					// if the UID match fails for legacy reasons, and the reference model has a false UID, try an ID match
				} else if(_Collection.ContainsKey(model.ID) && model.UID == Guid.Empty) {
					Debug.Print($">>> Updating Model (Type: {typeof(TModel).Name}) with ID: {model.ID} in collection: {this.GetType().Name}");
					using(var e = _Collection[model.ID].EditToken) {
						_Collection[model.ID].LoadNewData((T)model.MasterData);
						_Collection[model.ID].PackageReference = packTag;
					}

					// try another ID match, check if the UID of the target is valid
				} else if(_Collection.ContainsKey(model.ID) && _Collection[model.ID].UID == Guid.Empty) {
					using(_Collection[model.ID].EditToken) {
						_Collection[model.ID].UID = model.UID;
						_Collection[model.ID].LoadNewData((T)model.MasterData);
						_Collection[model.ID].PackageReference = packTag;
					}
					Debug.Print($">>> Reconfiguring Model (Type: {typeof(TModel).Name}) with UID: {model.UID} to collection: {this.GetType().Name}");

					// legacy state: model has an ID match, the UID's are valid but don't match, 
					// no valid creatorID on the reference model, and the reference model ID is outside the user range
				} else if(_Collection.ContainsKey(model.ID) && model.UID != Guid.Empty && _Collection[model.ID].UID != Guid.Empty && _Collection[model.ID].UID != model.UID && model.CreatorID == -1 && model.ID >= 100) {
					var newID = -1;
					// Try to find an ID between 0 and 100
					for(int i = 0; i < 100; i++) {
						if(_Collection.ContainsKey(i)) continue;
						newID = i;
						break;
					}
					// Find any new id, regardless of number
					if(newID == -1) {
						_NextIndex = _Collection.Keys.Max() + 1;
						newID = GetNextIndex();
					}
					model.ID = newID;
					InsertNew(model as TModel);

					// model is probably new
				} else {
					// if the ID is outside of the userrange and has a match
					// this is a case that should be covered above, but can happen supposedly.
					if(model.ID < 100 && _Collection.ContainsKey(model.ID)) {
						using(_Collection[model.ID].EditToken) {
							_Collection[model.ID].UID = model.UID;
							_Collection[model.ID].LoadNewData((T)model.MasterData);
							_Collection[model.ID].PackageReference = packTag;
						}
					} else {
						// definitely a new model we don't have.
						InsertNew(model as TModel);
					}
					Debug.Print($">>> Reconfiguring Model (Type: {typeof(TModel).Name}) with UID: {model.UID} to collection: {this.GetType().Name}");
				}
				EndEditActiveSelection();
			}
		}
		/// <summary>
		/// Removes any models in this collection that have a matching package tag
		/// </summary>
		/// <param name="packageTag">The package tag.</param>
		internal override void RemoveModelsByPackageTag(PackageTag packTag) {
			// cycle through matches and remove them one by one
			foreach(var match in EnumeratePackageMatches(packTag)) {
				if(IndexOf(match) != -1) {
					if(!ActiveSelectionLocked) {
						//// set the active model to the UID of the match
						//this.ActiveUID = match;
						//// remove active selection (the match) and don't archive it.
						//RemoveActiveSelection(false);

						RemoveUID(match, false);
					} else {
#if DEBUG
						throw new InvalidOperationException("Cannot remove a model if the active selection is locked");
#endif
					}
				} else {
#if DEBUG
					throw new IndexOutOfRangeException("Index was invalid for a matched UID");
#endif
				}
			}
		}
		internal override IEnumerable<Guid> EnumeratePackageMatches(PackageTag packTag) {
			return from a in _Collection.Values where a.PackageReference.PackageGuid == packTag.PackageGuid select a.UID;
		}
		/// <summary>
		/// Refreshes the serialization callbacks.
		/// </summary>
		protected override void RefreshSerializationCallbacks() {
			// Attaching all collection member serialization callbacks so this collection can handle serialization of it's members.
			_Collection.Values.ToList().ForEach(a => a.ParentCollectionSerializeCallback = MemberSerializeCallback);
		}
		/// <summary>
		/// Performs DataContract serialization on this collection, saving it to it's persistence file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		protected override void Serialize(string fileName) {
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
			} else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("parameter \"fileName\" must be valid file name [Serialize collection]");
				Console.ResetColor();
			}
		}
		protected override void Serialize(Stream stream) {
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
			} else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("parameter \"fileName\" must be valid file name [Serialize collection]");
				Console.ResetColor();
			}
		}
		protected internal override void ExportCollection(string fileName) {
#if DEBUG_DISABLE_SAVING
			Debug.Write("Information: Saving to disk skipped by DEBUG_DISABLE_SAVING");
			return;
#endif
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
			} else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("parameter \"fileName\" must be valid file name [Serialize collection]");
				Console.ResetColor();
			}
		}
		[Obsolete]
		protected internal override void ExportSubCollection(string fileName, IEnumerable<int> ids) {
			var subCollection = new ModelCollection<TModel, T>();
			foreach(var id in ids) {
				subCollection._Collection.Add(id, this[id]);
			}
			subCollection.ExportCollection(fileName);
		}
		[Obsolete]
		protected internal override void ImportCollection(string fileName, bool overwrite) {
			ModelCollection<TModel, T> import = null;
			import = Extensions.DataContractSerialization.DeserializeContract<ModelCollection<TModel, T>>(fileName);
			if(import != null) {
				foreach(var model in import._Collection) {
					if(this._Collection.ContainsKey(model.Key)) {
						if(overwrite) this._Collection[model.Key] = model.Value;
						else this.AddNew(model.Value);
					} else {
						this._Collection.Add(model);
					}
				}
				this.RefreshSerializationCallbacks();
			}
		}
		/// <summary>
		/// Deserializes the specified <typeparamref name="TModelCollection" /> DataContract using the data in the file
		/// <paramref name="fileName" />.
		/// </summary>
		/// <typeparam name="TModelCollection">The type of the model collection.</typeparam>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		internal static TModelCollection Deserialize<TModelCollection>(string fileName) where TModelCollection : ModelCollection<TModel, T>, new() {
			TModelCollection collection = null;
			collection = Extensions.DataContractSerialization.DeserializeContract<TModelCollection>(fileName);
			if(collection == null) {
				collection = new TModelCollection() { _FileName = fileName };
			} else {
				collection._FileName = fileName;
			}
			collection.RefreshSerializationCallbacks();
			return collection;
		}
		/// <summary>
		/// Deserializes the specified stream.
		/// </summary>
		/// <typeparam name="TModelCollection">The type of the model collection.</typeparam>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		internal static TModelCollection Deserialize<TModelCollection>(Stream stream) where TModelCollection : ModelCollection<TModel, T>, new() {
			return Extensions.DataContractSerialization.DeserializeContract<TModelCollection>(stream);
		}
		/// <summary>
		/// Deserializes the specified <typeparamref name="TModelCollection" /> DataContract using the data in the file
		/// <paramref name="fileName" />.
		/// </summary>
		/// <typeparam name="TModelCollection">The type of the model collection.</typeparam>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		internal static Task<TModelCollection> AsyncDeserialize<TModelCollection>(string fileName)
			where TModelCollection : ModelCollection<TModel, T>, new() {
			return Task.Run(() => {
				TModelCollection collection = null;
				collection = Extensions.DataContractSerialization.DeserializeContract<TModelCollection>(fileName);
				collection._FileName = fileName;
				collection.RefreshSerializationCallbacks();
				return collection;
			});
		}
		public override IEnumerator<Model> GetEnumerator() {
			foreach(var m in Models) {
				yield return m;
			}
		}
		public IEnumerator<TModel> GetEnumeratorT() {
			foreach(var m in Models) {
				yield return m;
			}
		}
		public IEnumerable<TModel> AsEnumerable() {
			foreach(var m in Models) {
				yield return m;
			}
		}

		internal override void UpdateModelUIDs() {
			foreach(var m in _Collection.Values) {
				if(m.UID == Guid.Empty) {
					using(m.EditToken) {
						m.UID = Guid.NewGuid();
					}
				}
			}
		}
		internal override void UpdateCreatorUIDs() {
			foreach(var m in _Collection.Values) {
				if(m.CreatorUID == Guid.Empty) {
					if(m.CreatorID != -1) {
						using(m.EditToken) {
							m.CreatorUID = AccountManager.AccountIDToUID(m.CreatorID);
						}
					}
				}
			}
		}
		internal protected virtual void UpdateUniqueReferencing() {

		}
	}
}