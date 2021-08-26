#if DEBUG
//#define RESET
//#define IGNORE_MERGE
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOSOverlay.Data.Models;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Diagnostics.Eventing;

namespace IOSOverlay.Data {
	public static class ModelManager {
		// TODO implement flags utilization to replace bools
		[Flags]
		public enum ModelCollectionUpdateFlags {
			None = 0,
			CollectionsImported = 1 << 1,
			NewCollectionsCreated = 1 << 2,
			DefaultDataUpdate = 1 << 3,
			MergingFromResources = 1 << 4,
			UIDInitialMigration = 1 << 5,
			ErrorsInLoading = 1 << 10,
		}
		private const string C_DBF_SETTINGS = "Settings.dbf";
		private const string C_DBF_REPORTS = "Reports.dbf";
		private const string C_DBF_USERS = "Users.dbf";
		private const string C_DBF_GROUPS = "Groups.dbf";
		private const string C_DBF_ENVIRONMENTS = "Environments.dbf";
		private const string C_DBF_CRANEVARIABLES = "CraneConfigurations.dbf";
		private const string C_DBF_EXERCISES = "Exercises.dbf";
		private const string C_DBF_ASSIGNMENTS = "Assignments.dbf";
		private const string C_DBF_GROUPASSIGNMENTS = "GroupAssignments.dbf";
		private const string C_DBF_SCORINGSETS = "ScoringSets.dbf";

		private const bool BACKUP_ENABLED = true;
		private const int BACKUP_COUNT = 100;

		public static bool Initializing { get; private set; } = false;
		public static bool SettingInitialUIDs { get; private set; } = false;

		private static SimulatorSettingsModel _SimulatorSettings;
		private static ReportModelCollection _ReportCollection;
		private static UserModelCollection _UserCollection;
		private static GroupModelCollection _GroupCollection;
		private static EnvironmentModelCollection _EnvironmentCollection;
		private static ExerciseModelCollection _ExerciseCollection;
		private static CraneConfigModelCollection _CraneConfigCollection;
		private static AssignmentModelCollection _AssignmentCollection;
		private static GroupAssignmentModelCollection _GroupAssignmentsCollection;
		private static ScoringDeductionsModelCollection _ScoringSetsCollection;

		private static string _RecoveryArchive {
			get {
				return Simulation.Directories.BackupsDirectory + "LastSuccessfulLoad.zip";
			}
		}
		public static SimulatorSettingsModel SimulatorSettings {
			get { return ModelManager._SimulatorSettings; }
			private set { ModelManager._SimulatorSettings = value; }
		}
		public static ReportModelCollection ReportCollection {
			get { return ModelManager._ReportCollection; }
			private set { ModelManager._ReportCollection = value; }
		}
		public static UserModelCollection UserCollection {
			get { return ModelManager._UserCollection; }
			private set { ModelManager._UserCollection = value; }
		}
		public static GroupModelCollection GroupCollection {
			get { return ModelManager._GroupCollection; }
			private set { ModelManager._GroupCollection = value; }
		}
		public static EnvironmentModelCollection EnvironmentCollection {
			get { return ModelManager._EnvironmentCollection; }
			private set { ModelManager._EnvironmentCollection = value; }
		}
		public static ExerciseModelCollection ExerciseCollection {
			get { return ModelManager._ExerciseCollection; }
			private set { ModelManager._ExerciseCollection = value; }
		}
		public static CraneConfigModelCollection CraneConfigCollection {
			get { return ModelManager._CraneConfigCollection; }
			private set { ModelManager._CraneConfigCollection = value; }
		}
		public static AssignmentModelCollection AssignmentCollection {
			get { return ModelManager._AssignmentCollection; }
			private set { ModelManager._AssignmentCollection = value; }
		}
		public static GroupAssignmentModelCollection GroupAssignmentsCollection {
			get { return ModelManager._GroupAssignmentsCollection; }
			private set { ModelManager._GroupAssignmentsCollection = value; }
		}
		public static ScoringDeductionsModelCollection ScoringSetsCollection {
			get { return ModelManager._ScoringSetsCollection; }
			private set { ModelManager._ScoringSetsCollection = value; }
		}

