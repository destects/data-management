using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Data.Models.IO {
	[DataContract]
	public struct PackageInfo {
		/// <summary>
		/// The name of the package
		/// </summary>
		[DataMember]
		public string PackageName;
		/// <summary>
		/// The package description
		/// </summary>
		[DataMember]
		public string PackageDescription;
		/// <summary>
		/// The package identifier
		/// </summary>
		[DataMember]
		public Guid PackageID;
		/// <summary>
		/// The package version
		/// </summary>
		[DataMember]
		public int PackageVersion;
		/// <summary>
		/// The software identifier this package was exported from.
		/// </summary>
		/// <remarks>Used to ensure you're not exporting and importing incompatible data.</remarks>
		[DataMember]
		public Guid SoftwareID;
		/// <summary>
		/// The software version this package was exported from.
		/// </summary>
		/// <remarks>Used to ensure data compatibility when importing.</remarks>
		[DataMember]
		public Version SoftwareVersion;

		public PackageInfo(string name, Guid uid, int version, Guid softwareID, Version softwareVersion) {
			PackageName = name;
			PackageDescription = string.Empty;
			PackageID = uid;
			PackageVersion = version;
			SoftwareID = softwareID;
			SoftwareVersion = softwareVersion;
		}
		public PackageInfo(string name, string description, Guid uid, int version, Guid softwareID, Version softwareVersion) {
			PackageName = name;
			PackageDescription = description;
			PackageID = uid;
			PackageVersion = version;
			SoftwareID = softwareID;
			SoftwareVersion = softwareVersion;
		}

		public PackageTag GetPackageTag() {
			return new PackageTag(PackageName, PackageID, PackageVersion);
		}
	}

	public class FilePackageInfo {
		public PackageInfo PackInfo;
		public string File;
		public bool Process = false;
	}

	public class PackageInfoModel:INotifyPropertyChanged {
		private PackageInfo _PackageInfo;
		private bool _Initialized = false;
		public event PropertyChangedEventHandler PropertyChanged;

		public PackageInfo PackageInfo {
			get { return _PackageInfo; }
			set {
				_PackageInfo = value;
				RaisePropertyChanged(nameof(PackageInfo));
				RaisePropertyChanged(nameof(PackageName));
				RaisePropertyChanged(nameof(PackageID));
				RaisePropertyChanged(nameof(PackageVersion));
				RaisePropertyChanged(nameof(Initialized));
			}
		}
		public string PackageName {
			get { return _PackageInfo.PackageName; }
			set {
				_PackageInfo.PackageName = value;
				RaisePropertyChanged(nameof(PackageName));
			}
		}
		public string PackageDescription {
			get { return _PackageInfo.PackageDescription; }
			set {
				_PackageInfo.PackageDescription = value;
				RaisePropertyChanged(nameof(PackageDescription));
			}
		}
		public string PackageID {
			get { return _PackageInfo.PackageID.ToString("D"); }
			set {
				if(Guid.TryParse(value, out _PackageInfo.PackageID)) {
					RaisePropertyChanged(nameof(PackageID));
				}
			}
		}
		public int PackageVersion {
			get { return _PackageInfo.PackageVersion; }
			set {
				_PackageInfo.PackageVersion = value;
				RaisePropertyChanged(nameof(PackageVersion));
			}
		}
		public bool Initialized {
			get { return _Initialized; }
			set {
				_Initialized = value;
				RaisePropertyChanged(nameof(Initialized));
			}
		}

		private void RaisePropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public PackageTag GetPackageTag() {
			return PackageInfo.GetPackageTag();
		}
	}
}
