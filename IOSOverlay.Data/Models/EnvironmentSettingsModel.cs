// TODO: move to DataModels.tt/Models.tt
using System;
using System.Runtime.Serialization;
using SharedData.Sync;
using Simulation;

namespace IOSOverlay.Data.Models {
	[KnownType(typeof(EnvironmentSettings))]
	public sealed partial class EnvironmentSettingsModel:Model<EnvironmentSettings> {
		[IgnoreDataMember]
		public EnvironmentFeatures SelectedFeatures {
			get {
				return View.SelectedFeatures;
			}
			set {
				if(EditMode) {
					if(View.SelectedFeatures != value) {
						View.SelectedFeatures = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal EnvironmentSettingsModel(string fileName) : base(fileName) {

		}
		public EnvironmentSettingsModel() : base() {
		}
		public override object Clone() {
			return new EnvironmentSettingsModel() { Master = this.Master };
		}
		internal static EnvironmentSettingsModel Create(string fileName) {
			return Deserialize<EnvironmentSettingsModel>(fileName);
		}
	}
}