		public static void InitializeModels() {
			Initializing = true;
			Model.BeginInit();
			ModelCollection.BeginInit();

			UnpackageDefaults();
			// We're doing backup's at initialization to ensure that we don't lose data on due to modeling changes
			// before the first backup is created
			if(BACKUP_ENABLED) BackupRepo();
			LoadModelCollections();
			Models.IO.PackageManager.RunDefaultsCheck();
			SyncCheck();
#if !IGNORE_MERGE
			LoadAndMergeUpdates();
#endif
			Models.IO.PackageManager.RunPackageUpdates();
			ProcessLoaded();
			ModelCollection.EndInit();
			Model.EndInit();
			Initializing = false;
		}
		private static void ProcessLoaded() {
			UserCollection.ProcessOnLoadCollection();

			GroupCollection.ProcessOnLoadCollection();

			CraneConfigCollection.ProcessOnLoadCollection();
			EnvironmentCollection.ProcessOnLoadCollection();
			ScoringSetsCollection.ProcessOnLoadCollection();
			ExerciseCollection.ProcessOnLoadCollection();

			AssignmentCollection.ProcessOnLoadCollection();
			GroupAssignmentsCollection.ProcessOnLoadCollection();

			ReportCollection.ProcessOnLoadCollection();
		}
		private static void SyncCheck() {
			bool requiresSyncing = false;

			if(ExerciseCollection.Models.Count > 0 && ExerciseCollection.FirstUID == Guid.Empty) requiresSyncing = true;
			if(ReportCollection.Models.Count > 0 && ReportCollection.FirstUID == Guid.Empty) requiresSyncing = true;
			if(GroupCollection.Models.Count > 0 && GroupCollection.FirstUID == Guid.Empty) requiresSyncing = true;
			if(UserCollection.Models.Count > 0 && UserCollection.FirstUID == Guid.Empty) requiresSyncing = true;
			if(EnvironmentCollection.Models.Count > 0 && EnvironmentCollection.FirstUID == Guid.Empty) requiresSyncing = true;
			if(CraneConfigCollection.Models.Count > 0 && CraneConfigCollection.FirstUID == Guid.Empty) requiresSyncing = true;
			if(AssignmentCollection.Models.Count > 0 && AssignmentCollection.FirstUID == Guid.Empty) requiresSyncing = true;
			if(GroupAssignmentsCollection.Models.Count > 0 && GroupAssignmentsCollection.FirstUID == Guid.Empty) requiresSyncing = true;
			if(ScoringSetsCollection.Models.Count > 0 && ScoringSetsCollection.FirstUID == Guid.Empty) requiresSyncing = true;

			if(requiresSyncing) {
				SyncAndUpdateUIDs();
			}
		}

