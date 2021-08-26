using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.IO;

namespace IOSOverlay.Data.Models {

	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct Report {
		[DataMember]
		public String ExerciseName;
		[DataMember]
		public String UserName;
		[DataMember]
		public String GroupName;
		[DataMember, Obsolete("Use UserUID", false)]
		public Int32 UserID;
		[DataMember]
		public Guid UserUID;
		[DataMember, Obsolete("Use GroupUID", false)]
		public Int32 GroupID;
		[DataMember]
		public Guid GroupUID;
		[DataMember, Obsolete("Use ExerciseUID", false)]
		public Int32 ExerciseID;
		[DataMember]
		public Guid ExerciseUID;
		[DataMember, Obsolete("Use GroupAssignmentUID", false)]
		public Int32 GroupAssignmentID;
		[DataMember]
		public Guid GroupAssignmentUID;
		[DataMember]
		public DateTime AssignedDate;
		[DataMember]
		public DateTime CompletionDate;
		[DataMember]
		public String AssignmentName;
		[DataMember]
		public Int32 MaxAttempts;
		[DataMember]
		public String EnvironmentName;
		[DataMember]
		public String BaseEnvironment;
		[DataMember]
		public String ViewPoint;
		[DataMember]
		public String Controls;
		[DataMember]
		public String Configuration;
		[DataMember]
		public String ScoringSet;
		[DataMember]
		public String Comments;
		[DataMember]
		public Int32 MinPassingScore;
		[DataMember]
		public Int32 AttemptsCount;
		[DataMember]
		public String ExerciseCategory;
		[DataMember]
		public Boolean PracticeAllowed;
		[DataMember]
		public Boolean PracticeUsed;
		[DataMember]
		public Int32 BestAttemptIndex;
		[DataMember]
		public Object[] AttemptsData;
		[DataMember]
		public Object ScoringSetCopy;
		[DataMember]
		public Object CraneConfigCopy;
		[DataMember]
		public Object ExerciseCopy;
	}

	[KnownType(typeof(Report))]
	public sealed partial class ReportModel:Model<Report> {

