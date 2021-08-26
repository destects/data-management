using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IOSOverlay.Data.Models.IO {
	[DataContract]
	public class PackageList {
		[DataMember]
		public List<PackageInfo> Packages = new List<PackageInfo>();

		public PackageInfo this[Guid id] {
			get {
				return Packages.Find(p => p.PackageID == id);
			}
			set {
				var i = Packages.FindIndex(p => p.PackageID == id);
				if(i != -1) {
					Packages[i] = value;
				}
			}
		}

		public bool Contains(Guid id) {
			return Packages.Any(p => p.PackageID == id);
		}
		public void AddNew(PackageInfo value) {
			Packages.Add(value);
		}
		public void AddNew(string name, Guid uid, int version) {
			Packages.Add(new PackageInfo(name, uid, version, Guid.Empty, new Version()));
		}
		public void Remove(PackageInfo value) {
			Packages.Remove(value);
		}
		public bool Remove(Guid id) {
			if(Contains(id)) {
				var p = Packages.First(a => a.PackageID == id);
				return Packages.Remove(p);
			}
			return false;
		}
	}
}