		private static void LoadModelCollections() {
			//SimulatorSettings = SimulatorSettingsModel.Create(GetDBFDirectory(C_DBF_SETTINGS));
			//ReportCollection = ReportModelCollection.Create(GetDBFDirectory(C_DBF_REPORTS));
			//UserCollection = UserModelCollection.Create(GetDBFDirectory(C_DBF_USERS));
			//GroupCollection = GroupModelCollection.Create(GetDBFDirectory(C_DBF_GROUPS));
			//EnvironmentCollection = EnvironmentModelCollection.Create(GetDBFDirectory(C_DBF_ENVIRONMENTS));
			//ExerciseCollection = ExerciseModelCollection.Create(GetDBFDirectory(C_DBF_EXERCISES));
			//CraneConfigCollection = CraneConfigModelCollection.Create(GetDBFDirectory(C_DBF_CRANEVARIABLES));
			//AssignmentCollection = AssignmentModelCollection.Create(GetDBFDirectory(C_DBF_ASSIGNMENTS));
			//GroupAssignmentsCollection = GroupAssignmentModelCollection.Create(GetDBFDirectory(C_DBF_GROUPASSIGNMENTS));
			//ScoringSetsCollection = ScoringDeductionsModelCollection.Create(GetDBFDirectory(C_DBF_SCORINGSETS));

			SimulatorSettings = LoadModel(C_DBF_SETTINGS, SimulatorSettingsModel.Create);
			ReportCollection = LoadModelCollection(C_DBF_REPORTS, ReportModelCollection.Create);
			UserCollection = LoadModelCollection(C_DBF_USERS, UserModelCollection.Create);
			GroupCollection = LoadModelCollection(C_DBF_GROUPS, GroupModelCollection.Create);
			EnvironmentCollection = LoadModelCollection(C_DBF_ENVIRONMENTS, EnvironmentModelCollection.Create);
			ExerciseCollection = LoadModelCollection(C_DBF_EXERCISES, ExerciseModelCollection.Create);
			CraneConfigCollection = LoadModelCollection(C_DBF_CRANEVARIABLES, CraneConfigModelCollection.Create);
			AssignmentCollection = LoadModelCollection(C_DBF_ASSIGNMENTS, AssignmentModelCollection.Create);
			GroupAssignmentsCollection = LoadModelCollection(C_DBF_GROUPASSIGNMENTS, GroupAssignmentModelCollection.Create);
			ScoringSetsCollection = LoadModelCollection(C_DBF_SCORINGSETS, ScoringDeductionsModelCollection.Create);
		}
		private static TModelCollection LoadModelCollection<TModelCollection>(string dbfName, Func<string, TModelCollection> factory) where TModelCollection : ModelCollection, new() {
			TModelCollection collection = null;
			try {
				collection = factory(GetDBFDirectory(dbfName));
				if(collection != null) {
					BackupVerifiedDBF(dbfName);
				}
			} catch(Exception e) {
				Trace.TraceError($"Model collection DBF failed to load, attempting recovery Exception: {e.Message}");
				if(TryRecoverDBF(dbfName)) {
					try {
						collection = factory(GetDBFDirectory(dbfName));
					} catch {
						// TODO: Handle Exceptions
						throw;
					}
				} else {
					Trace.TraceError($"Could not recover DBF {dbfName}");
				}
			}

			if(collection == null) {
				Trace.TraceWarning($"Could not load or recover DBF {dbfName}, forced to load default data");
				LoadDefaultDBFData(dbfName);
				collection = factory(GetDBFDirectory(dbfName));
			}

			return collection;
		}
		private static TModel LoadModel<TModel>(string dbfName, Func<string, TModel> factory) where TModel : Model, new() {
			TModel collection = null;
			try {
				collection = factory(GetDBFDirectory(dbfName));
				if(collection != null) {
					BackupVerifiedDBF(dbfName);
				}
			} catch(Exception e) {
				Trace.TraceError($"Model failed to load, attempting recovery Exception: {e.Message}");
				if(TryRecoverDBF(dbfName)) {
					try {
						collection = factory(GetDBFDirectory(dbfName));
					} catch {
						// TODO: Handle Exceptions
						throw;
					}
				} else {
					Trace.TraceError($"Could not recover DBF {dbfName}");
				}
			}

			if(collection == null) {
				Trace.TraceWarning($"Could not load or recover DBF {dbfName}, forced to load default data");
				LoadDefaultDBFData(dbfName);
				collection = factory(GetDBFDirectory(dbfName));
			}

			return collection;
		}

