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
using Simulation;
using Extensions;

namespace IOSOverlay.Data.Models.IO {
	/// <summary>
	/// 
	/// </summary>
	[DataContract]
	public class ModelPackage {
		/// <summary>
		/// The information manifest for this package
		/// </summary>
		[DataMember]
		public PackageInfo Info;
		/// <summary>
		/// The data set entries for models in this package
		/// </summary>
		[DataMember]
		public List<PackageDataEntry> DataSets = new List<PackageDataEntry>();

		/// <summary>
		/// Adds the serialized model dataset entry to the <see cref="DataSets"/> list.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="type">The type.</param>
		/// <exception cref="ArgumentNullException">The argument data was null</exception>
		/// <exception cref="InvalidEnumArgumentException">type was Unknown</exception>
		public void AddEntry(byte[] data, Guid modelUID, ModelTypeReference.Types type) {
			if(data == null) throw new ArgumentNullException($"The argument {nameof(data)} cannot be null.");
			if(type == ModelTypeReference.Types.Unknown) throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(ModelTypeReference.Types));
			DataSets.Add(new PackageDataEntry() { Data = data, DataType = type, ModelUID = modelUID });
		}
		/// <summary>
		/// Serializes the <paramref name="model"/> and adds the dataset entry to the <see cref="DataSets"/> list.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="type">The type.</param>
		/// <exception cref="ArgumentNullException">The argument <paramref name="model"/> was null</exception>
		/// <exception cref="InvalidEnumArgumentException"></exception>
		public void AddEntry(Model model, ModelTypeReference.Types type) {
			if(model == null) throw new ArgumentNullException($"The argument {nameof(model)} cannot be null.");
			if(type == ModelTypeReference.Types.Unknown) throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(ModelTypeReference.Types));
			model.PackageReference = Info.GetPackageTag();
			DataSets.Add(new PackageDataEntry() {
				Data = model.SerializeExport(),
				DataType = type,
				ModelUID = model.UID
			});
		}
		/// <summary>
		/// Adds the entry.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <exception cref="ArgumentNullException">The argument {nameof(model)}</exception>
		/// <exception cref="ArgumentException">The model type could not be determind.</exception>
		public void AddEntry(Model model) {
			if(model == null) throw new ArgumentNullException($"The argument {nameof(model)} cannot be null.");
			ModelTypeReference.Types t = ModelTypeReference.GetType(model);
			if(t == ModelTypeReference.Types.Unknown) throw new ArgumentException($"The model type could not be determind.");
			model.PackageReference = Info.GetPackageTag();
			DataSets.Add(new PackageDataEntry() {
				Data = model.SerializeExport(),
				DataType = t,
				ModelUID = model.UID
			});
		}
		/// <summary>
		/// Adds the entry.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="model">The model.</param>
		/// <exception cref="ArgumentException">The model type could not be determind.</exception>
		public void AddEntry<TModel>(TModel model) where TModel : Model {
			ModelTypeReference.Types t = ModelTypeReference.GetType(model);
			if(t == ModelTypeReference.Types.Unknown) throw new ArgumentException($"The model type could not be determined.");
			model.PackageReference = Info.GetPackageTag();
			DataSets.Add(new PackageDataEntry() {
				Data = model.SerializeExport(),
				DataType = t,
				ModelUID = model.UID
			});
		}

		/// <summary>
		/// Saves to the specified directory.
		/// </summary>
		/// <param name="directory">The directory.</param>
		public void Save(string filename) {
			DataContractSerialization.SerializeContractToFile<ModelPackage>(this, filename, plainText: true, tagged: false);
		}
	}
}
