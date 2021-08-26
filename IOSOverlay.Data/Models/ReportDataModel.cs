using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IOSOverlay.Data {
	[Serializable, StructLayout(LayoutKind.Sequential)]
	public class ReportDataModel:IXmlSerializable {
		public string ExerciseName;
		public string StudentName;
		public string GroupName;
		public int ExerciseID;
		public int StudentID;
		public int GroupID;

		public DateTime AssignedDate;
		public DateTime CompletionDate;
		public float Score;
		public bool Passed;
		public float RunTime;

		public int HookCollisions;
		public int LoadCollisions;
		public int ExcessiveSwingingPenalty;
		public int LoadHeightPenalty;

		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema() {
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader) {
			reader.MoveToContent();
			ExerciseName = reader.GetAttribute("ExerciseName");
			StudentName = reader.GetAttribute("StudentName");
			GroupName = reader.GetAttribute("GroupName");
			if(!int.TryParse(reader.GetAttribute("ExerciseID"), out ExerciseID)) { ExerciseID = -1; }
			if(!int.TryParse(reader.GetAttribute("StudentID"), out StudentID)) { StudentID = -1; }
			if(!int.TryParse(reader.GetAttribute("GroupID"), out GroupID)) { GroupID = -1; }
			if(!DateTime.TryParse(reader.GetAttribute("AssignedDate"), out AssignedDate)) { AssignedDate = new DateTime(); }
			if(!DateTime.TryParse(reader.GetAttribute("CompletionDate"), out CompletionDate)) { CompletionDate = new DateTime(); }
			if(!float.TryParse(reader.GetAttribute("Score"), out Score)) { Score = 0; }
			if(!bool.TryParse(reader.GetAttribute("Passed"), out Passed)) { Passed = false; }
			if(!float.TryParse(reader.GetAttribute("RunTime"), out RunTime)) { RunTime = 0; }
			if(!int.TryParse(reader.GetAttribute("HookCollisions"), out HookCollisions)) { HookCollisions = 0; }
			if(!int.TryParse(reader.GetAttribute("LoadCollisions"), out LoadCollisions)) { LoadCollisions = 0; }
			if(!int.TryParse(reader.GetAttribute("ExcessiveSwingingPenalty"), out ExcessiveSwingingPenalty)) { ExcessiveSwingingPenalty = 0; }
			if(!int.TryParse(reader.GetAttribute("LoadHeightPenalty"), out LoadHeightPenalty)) { LoadHeightPenalty = 0; }

		}

		public void WriteXml(System.Xml.XmlWriter writer) {

			writer.WriteAttributeString("ExerciseName", ExerciseName);
			writer.WriteAttributeString("StudentName", StudentName);
			writer.WriteAttributeString("GroupName", GroupName);
			writer.WriteAttributeString("ExerciseID", ExerciseID.ToString());
			writer.WriteAttributeString("StudentID", StudentID.ToString());
			writer.WriteAttributeString("GroupID", GroupID.ToString());
			writer.WriteAttributeString("AssignedDate", AssignedDate.ToString());
			writer.WriteAttributeString("CompletionDate", CompletionDate.ToString());
			writer.WriteAttributeString("Score", Score.ToString());
			writer.WriteAttributeString("Passed", Passed.ToString());
			writer.WriteAttributeString("RunTime", RunTime.ToString());
			writer.WriteAttributeString("HookCollisions", HookCollisions.ToString());
			writer.WriteAttributeString("LoadCollisions", LoadCollisions.ToString());
			writer.WriteAttributeString("ExcessiveSwingingPenalty", ExcessiveSwingingPenalty.ToString());
			writer.WriteAttributeString("LoadHeightPenalty", LoadHeightPenalty.ToString());
		}

		#endregion
	}
}