		[Obsolete("Currently disabled pending investigation.")]
		private static async void AsyncImportModelCollections() {
			SimulatorSettings = await SimulatorSettingsModel.AsyncCreate(GetDBFDirectory(C_DBF_SETTINGS));
			ReportCollection = await ReportModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_REPORTS));
			UserCollection = await UserModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_USERS));
			GroupCollection = await GroupModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_GROUPS));
			EnvironmentCollection = await EnvironmentModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_ENVIRONMENTS));
			ExerciseCollection = await ExerciseModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_EXERCISES));
			CraneConfigCollection = await CraneConfigModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_CRANEVARIABLES));
			AssignmentCollection = await AssignmentModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_ASSIGNMENTS));
			GroupAssignmentsCollection = await GroupAssignmentModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_GROUPASSIGNMENTS));
			ScoringSetsCollection = await ScoringDeductionsModelCollection.AsyncCreate(GetDBFDirectory(C_DBF_SCORINGSETS));
		}
		private static void UnpackageDefaults() {
			Simulation.Directories.CheckCreateMissingDirectories();

			//foreach(var f in new[] {
			//	new {dir = GetDBFDirectory(C_DBF_SETTINGS), dat = Properties.Resources.Settings},
			//	new {dir = GetDBFDirectory(C_DBF_REPORTS), dat = Properties.Resources.Reports},
			//	new {dir = GetDBFDirectory(C_DBF_USERS), dat = Properties.Resources.Users},
			//	new {dir = GetDBFDirectory(C_DBF_GROUPS), dat = Properties.Resources.Groups},
			//	new {dir = GetDBFDirectory(C_DBF_ENVIRONMENTS), dat = Properties.Resources.Environments},
			//	new {dir = GetDBFDirectory(C_DBF_EXERCISES), dat = Properties.Resources.Exercises},
			//	new {dir = GetDBFDirectory(C_DBF_CRANEVARIABLES), dat = Properties.Resources.CraneConfigurations},
			//	new {dir = GetDBFDirectory(C_DBF_ASSIGNMENTS), dat = Properties.Resources.Assignments},
			//	new {dir = GetDBFDirectory(C_DBF_GROUPASSIGNMENTS), dat = Properties.Resources.GroupAssignments},
			//	new {dir = GetDBFDirectory(C_DBF_SCORINGSETS), dat = Properties.Resources.ScoringSets},
			//}) {
			//	if(!File.Exists(f.dir)) File.WriteAllText(f.dir, f.dat);
			//}

#if RESET
			var fl = GetDBFDirectory(C_DBF_SETTINGS);
			SimulatorSettings = SimulatorSettingsModel.Create(fl);
			SimulatorSettings.Save();

			fl = GetDBFDirectory(C_DBF_REPORTS);
			ReportCollection = ReportModelCollection.Create(fl);
			ReportCollection.Save();

			fl = GetDBFDirectory(C_DBF_USERS);
			UserCollection = UserModelCollection.Create(fl);
			UserCollection.Save();

			fl = GetDBFDirectory(C_DBF_GROUPS);
			GroupCollection = GroupModelCollection.Create(fl);
			GroupCollection.Save();

			fl = GetDBFDirectory(C_DBF_ENVIRONMENTS);
			EnvironmentCollection = EnvironmentModelCollection.Create(fl);
			EnvironmentCollection.Save();

			fl = GetDBFDirectory(C_DBF_EXERCISES);
			ExerciseCollection = ExerciseModelCollection.Create(fl);
			ExerciseCollection.Save();

			fl = GetDBFDirectory(C_DBF_CRANEVARIABLES);
			CraneConfigCollection = CraneConfigModelCollection.Create(fl);
			CraneConfigCollection.Save();

			fl = GetDBFDirectory(C_DBF_ASSIGNMENTS);
			AssignmentCollection = AssignmentModelCollection.Create(fl);
			AssignmentCollection.Save();

			fl = GetDBFDirectory(C_DBF_GROUPASSIGNMENTS);
			GroupAssignmentsCollection = GroupAssignmentModelCollection.Create(fl);
			GroupAssignmentsCollection.Save();

			fl = GetDBFDirectory(C_DBF_SCORINGSETS);
			ScoringSetsCollection = ScoringDeductionsModelCollection.Create(fl);
			ScoringSetsCollection.Save();
#else

			foreach(var f in new[] {
				C_DBF_SETTINGS,
				C_DBF_REPORTS,
				C_DBF_USERS,
				C_DBF_GROUPS,
				C_DBF_ENVIRONMENTS,
				C_DBF_EXERCISES,
				C_DBF_CRANEVARIABLES,
				C_DBF_ASSIGNMENTS,
				C_DBF_GROUPASSIGNMENTS,
				C_DBF_SCORINGSETS,
			}) {
				if(!File.Exists(GetDBFDirectory(f))) {
					LoadDefaultDBFData(f);
				}
			}
#endif
		}

		/// <summary>
		/// Loads the default DBF data for a specified dbf.
		/// </summary>
		/// <param name="dbfName">Name of the DBF.</param>
		private static void LoadDefaultDBFData(string dbfName) {
			switch(dbfName) {
				case C_DBF_SETTINGS: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.Settings); break;
				case C_DBF_REPORTS: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.Reports); break;
				case C_DBF_USERS: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.Users); break;
				case C_DBF_GROUPS: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.Groups); break;
				case C_DBF_ENVIRONMENTS: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.Environments); break;
				case C_DBF_EXERCISES: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.Exercises); break;
				case C_DBF_CRANEVARIABLES: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.CraneConfigurations); break;
				case C_DBF_ASSIGNMENTS: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.Assignments); break;
				case C_DBF_GROUPASSIGNMENTS: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.GroupAssignments); break;
				case C_DBF_SCORINGSETS: File.WriteAllText(GetDBFDirectory(dbfName), Properties.Resources.ScoringSets); break;
			}
		}
		public static void SaveModelCollections() {
			SimulatorSettings.Save();
			ReportCollection.Save();
			UserCollection.Save();
			GroupCollection.Save();
			EnvironmentCollection.Save();
			ExerciseCollection.Save();
			CraneConfigCollection.Save();
			AssignmentCollection.Save();
			GroupAssignmentsCollection.Save();
			ScoringSetsCollection.Save();
			//VerifySavedData();
		}
		private static void LoadAndMergeUpdates() {
#if RESET
#else
			UpdateModelCollectionStream(Properties.Resources.Reports, (s) => { UpdateCollection(ReportModelCollection.Create(s), ReportCollection); });
			UpdateModelCollectionStream(Properties.Resources.Users, (s) => { UpdateCollection(UserModelCollection.Create(s), UserCollection); });
			UpdateModelCollectionStream(Properties.Resources.Groups, (s) => { UpdateCollection(GroupModelCollection.Create(s), GroupCollection); });
			UpdateModelCollectionStream(Properties.Resources.Environments, (s) => { UpdateCollection(EnvironmentModelCollection.Create(s), EnvironmentCollection); });
			UpdateModelCollectionStream(Properties.Resources.CraneConfigurations, (s) => { UpdateCollection(CraneConfigModelCollection.Create(s), CraneConfigCollection); });
			UpdateModelCollectionStream(Properties.Resources.Exercises, (s) => { UpdateCollection(ExerciseModelCollection.Create(s), ExerciseCollection); });
			UpdateModelCollectionStream(Properties.Resources.Assignments, (s) => { UpdateCollection(AssignmentModelCollection.Create(s), AssignmentCollection); });
			UpdateModelCollectionStream(Properties.Resources.GroupAssignments, (s) => { UpdateCollection(GroupAssignmentModelCollection.Create(s), GroupAssignmentsCollection); });
			UpdateModelCollectionStream(Properties.Resources.ScoringSets, (s) => { UpdateCollection(ScoringDeductionsModelCollection.Create(s), ScoringSetsCollection); });
#endif
		}
		private static void UpdateCollection(ModelCollection update, ModelCollection current) {
			if(update != null && current != null) {
				if(current.TryUpdateCollectionState(update)) {
					foreach(var model in update) {
						current.UpdateAddModel(model);
					}
				}
			}
		}
		private static void UpdateModelCollectionStream(string data, Action<Stream> updateFunction) {
			UpdateModelCollectionStream(Encoding.UTF8.GetBytes(data), updateFunction);
		}
		private static void UpdateModelCollectionStream(byte[] data, Action<Stream> updateFunction) {
			using(var updateStream = new MemoryStream()) {
				updateStream.Write(data, 0, data.Length);
				updateStream.Position = 0;
				//using(var uw = new StreamWriter(updateStream, Encoding.UTF8)) {
				//	uw.Write(data);
				//	uw.Flush();
				//	updateStream.Position = 0;
				//}
				updateFunction(updateStream);
			}
		}
		private static void BackupRepo() {
#if DEBUG_DISABLE_SAVING
			Debug.WriteLine("Information: Saving to disk skipped by DEBUG_DISABLE_SAVING");
			return;
#endif
			// get all RepoBackup's in the backup dir, sorted by descending backupID (5,4,3,2,1,0)
			var files = Simulation.Directories.Backups.GetFiles("RepoBackup_*.zip").OrderByDescending((a) => int.Parse(a.Name.Replace("RepoBackup_", "").Replace(".zip", "")));
			// loop through the backup files and increment their backup ID's
			foreach(var file in files) {
				var id = 1 + int.Parse(file.Name.Replace("RepoBackup_", "").Replace(".zip", ""));
				file.MoveTo(Simulation.Directories.BackupsDirectory + "RepoBackup_" + id + ".zip");
				// If the oldest backup is older than our backup count, remove it.
				if(id > BACKUP_COUNT) {
					file.Delete();
				}
			}

			ZipFile.CreateFromDirectory(Simulation.Directories.RepoFileDirectory, Simulation.Directories.BackupsDirectory + "RepoBackup_0.zip");
		}

		/// <summary>
		/// Attempts to load a DBF from the recovery archive and replace a corrupted DBF with it.
		/// </summary>
		/// <param name="dbfName">Name of the DBF.</param>
		/// <returns>True if the DBF was successfully recovered from the recovery archive; otherwise false.</returns>
		private static bool TryRecoverDBF(string dbfName) {
			if(File.Exists(_RecoveryArchive)) {
				using(var ar = ZipFile.Open(_RecoveryArchive, ZipArchiveMode.Read)) {
					try {
						var entry = ar.GetEntry(dbfName);
						if(entry != null) {
							try {
								entry.ExtractToFile(GetDBFDirectory(dbfName), true);
								return true;
							} catch {
								throw;
								// TODO: handle exceptions
							}
						}
					} catch {
						throw;
						// TODO: handle exceptions
					}
				}
			}
			return false;
		}
		/// <summary>
		/// Creates a backup of the specified DBF in the recovery archive.
		/// <para>DBF's should be verified before this method is called, this method does not verify DBF's</para>
		/// </summary>
		/// <param name="dbfName">Name of the DBF.</param>
		private static void BackupVerifiedDBF(string dbfName, bool retry = false) {
			if(!File.Exists(_RecoveryArchive)) {
				ZipFile.CreateFromDirectory(Simulation.Directories.RepoFileDirectory, _RecoveryArchive);
			}
			try {
				using(var ar = ZipFile.Open(_RecoveryArchive, ZipArchiveMode.Update)) {
					try {
						ar.GetEntry(dbfName)?.Delete();
						ar.CreateEntryFromFile(GetDBFDirectory(dbfName), dbfName);
					} catch {
						throw;
						// TODO: handle exceptions
					}
					if(retry) {
						EventLog.WriteEntry("IOSOverlay", "The backup and recovery archive was successfully recreated.", EventLogEntryType.Information);
					}
				}
			} catch(System.IO.InvalidDataException e) {
				// Recovery Archive must have become corrupt
				File.Delete(_RecoveryArchive);

				if(!retry) {
					EventLog.WriteEntry("IOSOverlay", "The backup and recovery archive was corrupted.", EventLogEntryType.Warning);
					BackupVerifiedDBF(dbfName, true);
				} else {
					EventLog.WriteEntry("IOSOverlay", "The backup and recovery archive was corrupted and could not be recreated.", EventLogEntryType.Error);
				}
			}
		}
		private static void VerifySavedData() {
			if(VerifySavedDBF(C_DBF_SETTINGS, SimulatorSettingsModel.Create)) BackupVerifiedDBF(C_DBF_SETTINGS);
			if(VerifySavedDBFCollection(C_DBF_REPORTS, ReportModelCollection.Create)) BackupVerifiedDBF(C_DBF_REPORTS);
			if(VerifySavedDBFCollection(C_DBF_USERS, UserModelCollection.Create)) BackupVerifiedDBF(C_DBF_USERS);
			if(VerifySavedDBFCollection(C_DBF_GROUPS, GroupModelCollection.Create)) BackupVerifiedDBF(C_DBF_GROUPS);
			if(VerifySavedDBFCollection(C_DBF_ENVIRONMENTS, EnvironmentModelCollection.Create)) BackupVerifiedDBF(C_DBF_ENVIRONMENTS);
			if(VerifySavedDBFCollection(C_DBF_EXERCISES, ExerciseModelCollection.Create)) BackupVerifiedDBF(C_DBF_EXERCISES);
			if(VerifySavedDBFCollection(C_DBF_CRANEVARIABLES, CraneConfigModelCollection.Create)) BackupVerifiedDBF(C_DBF_CRANEVARIABLES);
			if(VerifySavedDBFCollection(C_DBF_ASSIGNMENTS, AssignmentModelCollection.Create)) BackupVerifiedDBF(C_DBF_ASSIGNMENTS);
			if(VerifySavedDBFCollection(C_DBF_GROUPASSIGNMENTS, GroupAssignmentModelCollection.Create)) BackupVerifiedDBF(C_DBF_GROUPASSIGNMENTS);
			if(VerifySavedDBFCollection(C_DBF_SCORINGSETS, ScoringDeductionsModelCollection.Create)) BackupVerifiedDBF(C_DBF_SCORINGSETS);
		}
		private static bool VerifySavedDBF<TModel>(string dbfName, Func<string, TModel> factory) where TModel : Model, new() {
			try {
				var collection = factory(GetDBFDirectory(dbfName));
				if(collection != null) return true;
			} catch {
				return false;
			}
			return false;
		}
		private static bool VerifySavedDBFCollection<TModelCollection>(string dbfName, Func<string, TModelCollection> factory) where TModelCollection : ModelCollection, new() {
			try {
				var collection = factory(GetDBFDirectory(dbfName));
				if(collection != null) return true;
			} catch {
				return false;
			}
			return false;
		}
		private static string GetDBFDirectory(string dbf) {
			return Simulation.Directories.RepoFileDirectory + dbf;
		}

		private static void SyncAndUpdateUIDs() {
			SettingInitialUIDs = true;
			UserCollection.UpdateModelUIDs();
			GroupCollection.UpdateModelUIDs();
			CraneConfigCollection.UpdateModelUIDs();
			EnvironmentCollection.UpdateModelUIDs();
			ScoringSetsCollection.UpdateModelUIDs();
			ExerciseCollection.UpdateModelUIDs();
			AssignmentCollection.UpdateModelUIDs();
			GroupAssignmentsCollection.UpdateModelUIDs();
			ReportCollection.UpdateModelUIDs();

			UserCollection.UpdateCreatorUIDs();
			GroupCollection.UpdateCreatorUIDs();
			CraneConfigCollection.UpdateCreatorUIDs();
			EnvironmentCollection.UpdateCreatorUIDs();
			ScoringSetsCollection.UpdateCreatorUIDs();
			ExerciseCollection.UpdateCreatorUIDs();
			AssignmentCollection.UpdateCreatorUIDs();
			GroupAssignmentsCollection.UpdateCreatorUIDs();
			ReportCollection.UpdateCreatorUIDs();

			UserCollection.UpdateUniqueReferencing();
			GroupCollection.UpdateUniqueReferencing();
			CraneConfigCollection.UpdateUniqueReferencing();
			EnvironmentCollection.UpdateUniqueReferencing();
			ScoringSetsCollection.UpdateUniqueReferencing();
			ExerciseCollection.UpdateUniqueReferencing();
			AssignmentCollection.UpdateUniqueReferencing();
			GroupAssignmentsCollection.UpdateUniqueReferencing();
			ReportCollection.UpdateUniqueReferencing();
			SettingInitialUIDs = false;
		}
	}
}
