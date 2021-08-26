using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulation;
using Extensions;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Diagnostics;
using Microsoft.Win32;

namespace IOSOverlay.Data.Models.IO {
	public static class PackageManager {
		private class ProcessPackage {
			public FilePackageInfo PackInfo;
			public Guid PackGUIDInPackList;
			public int PackVersionInPackList;

			public bool PackReferenceExistsInPackList;
			public bool PackFileExists;
			public bool PackIsNewerThanPackListVersion;
			public bool PackRequiresProcessing;
			public bool PackHasBeenProcessed;
		}

		private const string _PACKEXTENSION = "*.pack";
		private static PackageList _PackList;
		private static List<FilePackageInfo> _PackFiles = new List<FilePackageInfo>();
		private static List<ProcessPackage> _PackagesToProcess = new List<ProcessPackage>();

		private static string _LocalPackagePath => $"{AppDomain.CurrentDomain.BaseDirectory}\\DataPacks\\";
		private static string _PackageListFile => $"{Directories.PackagesDirectory}\\PackageList.xml";

		public static void RunPackageUpdates() {
			OpenPackageList();

			// add packlist packages to process list
			foreach(var p in _PackList.Packages) {
				_PackagesToProcess.Add(new ProcessPackage() {
					PackGUIDInPackList = p.PackageID,
					PackVersionInPackList = p.PackageVersion,
					PackReferenceExistsInPackList = true,
					PackRequiresProcessing = true
				});
			}

			UnpackDefault();
			ScanForPackages();

			foreach(var p in _PackFiles) {
				var match = (from a in _PackagesToProcess where a.PackGUIDInPackList == p.PackInfo.PackageID select a).FirstOrDefault();
				if(match != null) {
					match.PackFileExists = true;
					match.PackRequiresProcessing = false;
					match.PackInfo = p;

					if(match.PackInfo.PackInfo.PackageVersion > match.PackVersionInPackList) {
						match.PackIsNewerThanPackListVersion = true;
						match.PackRequiresProcessing = true;
					}
				} else {
					_PackagesToProcess.Add(new ProcessPackage() {
						PackInfo = p,
						PackFileExists = true,
						PackRequiresProcessing = true
					});
				}
			}

			//ProcessPacks();
			EvaluatePackagesToProcess();
			SavePackageList();
		}
		public static bool RunDefaultsCheck() {
			OpenPackageList();
			UnpackDefault();
			SavePackageList();
			return false;
		}
		/// <summary>
		/// Unpacks the default data package if it's not already unpacked
		/// </summary>
		private static void UnpackDefault() {
			FilePackageInfo fpi;
			ProcessPackage ptp = new ProcessPackage();
			// if the packages directory doesn't exist, create it.
			// Why aren't we just using Directories.CheckCreateMissingDirectories ?
			if(!Directory.Exists(Simulation.Directories.PackagesDirectory)) {
				Directory.CreateDirectory(Simulation.Directories.PackagesDirectory);
			}
			// if the default data pack doesn't exist, create it
			if(!File.Exists($"{Simulation.Directories.PackagesDirectory}\\DefaultData.pack")) {
				File.WriteAllBytes($"{Simulation.Directories.PackagesDirectory}\\DefaultData.pack", Properties.Resources.DefaultData);
			} else {
				ptp.PackFileExists = true;

				// check if internal package is newer than the one saved.
				var d = Path.GetTempFileName();
				// fill temp file with internal default data pack
				File.WriteAllBytes(d, Properties.Resources.DefaultData);
				// get internal data package info
				var a = GetPackageInfo(d);
				ptp.PackGUIDInPackList = a.PackInfo.PackageID;
				ptp.PackReferenceExistsInPackList = true;
				ptp.PackVersionInPackList = a.PackInfo.PackageVersion;
				// get external data package info
				var b = GetPackageInfo($"{Simulation.Directories.PackagesDirectory}\\DefaultData.pack");
				a.File = b.File;
				ptp.PackInfo = a;
				// overwrite external data package if internal is newer
				if(a.PackInfo.PackageVersion > b.PackInfo.PackageVersion) {
					File.WriteAllBytes($"{Simulation.Directories.PackagesDirectory}\\DefaultData.pack", Properties.Resources.DefaultData);
					ptp.PackIsNewerThanPackListVersion = true;
					ptp.PackRequiresProcessing = true;
				}
			}

			fpi = GetPackageInfo($"{Simulation.Directories.PackagesDirectory}\\DefaultData.pack");
			//ImportPackage(DataContractSerialization.DeserializeContract<ModelPackage>(fpi.File));
			//ProcessPack(fpi);
			if(ptp.PackRequiresProcessing) {
				Console.WriteLine(ptp.PackInfo.File);
				UpdatePackageByProcess(ptp);


				// update version number in package list to the new version number
				var p = _PackList[ptp.PackGUIDInPackList];
				p.PackageVersion = ptp.PackInfo.PackInfo.PackageVersion;
				_PackList[ptp.PackGUIDInPackList] = p;

				SavePackageList();
			}
		}
		/// <summary>
		/// Opens the package list.
		/// </summary>
		private static void OpenPackageList() {
			if(System.IO.File.Exists(_PackageListFile)) {
				_PackList = DataContractSerialization.DeserializeContract<PackageList>(_PackageListFile);
			} else {
				_PackList = new PackageList();
			}
		}
		/// <summary>
		/// Scans for packages.
		/// </summary>
		private static void ScanForPackages() {
			//foreach(var p in Directory.EnumerateFiles(_LocalPackagePath, _PACKEXTENSION)) {
			//	_PackFiles.Add(ProcessPackFileFound(p));
			//}
			foreach(var p in Directory.EnumerateFiles(Simulation.Directories.PackagesDirectory, _PACKEXTENSION)) {
				_PackFiles.Add(GetPackageInfo(p));
			}
		}
		/// <summary>
		/// Gets the package information for the file
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		private static FilePackageInfo GetPackageInfo(string file) {
			XElement x = XElement.Load(file);
			var ns = x.Name.Namespace;
			x = x.Element(ns.GetName("Info"));
			//_PackList.Contains()
			return new FilePackageInfo() {
				PackInfo = new PackageInfo() {
					PackageName = x.Element(ns.GetName("PackageName")).Value,
					PackageID = Guid.Parse(x.Element(ns.GetName("PackageID")).Value),
					PackageVersion = int.Parse(x.Element(ns.GetName("PackageVersion")).Value)
				},
				File = file
			};
		}
		/// <summary>
		/// Processes the packages in <see cref="_PackFiles"/>, then clears _PackFiles
		/// </summary>
		private static void ProcessPacks() {
			foreach(var pack in _PackFiles) {
				ProcessPack(pack);
			}
			_PackFiles.Clear();
		}
		/// <summary>
		/// Processes the pack.
		/// </summary>
		/// <param name="packInfo">The pack.</param>
		private static void ProcessPack(FilePackageInfo packInfo) {
			// check if the package has been imported before
			if(_PackList.Contains(packInfo.PackInfo.PackageID)) {
				// is the PackageVersion of the pack newer than what's on file?
				if(_PackList[packInfo.PackInfo.PackageID].PackageVersion < packInfo.PackInfo.PackageVersion) {
					// mark the package as requiring further processing
					packInfo.Process = true;

					// update version number in package list to the new version number
					var p = _PackList[packInfo.PackInfo.PackageID];
					p.PackageVersion = packInfo.PackInfo.PackageVersion;
					_PackList[packInfo.PackInfo.PackageID] = p;
				}
			} else {
				// pack is a package that has never been imported
				// mark it for further processing and add it to the package list
				packInfo.Process = true;
				_PackList.AddNew(packInfo.PackInfo);
			}

			// is pack marked for further processing? if so, import time.
			// Deserialize the full package, and pass it to the import function
			if(packInfo.Process) ImportPackage(DataContractSerialization.DeserializeContract<ModelPackage>(packInfo.File));
		}
		/// <summary>
		/// Imports the package.
		/// </summary>
		/// <param name="pack">The pack.</param>
		private static void ImportPackage(ModelPackage pack) {
			// Cycle through each dataset in the ModelPackage
			foreach(var d in pack.DataSets) {
				Type t = null;
				// Import the DataSet based on it's type property
				switch(d.DataType) {
					case ModelTypeReference.Types.Assignment:
						ModelManager.AssignmentCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(AssignmentModel), d.Data), pack.Info.GetPackageTag());
						break;
					case ModelTypeReference.Types.AttemptData:
						t = typeof(EnvironmentModel);
						break;
					case ModelTypeReference.Types.Environment:
						ModelManager.EnvironmentCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(EnvironmentModel), d.Data), pack.Info.GetPackageTag());
						break;
					case ModelTypeReference.Types.Exercise:
						ModelManager.ExerciseCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(ExerciseModel), d.Data), pack.Info.GetPackageTag());
						break;
					case ModelTypeReference.Types.Group:
						ModelManager.GroupCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(GroupModel), d.Data), pack.Info.GetPackageTag());
						break;
					case ModelTypeReference.Types.GroupAssignment:
						ModelManager.GroupAssignmentsCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(GroupAssignmentModel), d.Data), pack.Info.GetPackageTag());
						break;
					case ModelTypeReference.Types.Report:
						ModelManager.ReportCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(ReportModel), d.Data), pack.Info.GetPackageTag());
						break;
					case ModelTypeReference.Types.ScoringDeductions:
						ModelManager.ScoringSetsCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(ScoringDeductionsModel), d.Data), pack.Info.GetPackageTag());
						break;
					case ModelTypeReference.Types.SimulatorSettings:
						t = typeof(SimulatorSettingsModel);
						break;
					case ModelTypeReference.Types.User:
						t = typeof(UserModel);
						ModelManager.UserCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(UserModel), d.Data), pack.Info.GetPackageTag());
						break;
					case ModelTypeReference.Types.CraneConfig:
						ModelManager.CraneConfigCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(CraneConfigModel), d.Data), pack.Info.GetPackageTag());
						break;
				}
			}
		}
		private static void SavePackageList() {
			if(_PackList != null) {
				DataContractSerialization.SerializeContractToFile<PackageList>(_PackList, _PackageListFile, plainText: true, tagged: false);
			}
			_PackList = null;
		}


		public static bool ExportModelPackage(ModelPackage pack) {
			var sfd = new SaveFileDialog();
			sfd.AddExtension = true;
			sfd.CheckFileExists = false;
			sfd.CheckPathExists = true;
			sfd.OverwritePrompt = true;
			sfd.ValidateNames = true;
			//sfd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
			sfd.FileName = pack.Info.PackageName;
			sfd.DefaultExt = ".pack";
			sfd.Filter = "Model Package |*.pack";
			if(sfd.ShowDialog() ?? false) {
				pack.Save(sfd.FileName);
				return true;
			}
			return false;
		}
		public static bool ImportModelPackage() {
			var ofd = new OpenFileDialog();
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.Multiselect = true;
			//ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
			ofd.FileName = "Package.pack";
			ofd.DefaultExt = ".pack";
			ofd.Filter = "Model Package |*.pack";

			if(ofd.ShowDialog() ?? false) {
				OpenPackageList();
				for(int i = 0; i < ofd.FileNames.Length; i++) {
					CompareCopyImportPackage(new FileInfo(ofd.FileNames[i]));
				}
				Model.BeginInit();
				ModelCollection.BeginInit();
				ProcessPacks();
				ModelCollection.EndInit();
				Model.EndInit();
				return true;
			}
			return false;
		}
		public static ModelPackage OpenPackage() {
			var ofd = new OpenFileDialog();
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.Multiselect = false;
			//ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
			ofd.FileName = "Package.pack";
			ofd.DefaultExt = ".pack";
			ofd.Filter = "Model Package |*.pack";

			if(ofd.ShowDialog() ?? false) {
				OpenPackageList();
				var fpi = GetPackageInfo(ofd.FileName);
				if(fpi != null) {
					return DataContractSerialization.DeserializeContract<ModelPackage>(fpi.File);
				}
			}
			return null;
		}
		public static void UpdatePackage() {

		}
		/// <summary>
		/// Attempts to copy a package that's being imported into the packages directory. Comparing names and package ID's to ensure that
		/// if a package with same id exists, it is replaced. And that if a package with the same name, but a different ID exists, an indexed
		/// file name is generated to ensure there are no conflicts.
		/// </summary>
		/// <param name="file">The file.</param>
		private static void CompareCopyImportPackage(FileInfo file) {
			File.Copy(file.FullName, $"{Directories.PackagesDirectory}\\{file.Name}", true);
			_PackFiles.Add(GetPackageInfo($"{Directories.PackagesDirectory}\\{file.Name}"));
		}


		#region NEW WAY
		private static void EvaluatePackagesToProcess() {
			while(_PackagesToProcess.Any(a => a.PackRequiresProcessing)) {
				var ptp = _PackagesToProcess.First(a => a.PackRequiresProcessing);
				// package update
				if(ptp.PackReferenceExistsInPackList && ptp.PackFileExists) {
					if(ptp.PackIsNewerThanPackListVersion) {
						UpdatePackageByProcess(ptp);
					}
				}
				// new package
				if(ptp.PackFileExists && !ptp.PackReferenceExistsInPackList) {
					ImportNewPackageByProcess(ptp);
				}
				// package removal
				if(ptp.PackReferenceExistsInPackList && !ptp.PackFileExists) {
					RemovePackageByProcess(ptp);
				}
			}
		}
		/// <summary>
		/// Updates the package by process.
		/// This occurs when the package is both registered in the package list, exists on disk, and there's a difference in versions
		/// </summary>
		/// <param name="ptp">The PTP.</param>
		private static void UpdatePackageByProcess(ProcessPackage ptp) {
			if(!ptp.PackHasBeenProcessed) {
				var data = DataContractSerialization.DeserializeContract<ModelPackage>(ptp.PackInfo.File);
				var tag = data.Info.GetPackageTag();

				UpdatePackageByProcess_Helper(ModelManager.AssignmentCollection, data, tag, typeof(AssignmentModel), ModelTypeReference.Types.Assignment);
				UpdatePackageByProcess_Helper(ModelManager.EnvironmentCollection, data, tag, typeof(EnvironmentModel), ModelTypeReference.Types.Environment);
				UpdatePackageByProcess_Helper(ModelManager.ExerciseCollection, data, tag, typeof(ExerciseModel), ModelTypeReference.Types.Exercise);
				UpdatePackageByProcess_Helper(ModelManager.GroupCollection, data, tag, typeof(GroupModel), ModelTypeReference.Types.Group);
				UpdatePackageByProcess_Helper(ModelManager.GroupAssignmentsCollection, data, tag, typeof(GroupAssignmentModel), ModelTypeReference.Types.GroupAssignment);
				UpdatePackageByProcess_Helper(ModelManager.ReportCollection, data, tag, typeof(ReportModel), ModelTypeReference.Types.Report);
				UpdatePackageByProcess_Helper(ModelManager.ScoringSetsCollection, data, tag, typeof(ScoringDeductionsModel), ModelTypeReference.Types.ScoringDeductions);
				UpdatePackageByProcess_Helper(ModelManager.UserCollection, data, tag, typeof(UserModel), ModelTypeReference.Types.User);
				UpdatePackageByProcess_Helper(ModelManager.CraneConfigCollection, data, tag, typeof(CraneConfigModel), ModelTypeReference.Types.CraneConfig);

				ptp.PackHasBeenProcessed = true;
			} else {
				ptp.PackRequiresProcessing = false;
			}
		}
		private static void UpdatePackageByProcess_Helper(ModelCollection collection, ModelPackage mp, PackageTag pt, Type modelType, ModelTypeReference.Types dataType) {
			var UIDsOfModelsWithTag = collection.EnumeratePackageMatches(pt);
			var UIDsOfModelsInPack = from a in mp.DataSets where a.DataType == dataType select a.ModelUID;

			// these are the UIDs of the models that exist in the repository and came from this package, but don't exist in the package anymore.
			// they should be removed from the repository
			// have to convert to list because otherwise optimization will evaluate the query as were running the foreach loop
			var UIDsInRepoButNotPack = UIDsOfModelsWithTag.Except(UIDsOfModelsInPack).ToList();
			// these are the UIDs of the models that exist in the package but no model in the repository with a matching package tag had the same UID
			// these should be added to the repository
			var UIDsInPackButNotRepo = UIDsOfModelsInPack.Except(UIDsOfModelsWithTag);
			// these are the UIDs of the models that exist in the package and in the repository. The tags matched and the UIDs matched up
			// These should be updated in the repository
			var UIDsInRepoAndInPack = from a in UIDsOfModelsWithTag join b in UIDsOfModelsInPack on a equals b select a;

			// remove models removed
			foreach(var uid in UIDsInRepoButNotPack) {
				collection.RemoveUID(uid, false);
			}

			// add new 
			foreach(var dm in (from a in mp.DataSets join b in UIDsInPackButNotRepo on a.ModelUID equals b select a.Data)) {
				collection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(modelType, dm), pt);
			}

			// update existing
			foreach(var dm in (from a in mp.DataSets join b in UIDsInRepoAndInPack on a.ModelUID equals b select a.Data)) {
				collection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(modelType, dm), pt);
			}
		}
		/// <summary>
		/// Imports the new package by process.
		/// This occurs when the package is not registered in the package list but exists on disk
		/// </summary>
		/// <param name="ptp">The PTP.</param>
		private static void ImportNewPackageByProcess(ProcessPackage ptp) {
			if(!ptp.PackHasBeenProcessed) {
				// register the package with the packlist
				_PackList.AddNew(ptp.PackInfo.PackInfo);

				var data = DataContractSerialization.DeserializeContract<ModelPackage>(ptp.PackInfo.File);
				// Cycle through each dataset in the ModelPackage
				foreach(var d in data.DataSets) {
					// Import the DataSet based on it's type property
					switch(d.DataType) {
						case ModelTypeReference.Types.Assignment: ModelManager.AssignmentCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(AssignmentModel), d.Data), data.Info.GetPackageTag()); break;
						case ModelTypeReference.Types.Environment: ModelManager.EnvironmentCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(EnvironmentModel), d.Data), data.Info.GetPackageTag()); break;
						case ModelTypeReference.Types.Exercise: ModelManager.ExerciseCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(ExerciseModel), d.Data), data.Info.GetPackageTag()); break;
						case ModelTypeReference.Types.Group: ModelManager.GroupCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(GroupModel), d.Data), data.Info.GetPackageTag()); break;
						case ModelTypeReference.Types.GroupAssignment: ModelManager.GroupAssignmentsCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(GroupAssignmentModel), d.Data), data.Info.GetPackageTag()); break;
						case ModelTypeReference.Types.Report: ModelManager.ReportCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(ReportModel), d.Data), data.Info.GetPackageTag()); break;
						case ModelTypeReference.Types.ScoringDeductions: ModelManager.ScoringSetsCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(ScoringDeductionsModel), d.Data), data.Info.GetPackageTag()); break;
						case ModelTypeReference.Types.AttemptData: break;
						case ModelTypeReference.Types.SimulatorSettings: break;
						case ModelTypeReference.Types.User: ModelManager.UserCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(UserModel), d.Data), data.Info.GetPackageTag()); break;
						case ModelTypeReference.Types.CraneConfig: ModelManager.CraneConfigCollection.UpdateAddModel((Model)DataContractSerialization.DeserializeContract(typeof(CraneConfigModel), d.Data), data.Info.GetPackageTag()); break;
					}
				}

				data = null;

				ptp.PackHasBeenProcessed = true;
			} else {
				ptp.PackRequiresProcessing = false;
			}
		}
		/// <summary>
		/// Removes the package by process.
		/// This occurs when the package is registered in the package list, but does not exist on disk
		/// </summary>
		/// <param name="ptp">The PTP.</param>
		private static void RemovePackageByProcess(ProcessPackage ptp) {
			if(!ptp.PackHasBeenProcessed) {
				var tag = new PackageTag(string.Empty, ptp.PackGUIDInPackList, ptp.PackVersionInPackList);

				RemovePackageByProcess_Helper(ModelManager.AssignmentCollection, tag, typeof(AssignmentModel));
				RemovePackageByProcess_Helper(ModelManager.EnvironmentCollection, tag, typeof(EnvironmentModel));
				RemovePackageByProcess_Helper(ModelManager.ExerciseCollection, tag, typeof(ExerciseModel));
				RemovePackageByProcess_Helper(ModelManager.GroupCollection, tag, typeof(GroupModel));
				RemovePackageByProcess_Helper(ModelManager.GroupAssignmentsCollection, tag, typeof(GroupAssignmentModel));
				RemovePackageByProcess_Helper(ModelManager.ReportCollection, tag, typeof(ReportModel));
				RemovePackageByProcess_Helper(ModelManager.ScoringSetsCollection, tag, typeof(ScoringDeductionsModel));
				RemovePackageByProcess_Helper(ModelManager.UserCollection, tag, typeof(UserModel));
				RemovePackageByProcess_Helper(ModelManager.CraneConfigCollection, tag, typeof(CraneConfigModel));

				if(!_PackList.Remove(ptp.PackGUIDInPackList)) {
					throw new Exception("Failed to remove package");
				}

				ptp.PackHasBeenProcessed = true;
			} else {
				ptp.PackRequiresProcessing = false;
			}
		}
		private static void RemovePackageByProcess_Helper(ModelCollection collection, PackageTag pt, Type modelType) {
			// have to convert to list because otherwise optimization will evaluate the query as were running the foreach loop
			var UIDsOfModelsWithTag = collection.EnumeratePackageMatches(pt).ToList();

			foreach(var uid in UIDsOfModelsWithTag) {
				collection.RemoveUID(uid, false);
			}
		}
		#endregion
	}
}
