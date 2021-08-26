// TODO: move to DataModels.tt/Models.tt
using System;
using System.Runtime.Serialization;
using Simulation;

namespace IOSOverlay.Data.Models {
	[KnownType(typeof(OperatorAids))]
	public class OperatorAidsModel:Model<OperatorAids> {
		[IgnoreDataMember]
		public bool EnableCableLengthMarkings {
			get {
				return View.EnableCableLengthMarkings;
			}
			set {
				if(EditMode) {
					if(View.EnableCableLengthMarkings != value) {
						View.EnableCableLengthMarkings = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public bool EnableHoistDotProjection {
			get {
				return View.EnableHoistDotProjection;
			}
			set {
				if(EditMode) {
					if(View.EnableHoistDotProjection != value) {
						View.EnableHoistDotProjection = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public bool EnableHookblockDotProjection {
			get {
				return View.EnableHookblockDotProjection;
			}
			set {
				if(EditMode) {
					if(View.EnableHookblockDotProjection != value) {
						View.EnableHookblockDotProjection = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public bool EnableHookGroundDistanceTicker {
			get {
				return View.EnableHookGroundDistanceTicker;
			}
			set {
				if(EditMode) {
					if(View.EnableHookGroundDistanceTicker != value) {
						View.EnableHookGroundDistanceTicker = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}
		[IgnoreDataMember]
		public bool EnableULDObstructionIndicator {
			get {
				return View.EnableULDObstructionIndicator;
			}
			set {
				if(EditMode) {
					if(View.EnableULDObstructionIndicator != value) {
						View.EnableULDObstructionIndicator = value;
						IsChanged = true;
						RaisePropertyChanged();
					}
				}
			}
		}

		internal OperatorAidsModel(string fileName) : base(fileName) {

		}
		public OperatorAidsModel() : base() {
		}
		public override object Clone() {
			return new OperatorAidsModel() { Master = this.Master };
		}
		internal static OperatorAidsModel Create(string fileName) {
			return Deserialize<OperatorAidsModel>(fileName);
		}
	}
}
