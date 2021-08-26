using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IOSOverlay.Data.Models.IO {
	/// <summary>
	/// Utilities for model type referencing
	/// </summary>
	[Serializable]
	public class ModelTypeReference {
		/// <summary>
		/// Enumeration of Models
		/// </summary>
		[Serializable]
		public enum Types {
			/// <summary>
			/// The Model Type cannot be determined
			/// </summary>
			Unknown = -1,
			Report,
			AttemptData,
			User,
			Group,
			GroupAssignment,
			Assignment,
			Environment,
			CraneConfig,
			Exercise,
			SimulatorSettings,
			ScoringDeductions
		}
		/// <summary>
		/// Gets the strongly named type of the abstract model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static Types GetType(Model model) {
			if(model is ReportModel) return Types.Report;
			if(model is AttemptDataModel) return Types.AttemptData;
			if(model is UserModel) return Types.User;
			if(model is GroupModel) return Types.Group;
			if(model is GroupAssignmentModel) return Types.GroupAssignment;
			if(model is AssignmentModel) return Types.Assignment;
			if(model is EnvironmentModel) return Types.Environment;
			if(model is CraneConfigModel) return Types.CraneConfig;
			if(model is ExerciseModel) return Types.Exercise;
			if(model is SimulatorSettingsModel) return Types.SimulatorSettings;
			if(model is ScoringDeductionsModel) return Types.ScoringDeductions;
			return Types.Unknown;
		}
	}
}
