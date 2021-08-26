using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Data {
	[DataContract]
	public enum UserTypes {
		[EnumMember]
		None = -1,
		[EnumMember]
		Student = 0,
		[EnumMember]
		Instructor = 1,
		[EnumMember]
		Moderator = 2,
		[EnumMember]
		Admin = 3,
		[EnumMember]
		Master = 4,
	}

	//TODO: Enable and configure EnumMember status for Defaults
	[Flags, DataContract]
	public enum UserPermissionFlags {
		[EnumMember]
		None = 1 << 0,
		[EnumMember]
		AccessAdminPanel = 1 << 1,
		[EnumMember]
		ModifySettings = 1 << 2,
		[EnumMember]
		ModifySystemSettings = 1 << 3,
		[EnumMember]
		EditInstructors = 1 << 4,
		[EnumMember]
		ModifySelf = 1 << 5,
		[EnumMember]
		ViewStudents = 1 << 6,
		[EnumMember]
		EditStudents = 1 << 7,
		[EnumMember]
		ViewReports = 1 << 8,
		[EnumMember]
		DeleteReports = 1 << 9,
		[EnumMember]
		ViewExercises = 1 << 10,
		[EnumMember]
		EditExercises = 1 << 11,
		[EnumMember]
		ViewEnvironments = 1 << 12,
		[EnumMember]
		EditEnvironments = 1 << 13,
		[EnumMember]
		ViewCraneConfigurations = 1 << 14,
		[EnumMember]
		EditCraneConfigurations = 1 << 15,
		[EnumMember]
		RunExercises = 1 << 16,
		[EnumMember]
		ViewInputConfiguration = 1 << 17,
		[EnumMember]
		EditInputConfiguration = 1 << 18,
		[EnumMember]
		AdministratePermissions = 1 << 19,
		[EnumMember]
		ViewInstructors = 1 << 20,
		[EnumMember]
		ViewGroups = 1 << 21,
		[EnumMember]
		EditGroups = 1 << 22,
		[EnumMember]
		EditBuiltIn = 1 << 23,
		/*----------------------------------------*/
		[EnumMember(Value = "Student")]
		DefaultStudent =
			ViewStudents |
			ViewReports |
			ViewExercises |
			RunExercises,
		[EnumMember(Value = "Instructor")]
		DefaultInstructor =
			ViewGroups |
			EditGroups |
			DefaultStudent |
			AccessAdminPanel |
			ViewInstructors |
			ModifySelf |
			EditStudents |
			EditExercises |
			ViewEnvironments |
			EditEnvironments |
			ViewCraneConfigurations |
			EditCraneConfigurations,
		[EnumMember(Value = "Moderator")]
		DefaultModerator =
			DefaultInstructor |
			EditInstructors |
			ModifySettings,
		[EnumMember(Value = "Admin")]
		DefaultAdmin =
			DefaultModerator |
			DeleteReports |
			ViewInputConfiguration |
			EditInputConfiguration |
			AdministratePermissions,
		[EnumMember(Value = "Master")]
		DefaultMaster =
			DefaultAdmin |
			EditBuiltIn |
			ModifySystemSettings,

	}


}