		[IgnoreDataMember]
		public String ExerciseName{
			get {
				return View.ExerciseName;
			}
			set {
				if(EditMode) {
					if(View.ExerciseName != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String UserName{
			get {
				return View.UserName;
			}
			set {
				if(EditMode) {
					if(View.UserName != value) {
						if(!EditMode) BeginEdit();
						View.UserName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String GroupName{
			get {
				return View.GroupName;
			}
			set {
				if(EditMode) {
					if(View.GroupName != value) {
						if(!EditMode) BeginEdit();
						View.GroupName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use UserUID", false)]
		public Int32 UserID{
			get {
				return View.UserID;
			}
			set {
				if(EditMode) {
					if(View.UserID != value) {
						if(!EditMode) BeginEdit();
						View.UserID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid UserUID{
			get {
				return View.UserUID;
			}
			set {
				if(EditMode) {
					if(View.UserUID != value) {
						if(!EditMode) BeginEdit();
						View.UserUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use GroupUID", false)]
		public Int32 GroupID{
			get {
				return View.GroupID;
			}
			set {
				if(EditMode) {
					if(View.GroupID != value) {
						if(!EditMode) BeginEdit();
						View.GroupID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid GroupUID{
			get {
				return View.GroupUID;
			}
			set {
				if(EditMode) {
					if(View.GroupUID != value) {
						if(!EditMode) BeginEdit();
						View.GroupUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use ExerciseUID", false)]
		public Int32 ExerciseID{
			get {
				return View.ExerciseID;
			}
			set {
				if(EditMode) {
					if(View.ExerciseID != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid ExerciseUID{
			get {
				return View.ExerciseUID;
			}
			set {
				if(EditMode) {
					if(View.ExerciseUID != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use GroupAssignmentUID", false)]
		public Int32 GroupAssignmentID{
			get {
				return View.GroupAssignmentID;
			}
			set {
				if(EditMode) {
					if(View.GroupAssignmentID != value) {
						if(!EditMode) BeginEdit();
						View.GroupAssignmentID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid GroupAssignmentUID{
			get {
				return View.GroupAssignmentUID;
			}
			set {
				if(EditMode) {
					if(View.GroupAssignmentUID != value) {
						if(!EditMode) BeginEdit();
						View.GroupAssignmentUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime AssignedDate{
			get {
				return View.AssignedDate;
			}
			set {
				if(EditMode) {
					if(View.AssignedDate != value) {
						if(!EditMode) BeginEdit();
						View.AssignedDate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime CompletionDate{
			get {
				return View.CompletionDate;
			}
			set {
				if(EditMode) {
					if(View.CompletionDate != value) {
						if(!EditMode) BeginEdit();
						View.CompletionDate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String AssignmentName{
			get {
				return View.AssignmentName;
			}
			set {
				if(EditMode) {
					if(View.AssignmentName != value) {
						if(!EditMode) BeginEdit();
						View.AssignmentName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 MaxAttempts{
			get {
				return View.MaxAttempts;
			}
			set {
				if(EditMode) {
					if(View.MaxAttempts != value) {
						if(!EditMode) BeginEdit();
						View.MaxAttempts = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String EnvironmentName{
			get {
				return View.EnvironmentName;
			}
			set {
				if(EditMode) {
					if(View.EnvironmentName != value) {
						if(!EditMode) BeginEdit();
						View.EnvironmentName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String BaseEnvironment{
			get {
				return View.BaseEnvironment;
			}
			set {
				if(EditMode) {
					if(View.BaseEnvironment != value) {
						if(!EditMode) BeginEdit();
						View.BaseEnvironment = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String ViewPoint{
			get {
				return View.ViewPoint;
			}
			set {
				if(EditMode) {
					if(View.ViewPoint != value) {
						if(!EditMode) BeginEdit();
						View.ViewPoint = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Controls{
			get {
				return View.Controls;
			}
			set {
				if(EditMode) {
					if(View.Controls != value) {
						if(!EditMode) BeginEdit();
						View.Controls = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Configuration{
			get {
				return View.Configuration;
			}
			set {
				if(EditMode) {
					if(View.Configuration != value) {
						if(!EditMode) BeginEdit();
						View.Configuration = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String ScoringSet{
			get {
				return View.ScoringSet;
			}
			set {
				if(EditMode) {
					if(View.ScoringSet != value) {
						if(!EditMode) BeginEdit();
						View.ScoringSet = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Comments{
			get {
				return View.Comments;
			}
			set {
				if(EditMode) {
					if(View.Comments != value) {
						if(!EditMode) BeginEdit();
						View.Comments = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 MinPassingScore{
			get {
				return View.MinPassingScore;
			}
			set {
				if(EditMode) {
					if(View.MinPassingScore != value) {
						if(!EditMode) BeginEdit();
						View.MinPassingScore = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 AttemptsCount{
			get {
				return View.AttemptsCount;
			}
			set {
				if(EditMode) {
					if(View.AttemptsCount != value) {
						if(!EditMode) BeginEdit();
						View.AttemptsCount = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String ExerciseCategory{
			get {
				return View.ExerciseCategory;
			}
			set {
				if(EditMode) {
					if(View.ExerciseCategory != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseCategory = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean PracticeAllowed{
			get {
				return View.PracticeAllowed;
			}
			set {
				if(EditMode) {
					if(View.PracticeAllowed != value) {
						if(!EditMode) BeginEdit();
						View.PracticeAllowed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean PracticeUsed{
			get {
				return View.PracticeUsed;
			}
			set {
				if(EditMode) {
					if(View.PracticeUsed != value) {
						if(!EditMode) BeginEdit();
						View.PracticeUsed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 BestAttemptIndex{
			get {
				return View.BestAttemptIndex;
			}
			set {
				if(EditMode) {
					if(View.BestAttemptIndex != value) {
						if(!EditMode) BeginEdit();
						View.BestAttemptIndex = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object[] AttemptsData{
			get {
				return View.AttemptsData;
			}
			set {
				if(EditMode) {
					if(View.AttemptsData != value) {
						if(!EditMode) BeginEdit();
						View.AttemptsData = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object ScoringSetCopy{
			get {
				return View.ScoringSetCopy;
			}
			set {
				if(EditMode) {
					if(View.ScoringSetCopy != value) {
						if(!EditMode) BeginEdit();
						View.ScoringSetCopy = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object CraneConfigCopy{
			get {
				return View.CraneConfigCopy;
			}
			set {
				if(EditMode) {
					if(View.CraneConfigCopy != value) {
						if(!EditMode) BeginEdit();
						View.CraneConfigCopy = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object ExerciseCopy{
			get {
				return View.ExerciseCopy;
			}
			set {
				if(EditMode) {
					if(View.ExerciseCopy != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseCopy = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal ReportModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public ReportModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static ReportModel Create(string fileName){
			return ReportModel.Deserialize<ReportModel>(fileName);
		}

		internal static ReportModel Create(Stream stream){
			return ReportModel.Deserialize<ReportModel>(stream);
		}

		internal static Task<ReportModel> AsyncCreate(string fileName){
			return ReportModel.AsyncDeserialize<ReportModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new ReportModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(ReportModel))]
	public sealed partial class ReportModelCollection:ModelCollection<ReportModel, Report> {

		internal static ReportModelCollection Create(string fileName) {
			return ReportModelCollection.Deserialize<ReportModelCollection>(fileName);
		}

		internal static ReportModelCollection Create(Stream stream) {
			return ReportModelCollection.Deserialize<ReportModelCollection>(stream);
		}

		internal static Task<ReportModelCollection> AsyncCreate(string fileName) {
			return ReportModelCollection.AsyncDeserialize<ReportModelCollection>(fileName);
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct User {
		[DataMember]
		public String Address;
		[DataMember]
		public String Company;
		[DataMember]
		public DateTime DateCreated;
		[DataMember]
		public String Email;
		[DataMember]
		public String FirstName;
		[DataMember]
		public String MiddleName;
		[DataMember]
		public String LastName;
		[DataMember]
		public String SirName;
		[DataMember]
		public DateTime LastActive;
		[DataMember]
		public DateTime LastModified;
		[DataMember]
		public String Phone;
		[DataMember]
		public String Title;
		[DataMember]
		public String Password;
		[DataMember]
		public String Supervisor;
		[DataMember]
		public String Notes;
		[DataMember]
		public Int32 UserType;
		[DataMember]
		public Int32 Permissions;
		[DataMember]
		public Object IOSSettings_Run;
		[DataMember]
		public Object IOSSettings_Edit;
	}

	[KnownType(typeof(User))]
	public sealed partial class UserModel:Model<User> {

		[IgnoreDataMember]
		public String Address{
			get {
				return View.Address;
			}
			set {
				if(EditMode) {
					if(View.Address != value) {
						if(!EditMode) BeginEdit();
						View.Address = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Company{
			get {
				return View.Company;
			}
			set {
				if(EditMode) {
					if(View.Company != value) {
						if(!EditMode) BeginEdit();
						View.Company = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime DateCreated{
			get {
				return View.DateCreated;
			}
			set {
				if(EditMode) {
					if(View.DateCreated != value) {
						if(!EditMode) BeginEdit();
						View.DateCreated = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Email{
			get {
				return View.Email;
			}
			set {
				if(EditMode) {
					if(View.Email != value) {
						if(!EditMode) BeginEdit();
						View.Email = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String FirstName{
			get {
				return View.FirstName;
			}
			set {
				if(EditMode) {
					if(View.FirstName != value) {
						if(!EditMode) BeginEdit();
						View.FirstName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String MiddleName{
			get {
				return View.MiddleName;
			}
			set {
				if(EditMode) {
					if(View.MiddleName != value) {
						if(!EditMode) BeginEdit();
						View.MiddleName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String LastName{
			get {
				return View.LastName;
			}
			set {
				if(EditMode) {
					if(View.LastName != value) {
						if(!EditMode) BeginEdit();
						View.LastName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String SirName{
			get {
				return View.SirName;
			}
			set {
				if(EditMode) {
					if(View.SirName != value) {
						if(!EditMode) BeginEdit();
						View.SirName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastActive{
			get {
				return View.LastActive;
			}
			set {
				if(EditMode) {
					if(View.LastActive != value) {
						if(!EditMode) BeginEdit();
						View.LastActive = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastModified{
			get {
				return View.LastModified;
			}
			set {
				if(EditMode) {
					if(View.LastModified != value) {
						if(!EditMode) BeginEdit();
						View.LastModified = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Phone{
			get {
				return View.Phone;
			}
			set {
				if(EditMode) {
					if(View.Phone != value) {
						if(!EditMode) BeginEdit();
						View.Phone = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Title{
			get {
				return View.Title;
			}
			set {
				if(EditMode) {
					if(View.Title != value) {
						if(!EditMode) BeginEdit();
						View.Title = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Password{
			get {
				return View.Password;
			}
			set {
				if(EditMode) {
					if(View.Password != value) {
						if(!EditMode) BeginEdit();
						View.Password = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Supervisor{
			get {
				return View.Supervisor;
			}
			set {
				if(EditMode) {
					if(View.Supervisor != value) {
						if(!EditMode) BeginEdit();
						View.Supervisor = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Notes{
			get {
				return View.Notes;
			}
			set {
				if(EditMode) {
					if(View.Notes != value) {
						if(!EditMode) BeginEdit();
						View.Notes = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 UserType{
			get {
				return View.UserType;
			}
			set {
				if(EditMode) {
					if(View.UserType != value) {
						if(!EditMode) BeginEdit();
						View.UserType = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 Permissions{
			get {
				return View.Permissions;
			}
			set {
				if(EditMode) {
					if(View.Permissions != value) {
						if(!EditMode) BeginEdit();
						View.Permissions = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object IOSSettings_Run{
			get {
				return View.IOSSettings_Run;
			}
			set {
				if(EditMode) {
					if(View.IOSSettings_Run != value) {
						if(!EditMode) BeginEdit();
						View.IOSSettings_Run = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object IOSSettings_Edit{
			get {
				return View.IOSSettings_Edit;
			}
			set {
				if(EditMode) {
					if(View.IOSSettings_Edit != value) {
						if(!EditMode) BeginEdit();
						View.IOSSettings_Edit = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal UserModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public UserModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static UserModel Create(string fileName){
			return UserModel.Deserialize<UserModel>(fileName);
		}

		internal static UserModel Create(Stream stream){
			return UserModel.Deserialize<UserModel>(stream);
		}

		internal static Task<UserModel> AsyncCreate(string fileName){
			return UserModel.AsyncDeserialize<UserModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new UserModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(UserModel))]
	public sealed partial class UserModelCollection:ModelCollection<UserModel, User> {

		internal static UserModelCollection Create(string fileName) {
			return UserModelCollection.Deserialize<UserModelCollection>(fileName);
		}

		internal static UserModelCollection Create(Stream stream) {
			return UserModelCollection.Deserialize<UserModelCollection>(stream);
		}

		internal static Task<UserModelCollection> AsyncCreate(string fileName) {
			return UserModelCollection.AsyncDeserialize<UserModelCollection>(fileName);
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct Group {
		[DataMember]
		public String Name;
		[DataMember, Obsolete("Use MemberUIDs", false)]
		public Int32[] MemberIDs;
		[DataMember]
		public Guid[] MemberUIDs;
		[DataMember]
		public Boolean GroupIsTemplate;
	}

	[KnownType(typeof(Group))]
	public sealed partial class GroupModel:Model<Group> {

		[IgnoreDataMember]
		public String Name{
			get {
				return View.Name;
			}
			set {
				if(EditMode) {
					if(View.Name != value) {
						if(!EditMode) BeginEdit();
						View.Name = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use MemberUIDs", false)]
		public Int32[] MemberIDs{
			get {
				return View.MemberIDs;
			}
			set {
				if(EditMode) {
					if(View.MemberIDs != value) {
						if(!EditMode) BeginEdit();
						View.MemberIDs = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid[] MemberUIDs{
			get {
				return View.MemberUIDs;
			}
			set {
				if(EditMode) {
					if(View.MemberUIDs != value) {
						if(!EditMode) BeginEdit();
						View.MemberUIDs = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean GroupIsTemplate{
			get {
				return View.GroupIsTemplate;
			}
			set {
				if(EditMode) {
					if(View.GroupIsTemplate != value) {
						if(!EditMode) BeginEdit();
						View.GroupIsTemplate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal GroupModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public GroupModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static GroupModel Create(string fileName){
			return GroupModel.Deserialize<GroupModel>(fileName);
		}

		internal static GroupModel Create(Stream stream){
			return GroupModel.Deserialize<GroupModel>(stream);
		}

		internal static Task<GroupModel> AsyncCreate(string fileName){
			return GroupModel.AsyncDeserialize<GroupModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new GroupModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(GroupModel))]
	public sealed partial class GroupModelCollection:ModelCollection<GroupModel, Group> {

		internal static GroupModelCollection Create(string fileName) {
			return GroupModelCollection.Deserialize<GroupModelCollection>(fileName);
		}

		internal static GroupModelCollection Create(Stream stream) {
			return GroupModelCollection.Deserialize<GroupModelCollection>(stream);
		}

		internal static Task<GroupModelCollection> AsyncCreate(string fileName) {
			return GroupModelCollection.AsyncDeserialize<GroupModelCollection>(fileName);
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct Environment {
		[DataMember]
		public String Name;
		[DataMember]
		public String Category;
		[DataMember]
		public String Description;
		[DataMember]
		public DateTime DateCreated;
		[DataMember]
		public DateTime LastModified;
		[DataMember]
		public DateTime LastUsed;
		[DataMember]
		public Boolean IsBuiltIn;
		[DataMember]
		public String InternalName;
		[DataMember]
		public String JSONData;
		[DataMember, Obsolete("Use BaseEnvironmentUID", false)]
		public Int32 BaseEnvironmentID;
		[DataMember]
		public Guid BaseEnvironmentUID;
		[DataMember]
		public Object EnvSettings;
		[DataMember]
		public Object EnvSupportedFeatures;
		[DataMember]
		public String PreviewImage;
		[DataMember]
		public Single EnvHeight;
		[DataMember]
		public Single EnvWidth;
		[DataMember]
		public Single EnvLength;
		[DataMember]
		public Single BridgeToGroundDistance;
	}

	[KnownType(typeof(Environment))]
	public sealed partial class EnvironmentModel:Model<Environment> {

		[IgnoreDataMember]
		public String Name{
			get {
				return View.Name;
			}
			set {
				if(EditMode) {
					if(View.Name != value) {
						if(!EditMode) BeginEdit();
						View.Name = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Category{
			get {
				return View.Category;
			}
			set {
				if(EditMode) {
					if(View.Category != value) {
						if(!EditMode) BeginEdit();
						View.Category = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Description{
			get {
				return View.Description;
			}
			set {
				if(EditMode) {
					if(View.Description != value) {
						if(!EditMode) BeginEdit();
						View.Description = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime DateCreated{
			get {
				return View.DateCreated;
			}
			set {
				if(EditMode) {
					if(View.DateCreated != value) {
						if(!EditMode) BeginEdit();
						View.DateCreated = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastModified{
			get {
				return View.LastModified;
			}
			set {
				if(EditMode) {
					if(View.LastModified != value) {
						if(!EditMode) BeginEdit();
						View.LastModified = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastUsed{
			get {
				return View.LastUsed;
			}
			set {
				if(EditMode) {
					if(View.LastUsed != value) {
						if(!EditMode) BeginEdit();
						View.LastUsed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean IsBuiltIn{
			get {
				return View.IsBuiltIn;
			}
			set {
				if(EditMode) {
					if(View.IsBuiltIn != value) {
						if(!EditMode) BeginEdit();
						View.IsBuiltIn = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String InternalName{
			get {
				return View.InternalName;
			}
			set {
				if(EditMode) {
					if(View.InternalName != value) {
						if(!EditMode) BeginEdit();
						View.InternalName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String JSONData{
			get {
				return View.JSONData;
			}
			set {
				if(EditMode) {
					if(View.JSONData != value) {
						if(!EditMode) BeginEdit();
						View.JSONData = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use BaseEnvironmentUID", false)]
		public Int32 BaseEnvironmentID{
			get {
				return View.BaseEnvironmentID;
			}
			set {
				if(EditMode) {
					if(View.BaseEnvironmentID != value) {
						if(!EditMode) BeginEdit();
						View.BaseEnvironmentID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid BaseEnvironmentUID{
			get {
				return View.BaseEnvironmentUID;
			}
			set {
				if(EditMode) {
					if(View.BaseEnvironmentUID != value) {
						if(!EditMode) BeginEdit();
						View.BaseEnvironmentUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object EnvSettings{
			get {
				return View.EnvSettings;
			}
			set {
				if(EditMode) {
					if(View.EnvSettings != value) {
						if(!EditMode) BeginEdit();
						View.EnvSettings = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object EnvSupportedFeatures{
			get {
				return View.EnvSupportedFeatures;
			}
			set {
				if(EditMode) {
					if(View.EnvSupportedFeatures != value) {
						if(!EditMode) BeginEdit();
						View.EnvSupportedFeatures = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String PreviewImage{
			get {
				return View.PreviewImage;
			}
			set {
				if(EditMode) {
					if(View.PreviewImage != value) {
						if(!EditMode) BeginEdit();
						View.PreviewImage = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single EnvHeight{
			get {
				return View.EnvHeight;
			}
			set {
				if(EditMode) {
					if(View.EnvHeight != value) {
						if(!EditMode) BeginEdit();
						View.EnvHeight = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single EnvWidth{
			get {
				return View.EnvWidth;
			}
			set {
				if(EditMode) {
					if(View.EnvWidth != value) {
						if(!EditMode) BeginEdit();
						View.EnvWidth = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single EnvLength{
			get {
				return View.EnvLength;
			}
			set {
				if(EditMode) {
					if(View.EnvLength != value) {
						if(!EditMode) BeginEdit();
						View.EnvLength = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single BridgeToGroundDistance{
			get {
				return View.BridgeToGroundDistance;
			}
			set {
				if(EditMode) {
					if(View.BridgeToGroundDistance != value) {
						if(!EditMode) BeginEdit();
						View.BridgeToGroundDistance = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal EnvironmentModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public EnvironmentModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static EnvironmentModel Create(string fileName){
			return EnvironmentModel.Deserialize<EnvironmentModel>(fileName);
		}

		internal static EnvironmentModel Create(Stream stream){
			return EnvironmentModel.Deserialize<EnvironmentModel>(stream);
		}

		internal static Task<EnvironmentModel> AsyncCreate(string fileName){
			return EnvironmentModel.AsyncDeserialize<EnvironmentModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new EnvironmentModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(EnvironmentModel))]
	public sealed partial class EnvironmentModelCollection:ModelCollection<EnvironmentModel, Environment> {

		internal static EnvironmentModelCollection Create(string fileName) {
			return EnvironmentModelCollection.Deserialize<EnvironmentModelCollection>(fileName);
		}

		internal static EnvironmentModelCollection Create(Stream stream) {
			return EnvironmentModelCollection.Deserialize<EnvironmentModelCollection>(stream);
		}

		internal static Task<EnvironmentModelCollection> AsyncCreate(string fileName) {
			return EnvironmentModelCollection.AsyncDeserialize<EnvironmentModelCollection>(fileName);
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct CraneConfig {
		[DataMember]
		public String Name;
		[DataMember]
		public String Description;
		[DataMember]
		public String Category;
		[DataMember]
		public DateTime DateCreated;
		[DataMember]
		public DateTime LastModified;
		[DataMember]
		public DateTime LastUsed;
		[DataMember]
		public Boolean IsDefault;
		[DataMember]
		public Boolean IsBuiltIn;
		[DataMember]
		public Single HoistMaxSpeed;
		[DataMember]
		public Single BringeMaxSpeed;
		[DataMember]
		public Single TrolleyMaxSpeed;
		[DataMember]
		public Single BridgeDecelerationRate;
		[DataMember]
		public Single TrolleyDecelerationRate;
		[DataMember]
		public Single SwingCoefficient;
		[DataMember]
		public Single MaxSwingAngle;
		[DataMember]
		public Int32 InputMethod;
		[DataMember]
		public Int32 ViewPoint;
		[DataMember]
		public Object OperatorAid;
		[DataMember]
		public Boolean EnableAuxTrolley;
		[DataMember]
		public Int32 ULDSelection;
		[DataMember]
		public Boolean EnableAuxHoist;
	}

	[KnownType(typeof(CraneConfig))]
	public sealed partial class CraneConfigModel:Model<CraneConfig> {

		[IgnoreDataMember]
		public String Name{
			get {
				return View.Name;
			}
			set {
				if(EditMode) {
					if(View.Name != value) {
						if(!EditMode) BeginEdit();
						View.Name = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Description{
			get {
				return View.Description;
			}
			set {
				if(EditMode) {
					if(View.Description != value) {
						if(!EditMode) BeginEdit();
						View.Description = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Category{
			get {
				return View.Category;
			}
			set {
				if(EditMode) {
					if(View.Category != value) {
						if(!EditMode) BeginEdit();
						View.Category = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime DateCreated{
			get {
				return View.DateCreated;
			}
			set {
				if(EditMode) {
					if(View.DateCreated != value) {
						if(!EditMode) BeginEdit();
						View.DateCreated = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastModified{
			get {
				return View.LastModified;
			}
			set {
				if(EditMode) {
					if(View.LastModified != value) {
						if(!EditMode) BeginEdit();
						View.LastModified = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastUsed{
			get {
				return View.LastUsed;
			}
			set {
				if(EditMode) {
					if(View.LastUsed != value) {
						if(!EditMode) BeginEdit();
						View.LastUsed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean IsDefault{
			get {
				return View.IsDefault;
			}
			set {
				if(EditMode) {
					if(View.IsDefault != value) {
						if(!EditMode) BeginEdit();
						View.IsDefault = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean IsBuiltIn{
			get {
				return View.IsBuiltIn;
			}
			set {
				if(EditMode) {
					if(View.IsBuiltIn != value) {
						if(!EditMode) BeginEdit();
						View.IsBuiltIn = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single HoistMaxSpeed{
			get {
				return View.HoistMaxSpeed;
			}
			set {
				if(EditMode) {
					if(View.HoistMaxSpeed != value) {
						if(!EditMode) BeginEdit();
						View.HoistMaxSpeed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single BringeMaxSpeed{
			get {
				return View.BringeMaxSpeed;
			}
			set {
				if(EditMode) {
					if(View.BringeMaxSpeed != value) {
						if(!EditMode) BeginEdit();
						View.BringeMaxSpeed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single TrolleyMaxSpeed{
			get {
				return View.TrolleyMaxSpeed;
			}
			set {
				if(EditMode) {
					if(View.TrolleyMaxSpeed != value) {
						if(!EditMode) BeginEdit();
						View.TrolleyMaxSpeed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single BridgeDecelerationRate{
			get {
				return View.BridgeDecelerationRate;
			}
			set {
				if(EditMode) {
					if(View.BridgeDecelerationRate != value) {
						if(!EditMode) BeginEdit();
						View.BridgeDecelerationRate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single TrolleyDecelerationRate{
			get {
				return View.TrolleyDecelerationRate;
			}
			set {
				if(EditMode) {
					if(View.TrolleyDecelerationRate != value) {
						if(!EditMode) BeginEdit();
						View.TrolleyDecelerationRate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single SwingCoefficient{
			get {
				return View.SwingCoefficient;
			}
			set {
				if(EditMode) {
					if(View.SwingCoefficient != value) {
						if(!EditMode) BeginEdit();
						View.SwingCoefficient = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single MaxSwingAngle{
			get {
				return View.MaxSwingAngle;
			}
			set {
				if(EditMode) {
					if(View.MaxSwingAngle != value) {
						if(!EditMode) BeginEdit();
						View.MaxSwingAngle = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 InputMethod{
			get {
				return View.InputMethod;
			}
			set {
				if(EditMode) {
					if(View.InputMethod != value) {
						if(!EditMode) BeginEdit();
						View.InputMethod = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 ViewPoint{
			get {
				return View.ViewPoint;
			}
			set {
				if(EditMode) {
					if(View.ViewPoint != value) {
						if(!EditMode) BeginEdit();
						View.ViewPoint = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object OperatorAid{
			get {
				return View.OperatorAid;
			}
			set {
				if(EditMode) {
					if(View.OperatorAid != value) {
						if(!EditMode) BeginEdit();
						View.OperatorAid = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableAuxTrolley{
			get {
				return View.EnableAuxTrolley;
			}
			set {
				if(EditMode) {
					if(View.EnableAuxTrolley != value) {
						if(!EditMode) BeginEdit();
						View.EnableAuxTrolley = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 ULDSelection{
			get {
				return View.ULDSelection;
			}
			set {
				if(EditMode) {
					if(View.ULDSelection != value) {
						if(!EditMode) BeginEdit();
						View.ULDSelection = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableAuxHoist{
			get {
				return View.EnableAuxHoist;
			}
			set {
				if(EditMode) {
					if(View.EnableAuxHoist != value) {
						if(!EditMode) BeginEdit();
						View.EnableAuxHoist = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal CraneConfigModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public CraneConfigModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static CraneConfigModel Create(string fileName){
			return CraneConfigModel.Deserialize<CraneConfigModel>(fileName);
		}

		internal static CraneConfigModel Create(Stream stream){
			return CraneConfigModel.Deserialize<CraneConfigModel>(stream);
		}

		internal static Task<CraneConfigModel> AsyncCreate(string fileName){
			return CraneConfigModel.AsyncDeserialize<CraneConfigModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new CraneConfigModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(CraneConfigModel))]
	public sealed partial class CraneConfigModelCollection:ModelCollection<CraneConfigModel, CraneConfig> {

		internal static CraneConfigModelCollection Create(string fileName) {
			return CraneConfigModelCollection.Deserialize<CraneConfigModelCollection>(fileName);
		}

		internal static CraneConfigModelCollection Create(Stream stream) {
			return CraneConfigModelCollection.Deserialize<CraneConfigModelCollection>(stream);
		}

		internal static Task<CraneConfigModelCollection> AsyncCreate(string fileName) {
			return CraneConfigModelCollection.AsyncDeserialize<CraneConfigModelCollection>(fileName);
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct Exercise {
		[DataMember]
		public String Name;
		[DataMember]
		public String Description;
		[DataMember]
		public String Category;
		[DataMember]
		public Boolean IsBuiltIn;
		[DataMember]
		public DateTime DateCreated;
		[DataMember]
		public DateTime LastModified;
		[DataMember]
		public DateTime LastUsed;
		[DataMember, Obsolete("Use EnvironmentUID", false)]
		public Int32 EnvironmentID;
		[DataMember]
		public Guid EnvironmentUID;
		[DataMember, Obsolete("Use CraneConfigUID", false)]
		public Int32 CraneConfigID;
		[DataMember]
		public Guid CraneConfigUID;
		[DataMember]
		public String JSONData;
		[DataMember]
		public Object EnvSettings;
		[DataMember]
		public Object ScoringSettings;
		[DataMember, Obsolete("Use ScoringSetUID", false)]
		public Int32 ScoringSetID;
		[DataMember]
		public Guid ScoringSetUID;
		[DataMember]
		public Boolean LockedForUpdate;
		[DataMember]
		public Int32 StartFlags;
		[DataMember]
		public Int32 EndFlags;
		[DataMember]
		public Object OperatorAids;
		[DataMember]
		public Single ExerciseTimeLimit;
		[DataMember]
		public Int32 InputMethod;
	}

	[KnownType(typeof(Exercise))]
	public sealed partial class ExerciseModel:Model<Exercise> {

		[IgnoreDataMember]
		public String Name{
			get {
				return View.Name;
			}
			set {
				if(EditMode) {
					if(View.Name != value) {
						if(!EditMode) BeginEdit();
						View.Name = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Description{
			get {
				return View.Description;
			}
			set {
				if(EditMode) {
					if(View.Description != value) {
						if(!EditMode) BeginEdit();
						View.Description = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Category{
			get {
				return View.Category;
			}
			set {
				if(EditMode) {
					if(View.Category != value) {
						if(!EditMode) BeginEdit();
						View.Category = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean IsBuiltIn{
			get {
				return View.IsBuiltIn;
			}
			set {
				if(EditMode) {
					if(View.IsBuiltIn != value) {
						if(!EditMode) BeginEdit();
						View.IsBuiltIn = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime DateCreated{
			get {
				return View.DateCreated;
			}
			set {
				if(EditMode) {
					if(View.DateCreated != value) {
						if(!EditMode) BeginEdit();
						View.DateCreated = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastModified{
			get {
				return View.LastModified;
			}
			set {
				if(EditMode) {
					if(View.LastModified != value) {
						if(!EditMode) BeginEdit();
						View.LastModified = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastUsed{
			get {
				return View.LastUsed;
			}
			set {
				if(EditMode) {
					if(View.LastUsed != value) {
						if(!EditMode) BeginEdit();
						View.LastUsed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use EnvironmentUID", false)]
		public Int32 EnvironmentID{
			get {
				return View.EnvironmentID;
			}
			set {
				if(EditMode) {
					if(View.EnvironmentID != value) {
						if(!EditMode) BeginEdit();
						View.EnvironmentID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid EnvironmentUID{
			get {
				return View.EnvironmentUID;
			}
			set {
				if(EditMode) {
					if(View.EnvironmentUID != value) {
						if(!EditMode) BeginEdit();
						View.EnvironmentUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use CraneConfigUID", false)]
		public Int32 CraneConfigID{
			get {
				return View.CraneConfigID;
			}
			set {
				if(EditMode) {
					if(View.CraneConfigID != value) {
						if(!EditMode) BeginEdit();
						View.CraneConfigID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid CraneConfigUID{
			get {
				return View.CraneConfigUID;
			}
			set {
				if(EditMode) {
					if(View.CraneConfigUID != value) {
						if(!EditMode) BeginEdit();
						View.CraneConfigUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String JSONData{
			get {
				return View.JSONData;
			}
			set {
				if(EditMode) {
					if(View.JSONData != value) {
						if(!EditMode) BeginEdit();
						View.JSONData = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object EnvSettings{
			get {
				return View.EnvSettings;
			}
			set {
				if(EditMode) {
					if(View.EnvSettings != value) {
						if(!EditMode) BeginEdit();
						View.EnvSettings = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object ScoringSettings{
			get {
				return View.ScoringSettings;
			}
			set {
				if(EditMode) {
					if(View.ScoringSettings != value) {
						if(!EditMode) BeginEdit();
						View.ScoringSettings = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use ScoringSetUID", false)]
		public Int32 ScoringSetID{
			get {
				return View.ScoringSetID;
			}
			set {
				if(EditMode) {
					if(View.ScoringSetID != value) {
						if(!EditMode) BeginEdit();
						View.ScoringSetID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid ScoringSetUID{
			get {
				return View.ScoringSetUID;
			}
			set {
				if(EditMode) {
					if(View.ScoringSetUID != value) {
						if(!EditMode) BeginEdit();
						View.ScoringSetUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean LockedForUpdate{
			get {
				return View.LockedForUpdate;
			}
			set {
				if(EditMode) {
					if(View.LockedForUpdate != value) {
						if(!EditMode) BeginEdit();
						View.LockedForUpdate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 StartFlags{
			get {
				return View.StartFlags;
			}
			set {
				if(EditMode) {
					if(View.StartFlags != value) {
						if(!EditMode) BeginEdit();
						View.StartFlags = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 EndFlags{
			get {
				return View.EndFlags;
			}
			set {
				if(EditMode) {
					if(View.EndFlags != value) {
						if(!EditMode) BeginEdit();
						View.EndFlags = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Object OperatorAids{
			get {
				return View.OperatorAids;
			}
			set {
				if(EditMode) {
					if(View.OperatorAids != value) {
						if(!EditMode) BeginEdit();
						View.OperatorAids = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single ExerciseTimeLimit{
			get {
				return View.ExerciseTimeLimit;
			}
			set {
				if(EditMode) {
					if(View.ExerciseTimeLimit != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseTimeLimit = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 InputMethod{
			get {
				return View.InputMethod;
			}
			set {
				if(EditMode) {
					if(View.InputMethod != value) {
						if(!EditMode) BeginEdit();
						View.InputMethod = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal ExerciseModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public ExerciseModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static ExerciseModel Create(string fileName){
			return ExerciseModel.Deserialize<ExerciseModel>(fileName);
		}

		internal static ExerciseModel Create(Stream stream){
			return ExerciseModel.Deserialize<ExerciseModel>(stream);
		}

		internal static Task<ExerciseModel> AsyncCreate(string fileName){
			return ExerciseModel.AsyncDeserialize<ExerciseModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new ExerciseModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(ExerciseModel))]
	public sealed partial class ExerciseModelCollection:ModelCollection<ExerciseModel, Exercise> {

		internal static ExerciseModelCollection Create(string fileName) {
			return ExerciseModelCollection.Deserialize<ExerciseModelCollection>(fileName);
		}

		internal static ExerciseModelCollection Create(Stream stream) {
			return ExerciseModelCollection.Deserialize<ExerciseModelCollection>(stream);
		}

		internal static Task<ExerciseModelCollection> AsyncCreate(string fileName) {
			return ExerciseModelCollection.AsyncDeserialize<ExerciseModelCollection>(fileName);
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct Assignment {
		[DataMember]
		public String AssignmentName;
		[DataMember]
		public DateTime CreationDate;
		[DataMember, Obsolete("Use AssignedToUID", false)]
		public Int32 AssignedToID;
		[DataMember]
		public Guid AssignedToUID;
		[DataMember, Obsolete("Use GroupUID", false)]
		public Int32 GroupID;
		[DataMember]
		public Guid GroupUID;
		[DataMember, Obsolete("Use GroupAssignmentUID", false)]
		public Int32 GroupAssignmentID;
		[DataMember]
		public Guid GroupAssignmentUID;
		[DataMember, Obsolete("Use ExerciseUID", false)]
		public Int32 ExerciseID;
		[DataMember]
		public Guid ExerciseUID;
		[DataMember]
		public Guid ScoringUID;
		[DataMember]
		public Int32 AttemptsTaken;
		[DataMember]
		public Int32 MaxAttempts;
		[DataMember]
		public Boolean AllowPractice;
		[DataMember]
		public Boolean Passed;
		[DataMember]
		public Boolean Complete;
		[DataMember]
		public Boolean Modified;
		[DataMember]
		public Int32 BestAttempt;
		[DataMember, Obsolete("Use LinkedReportUID", false)]
		public Int32 LinkedReportID;
		[DataMember]
		public Guid LinkedReportUID;
		[DataMember]
		public Single ExerciseTimeLimit;
		[DataMember]
		public Boolean SavePracticeScores;
		[DataMember]
		public Int32 PracticeLimit;
		[DataMember]
		public Single MaxPracticeTime;
		[DataMember]
		public Boolean LimitPracticeTime;
	}

	[KnownType(typeof(Assignment))]
	public sealed partial class AssignmentModel:Model<Assignment> {

		[IgnoreDataMember]
		public String AssignmentName{
			get {
				return View.AssignmentName;
			}
			set {
				if(EditMode) {
					if(View.AssignmentName != value) {
						if(!EditMode) BeginEdit();
						View.AssignmentName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime CreationDate{
			get {
				return View.CreationDate;
			}
			set {
				if(EditMode) {
					if(View.CreationDate != value) {
						if(!EditMode) BeginEdit();
						View.CreationDate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use AssignedToUID", false)]
		public Int32 AssignedToID{
			get {
				return View.AssignedToID;
			}
			set {
				if(EditMode) {
					if(View.AssignedToID != value) {
						if(!EditMode) BeginEdit();
						View.AssignedToID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid AssignedToUID{
			get {
				return View.AssignedToUID;
			}
			set {
				if(EditMode) {
					if(View.AssignedToUID != value) {
						if(!EditMode) BeginEdit();
						View.AssignedToUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use GroupUID", false)]
		public Int32 GroupID{
			get {
				return View.GroupID;
			}
			set {
				if(EditMode) {
					if(View.GroupID != value) {
						if(!EditMode) BeginEdit();
						View.GroupID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid GroupUID{
			get {
				return View.GroupUID;
			}
			set {
				if(EditMode) {
					if(View.GroupUID != value) {
						if(!EditMode) BeginEdit();
						View.GroupUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use GroupAssignmentUID", false)]
		public Int32 GroupAssignmentID{
			get {
				return View.GroupAssignmentID;
			}
			set {
				if(EditMode) {
					if(View.GroupAssignmentID != value) {
						if(!EditMode) BeginEdit();
						View.GroupAssignmentID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid GroupAssignmentUID{
			get {
				return View.GroupAssignmentUID;
			}
			set {
				if(EditMode) {
					if(View.GroupAssignmentUID != value) {
						if(!EditMode) BeginEdit();
						View.GroupAssignmentUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use ExerciseUID", false)]
		public Int32 ExerciseID{
			get {
				return View.ExerciseID;
			}
			set {
				if(EditMode) {
					if(View.ExerciseID != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid ExerciseUID{
			get {
				return View.ExerciseUID;
			}
			set {
				if(EditMode) {
					if(View.ExerciseUID != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid ScoringUID{
			get {
				return View.ScoringUID;
			}
			set {
				if(EditMode) {
					if(View.ScoringUID != value) {
						if(!EditMode) BeginEdit();
						View.ScoringUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 AttemptsTaken{
			get {
				return View.AttemptsTaken;
			}
			set {
				if(EditMode) {
					if(View.AttemptsTaken != value) {
						if(!EditMode) BeginEdit();
						View.AttemptsTaken = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 MaxAttempts{
			get {
				return View.MaxAttempts;
			}
			set {
				if(EditMode) {
					if(View.MaxAttempts != value) {
						if(!EditMode) BeginEdit();
						View.MaxAttempts = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean AllowPractice{
			get {
				return View.AllowPractice;
			}
			set {
				if(EditMode) {
					if(View.AllowPractice != value) {
						if(!EditMode) BeginEdit();
						View.AllowPractice = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean Passed{
			get {
				return View.Passed;
			}
			set {
				if(EditMode) {
					if(View.Passed != value) {
						if(!EditMode) BeginEdit();
						View.Passed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean Complete{
			get {
				return View.Complete;
			}
			set {
				if(EditMode) {
					if(View.Complete != value) {
						if(!EditMode) BeginEdit();
						View.Complete = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean Modified{
			get {
				return View.Modified;
			}
			set {
				if(EditMode) {
					if(View.Modified != value) {
						if(!EditMode) BeginEdit();
						View.Modified = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 BestAttempt{
			get {
				return View.BestAttempt;
			}
			set {
				if(EditMode) {
					if(View.BestAttempt != value) {
						if(!EditMode) BeginEdit();
						View.BestAttempt = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use LinkedReportUID", false)]
		public Int32 LinkedReportID{
			get {
				return View.LinkedReportID;
			}
			set {
				if(EditMode) {
					if(View.LinkedReportID != value) {
						if(!EditMode) BeginEdit();
						View.LinkedReportID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid LinkedReportUID{
			get {
				return View.LinkedReportUID;
			}
			set {
				if(EditMode) {
					if(View.LinkedReportUID != value) {
						if(!EditMode) BeginEdit();
						View.LinkedReportUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single ExerciseTimeLimit{
			get {
				return View.ExerciseTimeLimit;
			}
			set {
				if(EditMode) {
					if(View.ExerciseTimeLimit != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseTimeLimit = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean SavePracticeScores{
			get {
				return View.SavePracticeScores;
			}
			set {
				if(EditMode) {
					if(View.SavePracticeScores != value) {
						if(!EditMode) BeginEdit();
						View.SavePracticeScores = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 PracticeLimit{
			get {
				return View.PracticeLimit;
			}
			set {
				if(EditMode) {
					if(View.PracticeLimit != value) {
						if(!EditMode) BeginEdit();
						View.PracticeLimit = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single MaxPracticeTime{
			get {
				return View.MaxPracticeTime;
			}
			set {
				if(EditMode) {
					if(View.MaxPracticeTime != value) {
						if(!EditMode) BeginEdit();
						View.MaxPracticeTime = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean LimitPracticeTime{
			get {
				return View.LimitPracticeTime;
			}
			set {
				if(EditMode) {
					if(View.LimitPracticeTime != value) {
						if(!EditMode) BeginEdit();
						View.LimitPracticeTime = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal AssignmentModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public AssignmentModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static AssignmentModel Create(string fileName){
			return AssignmentModel.Deserialize<AssignmentModel>(fileName);
		}

		internal static AssignmentModel Create(Stream stream){
			return AssignmentModel.Deserialize<AssignmentModel>(stream);
		}

		internal static Task<AssignmentModel> AsyncCreate(string fileName){
			return AssignmentModel.AsyncDeserialize<AssignmentModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new AssignmentModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(AssignmentModel))]
	public sealed partial class AssignmentModelCollection:ModelCollection<AssignmentModel, Assignment> {

		internal static AssignmentModelCollection Create(string fileName) {
			return AssignmentModelCollection.Deserialize<AssignmentModelCollection>(fileName);
		}

		internal static AssignmentModelCollection Create(Stream stream) {
			return AssignmentModelCollection.Deserialize<AssignmentModelCollection>(stream);
		}

		internal static Task<AssignmentModelCollection> AsyncCreate(string fileName) {
			return AssignmentModelCollection.AsyncDeserialize<AssignmentModelCollection>(fileName);
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct SimulatorSettings {
		[DataMember]
		public Boolean EnableStudentLogin;
		[DataMember]
		public Boolean EnableDebugMode;
		[DataMember]
		public Boolean EnableInstructorLogin;
		[DataMember]
		public Boolean UseInstructorPasswords;
		[DataMember]
		public Boolean EnableSystemLogging;
		[DataMember]
		public Boolean EnableHelpMenu;
		[DataMember]
		public String MasterPassword;
		[DataMember]
		public Boolean AdminEnabled;
		[DataMember]
		public String AdminPassword;
		[DataMember]
		public Int32 MaxPlaybackSaves;
		[DataMember]
		public Int32 DisplayUnits;
		[DataMember]
		public Boolean EnableShutdown;
		[DataMember]
		public Boolean AutoStartExercises;
		[DataMember]
		public Boolean EnableFeature_Instructors;
		[DataMember]
		public Boolean EnableFeature_Students;
		[DataMember]
		public Boolean EnableFeature_Groups;
		[DataMember]
		public Boolean EnableFeature_Reports;
		[DataMember]
		public Boolean EnableFeature_Assignments;
		[DataMember]
		public Boolean EnableFeature_Environments;
		[DataMember]
		public Boolean EnableFeature_CustomScoring;
		[DataMember]
		public Boolean EnableFeature_CustomCrane;
		[DataMember]
		public Int32 SimulatorType;
		[DataMember]
		public Int32 SelectedDisplayProfile;
		[DataMember]
		public Boolean SponsoredMode;
		public Int32 VRSeatSelection;
	}

	[KnownType(typeof(SimulatorSettings))]
	public sealed partial class SimulatorSettingsModel:Model<SimulatorSettings> {

		[IgnoreDataMember]
		public Boolean EnableStudentLogin{
			get {
				return View.EnableStudentLogin;
			}
			set {
				if(EditMode) {
					if(View.EnableStudentLogin != value) {
						if(!EditMode) BeginEdit();
						View.EnableStudentLogin = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableDebugMode{
			get {
				return View.EnableDebugMode;
			}
			set {
				if(EditMode) {
					if(View.EnableDebugMode != value) {
						if(!EditMode) BeginEdit();
						View.EnableDebugMode = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableInstructorLogin{
			get {
				return View.EnableInstructorLogin;
			}
			set {
				if(EditMode) {
					if(View.EnableInstructorLogin != value) {
						if(!EditMode) BeginEdit();
						View.EnableInstructorLogin = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean UseInstructorPasswords{
			get {
				return View.UseInstructorPasswords;
			}
			set {
				if(EditMode) {
					if(View.UseInstructorPasswords != value) {
						if(!EditMode) BeginEdit();
						View.UseInstructorPasswords = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableSystemLogging{
			get {
				return View.EnableSystemLogging;
			}
			set {
				if(EditMode) {
					if(View.EnableSystemLogging != value) {
						if(!EditMode) BeginEdit();
						View.EnableSystemLogging = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableHelpMenu{
			get {
				return View.EnableHelpMenu;
			}
			set {
				if(EditMode) {
					if(View.EnableHelpMenu != value) {
						if(!EditMode) BeginEdit();
						View.EnableHelpMenu = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String MasterPassword{
			get {
				return View.MasterPassword;
			}
			set {
				if(EditMode) {
					if(View.MasterPassword != value) {
						if(!EditMode) BeginEdit();
						View.MasterPassword = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean AdminEnabled{
			get {
				return View.AdminEnabled;
			}
			set {
				if(EditMode) {
					if(View.AdminEnabled != value) {
						if(!EditMode) BeginEdit();
						View.AdminEnabled = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String AdminPassword{
			get {
				return View.AdminPassword;
			}
			set {
				if(EditMode) {
					if(View.AdminPassword != value) {
						if(!EditMode) BeginEdit();
						View.AdminPassword = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 MaxPlaybackSaves{
			get {
				return View.MaxPlaybackSaves;
			}
			set {
				if(EditMode) {
					if(View.MaxPlaybackSaves != value) {
						if(!EditMode) BeginEdit();
						View.MaxPlaybackSaves = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 DisplayUnits{
			get {
				return View.DisplayUnits;
			}
			set {
				if(EditMode) {
					if(View.DisplayUnits != value) {
						if(!EditMode) BeginEdit();
						View.DisplayUnits = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableShutdown{
			get {
				return View.EnableShutdown;
			}
			set {
				if(EditMode) {
					if(View.EnableShutdown != value) {
						if(!EditMode) BeginEdit();
						View.EnableShutdown = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean AutoStartExercises{
			get {
				return View.AutoStartExercises;
			}
			set {
				if(EditMode) {
					if(View.AutoStartExercises != value) {
						if(!EditMode) BeginEdit();
						View.AutoStartExercises = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableFeature_Instructors{
			get {
				return View.EnableFeature_Instructors;
			}
			set {
				if(EditMode) {
					if(View.EnableFeature_Instructors != value) {
						if(!EditMode) BeginEdit();
						View.EnableFeature_Instructors = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableFeature_Students{
			get {
				return View.EnableFeature_Students;
			}
			set {
				if(EditMode) {
					if(View.EnableFeature_Students != value) {
						if(!EditMode) BeginEdit();
						View.EnableFeature_Students = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableFeature_Groups{
			get {
				return View.EnableFeature_Groups;
			}
			set {
				if(EditMode) {
					if(View.EnableFeature_Groups != value) {
						if(!EditMode) BeginEdit();
						View.EnableFeature_Groups = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableFeature_Reports{
			get {
				return View.EnableFeature_Reports;
			}
			set {
				if(EditMode) {
					if(View.EnableFeature_Reports != value) {
						if(!EditMode) BeginEdit();
						View.EnableFeature_Reports = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableFeature_Assignments{
			get {
				return View.EnableFeature_Assignments;
			}
			set {
				if(EditMode) {
					if(View.EnableFeature_Assignments != value) {
						if(!EditMode) BeginEdit();
						View.EnableFeature_Assignments = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableFeature_Environments{
			get {
				return View.EnableFeature_Environments;
			}
			set {
				if(EditMode) {
					if(View.EnableFeature_Environments != value) {
						if(!EditMode) BeginEdit();
						View.EnableFeature_Environments = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableFeature_CustomScoring{
			get {
				return View.EnableFeature_CustomScoring;
			}
			set {
				if(EditMode) {
					if(View.EnableFeature_CustomScoring != value) {
						if(!EditMode) BeginEdit();
						View.EnableFeature_CustomScoring = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean EnableFeature_CustomCrane{
			get {
				return View.EnableFeature_CustomCrane;
			}
			set {
				if(EditMode) {
					if(View.EnableFeature_CustomCrane != value) {
						if(!EditMode) BeginEdit();
						View.EnableFeature_CustomCrane = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 SimulatorType{
			get {
				return View.SimulatorType;
			}
			set {
				if(EditMode) {
					if(View.SimulatorType != value) {
						if(!EditMode) BeginEdit();
						View.SimulatorType = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 SelectedDisplayProfile{
			get {
				return View.SelectedDisplayProfile;
			}
			set {
				if(EditMode) {
					if(View.SelectedDisplayProfile != value) {
						if(!EditMode) BeginEdit();
						View.SelectedDisplayProfile = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean SponsoredMode{
			get {
				return View.SponsoredMode;
			}
			set {
				if(EditMode) {
					if(View.SponsoredMode != value) {
						if(!EditMode) BeginEdit();
						View.SponsoredMode = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		public Int32 VRSeatSelection{
			get {
				return View.VRSeatSelection;
			}
			set {
				if(EditMode) {
					if(View.VRSeatSelection != value) {
						if(!EditMode) BeginEdit();
						View.VRSeatSelection = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal SimulatorSettingsModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public SimulatorSettingsModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static SimulatorSettingsModel Create(string fileName){
			return SimulatorSettingsModel.Deserialize<SimulatorSettingsModel>(fileName);
		}

		internal static SimulatorSettingsModel Create(Stream stream){
			return SimulatorSettingsModel.Deserialize<SimulatorSettingsModel>(stream);
		}

		internal static Task<SimulatorSettingsModel> AsyncCreate(string fileName){
			return SimulatorSettingsModel.AsyncDeserialize<SimulatorSettingsModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new SimulatorSettingsModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct ScoringDeductions {
		[DataMember]
		public String Name;
		[DataMember]
		public String Description;
		[DataMember]
		public String Category;
		[DataMember]
		public Boolean IsBuiltIn;
		[DataMember]
		public DateTime DateCreated;
		[DataMember]
		public DateTime LastModified;
		[DataMember]
		public DateTime LastUsed;
		[DataMember]
		public Single LoadCollisionDeduction;
		[DataMember]
		public Single HookCollisionDeduction;
		[DataMember]
		public Single ConeCollisionDeduction;
		[DataMember]
		public Single ExcessiveSwingDeduction;
		[DataMember]
		public Single LoadHeightDeduction;
		[DataMember]
		public Single SwingThreshold;
		[DataMember]
		public Single LoadHeightThreshold;
		[DataMember]
		public Single LoadCollisionSensitivity;
		[DataMember]
		public Single HookCollisionSensitivity;
		[DataMember]
		public Single LoadSetDownSensitivity;
		[DataMember]
		public Single LoadHeightTravelDistance;
		[DataMember]
		public Int32 MinimumPassingScore;
		[DataMember]
		public Single ExerciseTimeLimit;
		[DataMember]
		public Single ConeCollisionSensitivity;
		[DataMember]
		public Boolean SeparateConeCollisions;
		[DataMember]
		public Boolean HeightLimitIgnoredwithNoLoad;
		[DataMember]
		public Single PowerlineProximityPenalty;
		[DataMember]
		public Single PowerlineContactPenalty;
		[DataMember]
		public Single WarningAcknowledgementFailurePenalty;
		[DataMember]
		public Single PathDeviationDistance;
		[DataMember]
		public Single PathDeviationPenalty;
		[DataMember]
		public Single WarningTimeout;
		[DataMember]
		public Single PowerlineProximityRadius;
		[DataMember]
		public Int32 EnabledPenalties;
		[DataMember]
		public Single GeneralPenaltyDeduction;
		[DataMember]
		public Single PickupShockLoadingPenalty;
		[DataMember]
		public Single SetdownShockLoadingPenalty;
		[DataMember]
		public Int32 IncompleteExerciseMaxScore;
	}

	[KnownType(typeof(ScoringDeductions))]
	public sealed partial class ScoringDeductionsModel:Model<ScoringDeductions> {

		[IgnoreDataMember]
		public String Name{
			get {
				return View.Name;
			}
			set {
				if(EditMode) {
					if(View.Name != value) {
						if(!EditMode) BeginEdit();
						View.Name = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Description{
			get {
				return View.Description;
			}
			set {
				if(EditMode) {
					if(View.Description != value) {
						if(!EditMode) BeginEdit();
						View.Description = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public String Category{
			get {
				return View.Category;
			}
			set {
				if(EditMode) {
					if(View.Category != value) {
						if(!EditMode) BeginEdit();
						View.Category = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean IsBuiltIn{
			get {
				return View.IsBuiltIn;
			}
			set {
				if(EditMode) {
					if(View.IsBuiltIn != value) {
						if(!EditMode) BeginEdit();
						View.IsBuiltIn = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime DateCreated{
			get {
				return View.DateCreated;
			}
			set {
				if(EditMode) {
					if(View.DateCreated != value) {
						if(!EditMode) BeginEdit();
						View.DateCreated = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastModified{
			get {
				return View.LastModified;
			}
			set {
				if(EditMode) {
					if(View.LastModified != value) {
						if(!EditMode) BeginEdit();
						View.LastModified = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime LastUsed{
			get {
				return View.LastUsed;
			}
			set {
				if(EditMode) {
					if(View.LastUsed != value) {
						if(!EditMode) BeginEdit();
						View.LastUsed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single LoadCollisionDeduction{
			get {
				return View.LoadCollisionDeduction;
			}
			set {
				if(EditMode) {
					if(View.LoadCollisionDeduction != value) {
						if(!EditMode) BeginEdit();
						View.LoadCollisionDeduction = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single HookCollisionDeduction{
			get {
				return View.HookCollisionDeduction;
			}
			set {
				if(EditMode) {
					if(View.HookCollisionDeduction != value) {
						if(!EditMode) BeginEdit();
						View.HookCollisionDeduction = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single ConeCollisionDeduction{
			get {
				return View.ConeCollisionDeduction;
			}
			set {
				if(EditMode) {
					if(View.ConeCollisionDeduction != value) {
						if(!EditMode) BeginEdit();
						View.ConeCollisionDeduction = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single ExcessiveSwingDeduction{
			get {
				return View.ExcessiveSwingDeduction;
			}
			set {
				if(EditMode) {
					if(View.ExcessiveSwingDeduction != value) {
						if(!EditMode) BeginEdit();
						View.ExcessiveSwingDeduction = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single LoadHeightDeduction{
			get {
				return View.LoadHeightDeduction;
			}
			set {
				if(EditMode) {
					if(View.LoadHeightDeduction != value) {
						if(!EditMode) BeginEdit();
						View.LoadHeightDeduction = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single SwingThreshold{
			get {
				return View.SwingThreshold;
			}
			set {
				if(EditMode) {
					if(View.SwingThreshold != value) {
						if(!EditMode) BeginEdit();
						View.SwingThreshold = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single LoadHeightThreshold{
			get {
				return View.LoadHeightThreshold;
			}
			set {
				if(EditMode) {
					if(View.LoadHeightThreshold != value) {
						if(!EditMode) BeginEdit();
						View.LoadHeightThreshold = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single LoadCollisionSensitivity{
			get {
				return View.LoadCollisionSensitivity;
			}
			set {
				if(EditMode) {
					if(View.LoadCollisionSensitivity != value) {
						if(!EditMode) BeginEdit();
						View.LoadCollisionSensitivity = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single HookCollisionSensitivity{
			get {
				return View.HookCollisionSensitivity;
			}
			set {
				if(EditMode) {
					if(View.HookCollisionSensitivity != value) {
						if(!EditMode) BeginEdit();
						View.HookCollisionSensitivity = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single LoadSetDownSensitivity{
			get {
				return View.LoadSetDownSensitivity;
			}
			set {
				if(EditMode) {
					if(View.LoadSetDownSensitivity != value) {
						if(!EditMode) BeginEdit();
						View.LoadSetDownSensitivity = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single LoadHeightTravelDistance{
			get {
				return View.LoadHeightTravelDistance;
			}
			set {
				if(EditMode) {
					if(View.LoadHeightTravelDistance != value) {
						if(!EditMode) BeginEdit();
						View.LoadHeightTravelDistance = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 MinimumPassingScore{
			get {
				return View.MinimumPassingScore;
			}
			set {
				if(EditMode) {
					if(View.MinimumPassingScore != value) {
						if(!EditMode) BeginEdit();
						View.MinimumPassingScore = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single ExerciseTimeLimit{
			get {
				return View.ExerciseTimeLimit;
			}
			set {
				if(EditMode) {
					if(View.ExerciseTimeLimit != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseTimeLimit = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single ConeCollisionSensitivity{
			get {
				return View.ConeCollisionSensitivity;
			}
			set {
				if(EditMode) {
					if(View.ConeCollisionSensitivity != value) {
						if(!EditMode) BeginEdit();
						View.ConeCollisionSensitivity = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean SeparateConeCollisions{
			get {
				return View.SeparateConeCollisions;
			}
			set {
				if(EditMode) {
					if(View.SeparateConeCollisions != value) {
						if(!EditMode) BeginEdit();
						View.SeparateConeCollisions = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean HeightLimitIgnoredwithNoLoad{
			get {
				return View.HeightLimitIgnoredwithNoLoad;
			}
			set {
				if(EditMode) {
					if(View.HeightLimitIgnoredwithNoLoad != value) {
						if(!EditMode) BeginEdit();
						View.HeightLimitIgnoredwithNoLoad = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single PowerlineProximityPenalty{
			get {
				return View.PowerlineProximityPenalty;
			}
			set {
				if(EditMode) {
					if(View.PowerlineProximityPenalty != value) {
						if(!EditMode) BeginEdit();
						View.PowerlineProximityPenalty = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single PowerlineContactPenalty{
			get {
				return View.PowerlineContactPenalty;
			}
			set {
				if(EditMode) {
					if(View.PowerlineContactPenalty != value) {
						if(!EditMode) BeginEdit();
						View.PowerlineContactPenalty = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single WarningAcknowledgementFailurePenalty{
			get {
				return View.WarningAcknowledgementFailurePenalty;
			}
			set {
				if(EditMode) {
					if(View.WarningAcknowledgementFailurePenalty != value) {
						if(!EditMode) BeginEdit();
						View.WarningAcknowledgementFailurePenalty = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single PathDeviationDistance{
			get {
				return View.PathDeviationDistance;
			}
			set {
				if(EditMode) {
					if(View.PathDeviationDistance != value) {
						if(!EditMode) BeginEdit();
						View.PathDeviationDistance = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single PathDeviationPenalty{
			get {
				return View.PathDeviationPenalty;
			}
			set {
				if(EditMode) {
					if(View.PathDeviationPenalty != value) {
						if(!EditMode) BeginEdit();
						View.PathDeviationPenalty = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single WarningTimeout{
			get {
				return View.WarningTimeout;
			}
			set {
				if(EditMode) {
					if(View.WarningTimeout != value) {
						if(!EditMode) BeginEdit();
						View.WarningTimeout = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single PowerlineProximityRadius{
			get {
				return View.PowerlineProximityRadius;
			}
			set {
				if(EditMode) {
					if(View.PowerlineProximityRadius != value) {
						if(!EditMode) BeginEdit();
						View.PowerlineProximityRadius = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 EnabledPenalties{
			get {
				return View.EnabledPenalties;
			}
			set {
				if(EditMode) {
					if(View.EnabledPenalties != value) {
						if(!EditMode) BeginEdit();
						View.EnabledPenalties = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single GeneralPenaltyDeduction{
			get {
				return View.GeneralPenaltyDeduction;
			}
			set {
				if(EditMode) {
					if(View.GeneralPenaltyDeduction != value) {
						if(!EditMode) BeginEdit();
						View.GeneralPenaltyDeduction = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single PickupShockLoadingPenalty{
			get {
				return View.PickupShockLoadingPenalty;
			}
			set {
				if(EditMode) {
					if(View.PickupShockLoadingPenalty != value) {
						if(!EditMode) BeginEdit();
						View.PickupShockLoadingPenalty = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single SetdownShockLoadingPenalty{
			get {
				return View.SetdownShockLoadingPenalty;
			}
			set {
				if(EditMode) {
					if(View.SetdownShockLoadingPenalty != value) {
						if(!EditMode) BeginEdit();
						View.SetdownShockLoadingPenalty = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 IncompleteExerciseMaxScore{
			get {
				return View.IncompleteExerciseMaxScore;
			}
			set {
				if(EditMode) {
					if(View.IncompleteExerciseMaxScore != value) {
						if(!EditMode) BeginEdit();
						View.IncompleteExerciseMaxScore = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal ScoringDeductionsModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public ScoringDeductionsModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static ScoringDeductionsModel Create(string fileName){
			return ScoringDeductionsModel.Deserialize<ScoringDeductionsModel>(fileName);
		}

		internal static ScoringDeductionsModel Create(Stream stream){
			return ScoringDeductionsModel.Deserialize<ScoringDeductionsModel>(stream);
		}

		internal static Task<ScoringDeductionsModel> AsyncCreate(string fileName){
			return ScoringDeductionsModel.AsyncDeserialize<ScoringDeductionsModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new ScoringDeductionsModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(ScoringDeductionsModel))]
	public sealed partial class ScoringDeductionsModelCollection:ModelCollection<ScoringDeductionsModel, ScoringDeductions> {

		internal static ScoringDeductionsModelCollection Create(string fileName) {
			return ScoringDeductionsModelCollection.Deserialize<ScoringDeductionsModelCollection>(fileName);
		}

		internal static ScoringDeductionsModelCollection Create(Stream stream) {
			return ScoringDeductionsModelCollection.Deserialize<ScoringDeductionsModelCollection>(stream);
		}

		internal static Task<ScoringDeductionsModelCollection> AsyncCreate(string fileName) {
			return ScoringDeductionsModelCollection.AsyncDeserialize<ScoringDeductionsModelCollection>(fileName);
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct AttemptData {
		[DataMember]
		public Int32 AttemptNum;
		[DataMember]
		public Int32 Score;
		[DataMember]
		public Boolean Passed;
		[DataMember]
		public Int32 RunTime;
		[DataMember]
		public Int32 HookCollisions;
		[DataMember]
		public Int32 LoadCollisions;
		[DataMember]
		public Int32 ConeCollisions;
		[DataMember]
		public Int32 PowerLineProximityPenalties;
		[DataMember]
		public Int32 PowerLineContactPenalties;
		[DataMember]
		public Int32 WarningAcknowledgementFailures;
		[DataMember]
		public Single Overtime;
		[DataMember]
		public Int32 ExcessiveSwingingPenalty;
		[DataMember]
		public Int32 LoadHeightPenalty;
		[DataMember]
		public DateTime AttemptDate;
		[DataMember]
		public Boolean WasPractice;
		[DataMember]
		public Boolean CatastrophicFailure;
		[DataMember]
		public Boolean UnableToContinue;
		[DataMember]
		public Boolean TimeLimitReached;
		[DataMember]
		public Boolean SuccessfulFaultPass;
		[DataMember]
		public Boolean ExerciseCompleted;
		[DataMember]
		public Int32 PenaltyFlags;
		[DataMember]
		public Int32 TimesUsedTelescope;
		[DataMember]
		public Int32 TimesUsedBoom;
		[DataMember]
		public Int32 TimesUsedSwing;
		[DataMember]
		public Int32 TimesUsedHoist;
		[DataMember]
		public Int32 TimesUsedBrake;
		[DataMember]
		public Int32 TimesUsedThrottle;
		[DataMember]
		public Int32 TotalObjectives;
		[DataMember]
		public Int32 ObjectivesCompleted;
		[DataMember]
		public Int32 TimesOverloadedCrane;
		[DataMember]
		public Int32 TimesTwoBlockedCrane;
		[DataMember]
		public Guid ReportUID;
	}

	[KnownType(typeof(AttemptData))]
	public sealed partial class AttemptDataModel:Model<AttemptData> {

		[IgnoreDataMember]
		public Int32 AttemptNum{
			get {
				return View.AttemptNum;
			}
			set {
				if(EditMode) {
					if(View.AttemptNum != value) {
						if(!EditMode) BeginEdit();
						View.AttemptNum = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 Score{
			get {
				return View.Score;
			}
			set {
				if(EditMode) {
					if(View.Score != value) {
						if(!EditMode) BeginEdit();
						View.Score = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean Passed{
			get {
				return View.Passed;
			}
			set {
				if(EditMode) {
					if(View.Passed != value) {
						if(!EditMode) BeginEdit();
						View.Passed = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 RunTime{
			get {
				return View.RunTime;
			}
			set {
				if(EditMode) {
					if(View.RunTime != value) {
						if(!EditMode) BeginEdit();
						View.RunTime = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 HookCollisions{
			get {
				return View.HookCollisions;
			}
			set {
				if(EditMode) {
					if(View.HookCollisions != value) {
						if(!EditMode) BeginEdit();
						View.HookCollisions = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 LoadCollisions{
			get {
				return View.LoadCollisions;
			}
			set {
				if(EditMode) {
					if(View.LoadCollisions != value) {
						if(!EditMode) BeginEdit();
						View.LoadCollisions = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 ConeCollisions{
			get {
				return View.ConeCollisions;
			}
			set {
				if(EditMode) {
					if(View.ConeCollisions != value) {
						if(!EditMode) BeginEdit();
						View.ConeCollisions = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 PowerLineProximityPenalties{
			get {
				return View.PowerLineProximityPenalties;
			}
			set {
				if(EditMode) {
					if(View.PowerLineProximityPenalties != value) {
						if(!EditMode) BeginEdit();
						View.PowerLineProximityPenalties = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 PowerLineContactPenalties{
			get {
				return View.PowerLineContactPenalties;
			}
			set {
				if(EditMode) {
					if(View.PowerLineContactPenalties != value) {
						if(!EditMode) BeginEdit();
						View.PowerLineContactPenalties = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 WarningAcknowledgementFailures{
			get {
				return View.WarningAcknowledgementFailures;
			}
			set {
				if(EditMode) {
					if(View.WarningAcknowledgementFailures != value) {
						if(!EditMode) BeginEdit();
						View.WarningAcknowledgementFailures = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Single Overtime{
			get {
				return View.Overtime;
			}
			set {
				if(EditMode) {
					if(View.Overtime != value) {
						if(!EditMode) BeginEdit();
						View.Overtime = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 ExcessiveSwingingPenalty{
			get {
				return View.ExcessiveSwingingPenalty;
			}
			set {
				if(EditMode) {
					if(View.ExcessiveSwingingPenalty != value) {
						if(!EditMode) BeginEdit();
						View.ExcessiveSwingingPenalty = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 LoadHeightPenalty{
			get {
				return View.LoadHeightPenalty;
			}
			set {
				if(EditMode) {
					if(View.LoadHeightPenalty != value) {
						if(!EditMode) BeginEdit();
						View.LoadHeightPenalty = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime AttemptDate{
			get {
				return View.AttemptDate;
			}
			set {
				if(EditMode) {
					if(View.AttemptDate != value) {
						if(!EditMode) BeginEdit();
						View.AttemptDate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean WasPractice{
			get {
				return View.WasPractice;
			}
			set {
				if(EditMode) {
					if(View.WasPractice != value) {
						if(!EditMode) BeginEdit();
						View.WasPractice = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean CatastrophicFailure{
			get {
				return View.CatastrophicFailure;
			}
			set {
				if(EditMode) {
					if(View.CatastrophicFailure != value) {
						if(!EditMode) BeginEdit();
						View.CatastrophicFailure = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean UnableToContinue{
			get {
				return View.UnableToContinue;
			}
			set {
				if(EditMode) {
					if(View.UnableToContinue != value) {
						if(!EditMode) BeginEdit();
						View.UnableToContinue = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean TimeLimitReached{
			get {
				return View.TimeLimitReached;
			}
			set {
				if(EditMode) {
					if(View.TimeLimitReached != value) {
						if(!EditMode) BeginEdit();
						View.TimeLimitReached = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean SuccessfulFaultPass{
			get {
				return View.SuccessfulFaultPass;
			}
			set {
				if(EditMode) {
					if(View.SuccessfulFaultPass != value) {
						if(!EditMode) BeginEdit();
						View.SuccessfulFaultPass = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean ExerciseCompleted{
			get {
				return View.ExerciseCompleted;
			}
			set {
				if(EditMode) {
					if(View.ExerciseCompleted != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseCompleted = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 PenaltyFlags{
			get {
				return View.PenaltyFlags;
			}
			set {
				if(EditMode) {
					if(View.PenaltyFlags != value) {
						if(!EditMode) BeginEdit();
						View.PenaltyFlags = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TimesUsedTelescope{
			get {
				return View.TimesUsedTelescope;
			}
			set {
				if(EditMode) {
					if(View.TimesUsedTelescope != value) {
						if(!EditMode) BeginEdit();
						View.TimesUsedTelescope = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TimesUsedBoom{
			get {
				return View.TimesUsedBoom;
			}
			set {
				if(EditMode) {
					if(View.TimesUsedBoom != value) {
						if(!EditMode) BeginEdit();
						View.TimesUsedBoom = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TimesUsedSwing{
			get {
				return View.TimesUsedSwing;
			}
			set {
				if(EditMode) {
					if(View.TimesUsedSwing != value) {
						if(!EditMode) BeginEdit();
						View.TimesUsedSwing = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TimesUsedHoist{
			get {
				return View.TimesUsedHoist;
			}
			set {
				if(EditMode) {
					if(View.TimesUsedHoist != value) {
						if(!EditMode) BeginEdit();
						View.TimesUsedHoist = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TimesUsedBrake{
			get {
				return View.TimesUsedBrake;
			}
			set {
				if(EditMode) {
					if(View.TimesUsedBrake != value) {
						if(!EditMode) BeginEdit();
						View.TimesUsedBrake = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TimesUsedThrottle{
			get {
				return View.TimesUsedThrottle;
			}
			set {
				if(EditMode) {
					if(View.TimesUsedThrottle != value) {
						if(!EditMode) BeginEdit();
						View.TimesUsedThrottle = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TotalObjectives{
			get {
				return View.TotalObjectives;
			}
			set {
				if(EditMode) {
					if(View.TotalObjectives != value) {
						if(!EditMode) BeginEdit();
						View.TotalObjectives = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 ObjectivesCompleted{
			get {
				return View.ObjectivesCompleted;
			}
			set {
				if(EditMode) {
					if(View.ObjectivesCompleted != value) {
						if(!EditMode) BeginEdit();
						View.ObjectivesCompleted = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TimesOverloadedCrane{
			get {
				return View.TimesOverloadedCrane;
			}
			set {
				if(EditMode) {
					if(View.TimesOverloadedCrane != value) {
						if(!EditMode) BeginEdit();
						View.TimesOverloadedCrane = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 TimesTwoBlockedCrane{
			get {
				return View.TimesTwoBlockedCrane;
			}
			set {
				if(EditMode) {
					if(View.TimesTwoBlockedCrane != value) {
						if(!EditMode) BeginEdit();
						View.TimesTwoBlockedCrane = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid ReportUID{
			get {
				return View.ReportUID;
			}
			set {
				if(EditMode) {
					if(View.ReportUID != value) {
						if(!EditMode) BeginEdit();
						View.ReportUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal AttemptDataModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public AttemptDataModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static AttemptDataModel Create(string fileName){
			return AttemptDataModel.Deserialize<AttemptDataModel>(fileName);
		}

		internal static AttemptDataModel Create(Stream stream){
			return AttemptDataModel.Deserialize<AttemptDataModel>(stream);
		}

		internal static Task<AttemptDataModel> AsyncCreate(string fileName){
			return AttemptDataModel.AsyncDeserialize<AttemptDataModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new AttemptDataModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
	
	[DataContract, Serializable, StructLayout(LayoutKind.Sequential)]
	public partial struct GroupAssignment {
		[DataMember]
		public String AssignmentName;
		[DataMember]
		public DateTime CreationDate;
		[DataMember, Obsolete("Use GroupUID", false)]
		public Int32 GroupID;
		[DataMember]
		public Guid GroupUID;
		[DataMember, Obsolete("Use ExerciseUID", false)]
		public Int32 ExerciseID;
		[DataMember]
		public Guid ExerciseUID;
		[DataMember]
		public Int32 MaxAttempts;
		[DataMember]
		public Boolean AllowPractice;
	}

	[KnownType(typeof(GroupAssignment))]
	public sealed partial class GroupAssignmentModel:Model<GroupAssignment> {

		[IgnoreDataMember]
		public String AssignmentName{
			get {
				return View.AssignmentName;
			}
			set {
				if(EditMode) {
					if(View.AssignmentName != value) {
						if(!EditMode) BeginEdit();
						View.AssignmentName = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public DateTime CreationDate{
			get {
				return View.CreationDate;
			}
			set {
				if(EditMode) {
					if(View.CreationDate != value) {
						if(!EditMode) BeginEdit();
						View.CreationDate = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use GroupUID", false)]
		public Int32 GroupID{
			get {
				return View.GroupID;
			}
			set {
				if(EditMode) {
					if(View.GroupID != value) {
						if(!EditMode) BeginEdit();
						View.GroupID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid GroupUID{
			get {
				return View.GroupUID;
			}
			set {
				if(EditMode) {
					if(View.GroupUID != value) {
						if(!EditMode) BeginEdit();
						View.GroupUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember, Obsolete("Use ExerciseUID", false)]
		public Int32 ExerciseID{
			get {
				return View.ExerciseID;
			}
			set {
				if(EditMode) {
					if(View.ExerciseID != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Guid ExerciseUID{
			get {
				return View.ExerciseUID;
			}
			set {
				if(EditMode) {
					if(View.ExerciseUID != value) {
						if(!EditMode) BeginEdit();
						View.ExerciseUID = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Int32 MaxAttempts{
			get {
				return View.MaxAttempts;
			}
			set {
				if(EditMode) {
					if(View.MaxAttempts != value) {
						if(!EditMode) BeginEdit();
						View.MaxAttempts = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public Boolean AllowPractice{
			get {
				return View.AllowPractice;
			}
			set {
				if(EditMode) {
					if(View.AllowPractice != value) {
						if(!EditMode) BeginEdit();
						View.AllowPractice = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal GroupAssignmentModel(string fileName):base(fileName) {
			PropertyNotificationLinker.Inspect(this.GetType());
		}
		
		public GroupAssignmentModel():base(){
			PropertyNotificationLinker.Inspect(this.GetType());
		}

		internal static GroupAssignmentModel Create(string fileName){
			return GroupAssignmentModel.Deserialize<GroupAssignmentModel>(fileName);
		}

		internal static GroupAssignmentModel Create(Stream stream){
			return GroupAssignmentModel.Deserialize<GroupAssignmentModel>(stream);
		}

		internal static Task<GroupAssignmentModel> AsyncCreate(string fileName){
			return GroupAssignmentModel.AsyncDeserialize<GroupAssignmentModel>(fileName);
		}

		public override object Clone(){
			return OnCloning(this, new GroupAssignmentModel(){ Master = this.Master, State = ModelStates.New });
		}
	}
		[KnownType(typeof(GroupAssignmentModel))]
	public sealed partial class GroupAssignmentModelCollection:ModelCollection<GroupAssignmentModel, GroupAssignment> {

		internal static GroupAssignmentModelCollection Create(string fileName) {
			return GroupAssignmentModelCollection.Deserialize<GroupAssignmentModelCollection>(fileName);
		}

		internal static GroupAssignmentModelCollection Create(Stream stream) {
			return GroupAssignmentModelCollection.Deserialize<GroupAssignmentModelCollection>(stream);
		}

		internal static Task<GroupAssignmentModelCollection> AsyncCreate(string fileName) {
			return GroupAssignmentModelCollection.AsyncDeserialize<GroupAssignmentModelCollection>(fileName);
		}
	}
	}

