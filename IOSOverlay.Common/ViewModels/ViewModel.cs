using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IOSOverlay.Data;

namespace IOSOverlay.Common.ViewModels {
	[Serializable]
	public abstract class ViewModel:INotifyPropertyChanged, INotifyPropertyChanging {
#if DEBUG
		public static bool DEBUG => true;
#else
		public static bool DEBUG => false;
#endif
		private PropertyChangedEventHandler _PropertyChanged;
		private PropertyChangingEventHandler _PropertyChanging;

		public static ViewModel DataContextRelay {
			get { return Navigator.GetPageContextAs<ViewModel>(); }
		}
		public virtual string Title => string.Empty;
		public bool DESIGN_MODE => ApplicationBridge.IsInDesignMode;
		public Data.Models.SimulatorSettingsModel SimSettings => ModelManager.SimulatorSettings;
		public event PropertyChangedEventHandler PropertyChanged {
			add { _PropertyChanged += value; }
			remove { _PropertyChanged -= value; }
		}
		public event PropertyChangingEventHandler PropertyChanging {
			add { _PropertyChanging += value; }
			remove { _PropertyChanging -= value; }
		}

		public virtual void Reset() { }
		public virtual void Leaving() { }
		public virtual void Left() { }
		public virtual void OnViewFocusGained() { }
		public virtual void OnViewFocusLost() { }

		[Obsolete("Use RaisePropertyChanged")]
		public void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			RaisePropertyChanged(propertyName);
		}
		[Obsolete("Use RaisePropertyChanging")]
		public void NotifyPropertyChanging([CallerMemberName] string propertyName = "") {
			RaisePropertyChanging(propertyName);
		}
		public void RaisePropertyChanged([CallerMemberName] string propertyName = "") {
			if(ApplicationBridge.GetInvokeRequired?.Invoke() ?? false) {
				ApplicationBridge.InvokeDispatcher(() => RaisePropertyChanged(propertyName));
				return;
			}
			_PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public void RaisePropertyChanging([CallerMemberName] string propertyName = "") {
			if(ApplicationBridge.GetInvokeRequired?.Invoke() ?? false) {
				ApplicationBridge.InvokeDispatcher(() => RaisePropertyChanging(propertyName));
				return;
			}
			_PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
		}
	}
}
