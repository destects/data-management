using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Data.Models.IO {
	[DataContract]
	public class PackageDataEntry {
		[DataMember]
		public ModelTypeReference.Types DataType;
		[DataMember]
		public Guid ModelUID;
		[DataMember]
		public byte[] Data;
	}
}
