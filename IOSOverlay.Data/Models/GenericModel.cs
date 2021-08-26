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

namespace IOSOverlay.Data.Models {
	/// <summary>
	/// Functionality and wrapping for a structured data model to provide persistence, state management,
	/// observability, indexing, and data versioning.
	/// <para>
	/// This class is marked as abstract and should be used as such, all public fields of the <paramref name="T"/>
	/// data-structure should have properties implemented that provide access to the <see cref="_View"/>.
	/// </para>
	/// </summary>
	/// <typeparam name="T">A DataContract structure this model will wrap.</typeparam>
	[DataContract]
	public abstract class Model<T>:Model, IComparable<Model<T>> where T : struct {
		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <value>
		/// The view.
		/// </value>
		[IgnoreDataMember]
		protected T View;
		/// <summary>
		/// This member is exposed for ModelCollections, it should not be accessed directly.
		/// <para>
		/// Gets the Master copy of this models data structure.
		/// </para>
		/// </summary>
		/// <value>
		/// The master.
		/// </value>
		[DataMember]
		protected internal T Master {
			get { return (T)MasterData; }
			set { MasterData = value; }
		}
		/// <summary>
		/// The front facing copy of this models data structure
		/// </summary>
		[IgnoreDataMember]
		protected override object _View {
			get { return View; }
			set { View = (T)value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Model{T}"/> class.
		/// </summary>
		protected Model() : base(default(T)) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="Model{T}"/> class.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		protected Model(string fileName) : base(fileName) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="Model{T}"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		protected Model(T data) {
			Master = data;
		}

		/// <summary>
		/// Loads the new data.
		/// </summary>
		/// <param name="data">The data.</param>
		public virtual void LoadNewData(T data) {
			base.LoadNewData(data);
		}
		protected async override void ValidateModel() {
			await Task.Run(() => {
				typeof(T).GetFields().ToList().ForEach((p) => Validate(p.Name));
			});
		}
		internal virtual object OnCloning<MType>(MType source, MType clone) where MType : Model<T> {
			return clone;
		}
		/// <summary>
		/// Compares to.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public virtual int CompareTo(Model<T> other) {
			return this.CompareTo(other.UID);
		}
		/// <summary>
		/// Compares to.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public override int CompareTo(Model other) {
			if(other is Model<T>) return this.CompareTo(other as Model<T>);
			return base.CompareTo(other.UID);
		}
		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString() {
			return typeof(T).Name + "_" + base.ToString();
		}
		/// <summary>
		/// Deserializes the specified file into a <typeparamref name="TModel"/>.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		internal static TModel Deserialize<TModel>(string fileName) where TModel : Model<T>, new() {
			TModel model = null;
			model = Extensions.DataContractSerialization.DeserializeContract<TModel>(fileName);
			if(model == null) {
				model = new TModel() { _FileName = fileName };
			} else {
				model._FileName = fileName;
			}
			return model;
		}
		/// <summary>
		/// Deserializes the specified stream.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="stream">The stream.</param>
		/// <returns>null if failed to read stream</returns>
		internal static TModel Deserialize<TModel>(Stream stream) where TModel : Model<T>, new() {
			return Extensions.DataContractSerialization.DeserializeContract<TModel>(stream);
		}
		/// <summary>
		/// Deserializes the specified file into a <typeparamref name="TModel"/>.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		internal static Task<TModel> AsyncDeserialize<TModel>(string fileName) where TModel : Model<T>, new() {
			return Task.Run(() => {
				TModel model = null;
				model = Extensions.DataContractSerialization.DeserializeContract<TModel>(fileName);
				model._FileName = fileName;
				return model;
			});
		}
		/// <summary>
		/// Performs an implicit conversion from <see cref="Model{T}"/> to <see cref="T"/>.
		/// </summary>
		/// <param name="m">The m.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator T(Model<T> m) {
			return m.View;
		}
	}
}
