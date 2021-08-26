using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFExtension.Basic;
using System.Windows.Forms;
using System.Windows;

namespace IOSOverlay.Data.Models {
	public class SelectableModelWrapper:INotifyPropertyChanged {
		private bool _IsSelected;
		private object _Visual;
		private Model _Model;

		public event PropertyChangedEventHandler PropertyChanged;

		public bool IsSelected {
			get { return _IsSelected; }
			set {
				_IsSelected = value;
				RaisePropertyChanged(nameof(IsSelected));
			}
		}
		public virtual Model Model {
			get { return this._Model; }
			set {
				this._Model = value;
				RaisePropertyChanged(nameof(Model));
			}
		}
		public object Visual {
			get { return _Visual; }
			set {
				this._Visual = value;
				RaisePropertyChanged(nameof(Visual));
			}
		}
		public bool ValidPackageTag => Model.PackageReference.IsValid;
		public string ToolTip => ValidPackageTag ? $"Member of Package {Model.PackageReference.PackageName} @ v{Model.PackageReference.PackageVersion}" : "Not a packaged member";

		public ICommand CopyGUIDToClipboard => new RelayCommand(() => {
			Clipboard.SetText(_Model.UID.ToString());
			Debug.WriteLine("Model GUID copied to clipboard.");
		});

		public void RaisePropertyChanged(string name) {
			if(PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}
	}
}
