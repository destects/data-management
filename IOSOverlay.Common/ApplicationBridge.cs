using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Common {
	public static class ApplicationBridge {
		public static Func<bool> GetInvokeRequired;
		public static Action<Action> InvokeDispatcher;
		public static Func<bool> MainWindowExists;
		public static Func<bool> RunningInDesignMode;
		public static Action HideOverlay;
		public static Action ShowOverlay;
		public static Action AppExitNoShutdown;
		// These static fields for Set and Get DemoMode of the application bridge are not the permenant solution
		// they exist while application settings remain inaccessible to other parts of the
		// application.
		public static Action<bool> SetDemoModeEnabled;
		public static Func<bool> GetDemoModeEnabled;

		public static bool InvokeRequired => GetInvokeRequired();
		public static bool IsInDesignMode {
			get {
				if(RunningInDesignMode != null) {
					return RunningInDesignMode();
				}
				return true;
			}
		}
	}
}
