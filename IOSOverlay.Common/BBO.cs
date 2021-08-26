using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IOSOverlay.Data;
using WPFExtension.Basic;

namespace IOSOverlay.Common {
	public class BBO:INotifyPropertyChanged {
		public static class Predefined {
			public static readonly BBO Loading = new BBO("Show Loader", "Show Loader rotation", new RelayCommand(() => Navigator.ShowAltContent((int)KnownPagesIndex.LoadingScreen)));
			public static readonly BBO Demo = new BBO("Demo", "Demo", new RelayCommand(() => Navigator.NavigateToPage((int)KnownPagesIndex.Demo), () => true));
			public static readonly BBO Help = new BBO("Help", "Help", new RelayCommand(() => Navigator.NavigateToPage((int)KnownPagesIndex.Help), () => ModelManager.SimulatorSettings != null && ModelManager.SimulatorSettings.EnableHelpMenu));
			public static readonly BBO Admin = new BBO("Management", "Management", new RelayCommand(() => Navigator.ShowAltContent((int)KnownPagesIndex.AdminLogin)), new Uri("pack://application:,,,/IOSOverlay.Common;Component/Resources/Icons/Icon_Management.png"));
			public static readonly BBO MainMenu = new BBO("Main Menu", "Main Menu", new RelayCommand(() => Navigator.NavigateToPage((int)KnownPagesIndex.MainMenu), () => AccountManager.PrimaryUserSet));
			public static readonly BBO Login = new BBO("Login", "Login", new RelayCommand(() => Navigator.NavigateToPage((int)KnownPagesIndex.Login), () => true), new Uri("pack://application:,,,/IOSOverlay.Common;Component/Resources/Icons/Icon_Login.png"));
			public static readonly BBO DirectShutdown = new BBO("Shutdown", "Shutdown", new RelayCommand(() => {
				System.Windows.Application.Current.Shutdown();
			}));
			public static readonly BBO ExitApplication = new BBO("Exit To Windows", "Exit Application", new RelayCommand(() => ApplicationBridge.AppExitNoShutdown()), new Uri("pack://application:,,,/IOSOverlay.Common;Component/Resources/Icons/Icon_Exit.png"));
			//public static readonly BBO Shutdown = new BBO("","", null),
			public static readonly BBO ExitOrShutdown = new BBO(
				((ModelManager.SimulatorSettings?.EnableShutdown ?? false) ? "Shutdown" : "Exit"),
				((ModelManager.SimulatorSettings?.EnableShutdown ?? false) ? "Shutdown" : "Exit"),
				new RelayCommand(() => System.Windows.Application.Current?.Shutdown()),
				ModelManager.SimulatorSettings,
				(PropertyChangedEventArgs e, BBO @this) => {
					if(@this != null) {
						@this.Text = ((ModelManager.SimulatorSettings?.EnableShutdown ?? false) ? "Shutdown" : "Exit");
						@this.ToolTip = ((ModelManager.SimulatorSettings?.EnableShutdown ?? false) ? "Shutdown" : "Exit");
					}
				}, new Uri("pack://application:,,,/IOSOverlay.Common;Component/Resources/Icons/Icon_Exit.png"));
			public static readonly BBO Back = new BBO("Back", "Back", new RelayCommand(
				() => {
					Navigator.NavigateToPreviousPage();
				}, () => {
					if(Navigator.NavigationLog != null && Navigator.NavigationLog.Count > 0) {
						if(Navigator.NavigationLog.Peek() == (int)KnownPagesIndex.None) {
							Navigator.NavigationLog.Clear();
						}
					}
					return Navigator.NavigationLog != null && Navigator.NavigationLog.Count > 0;
				}), new Uri("pack://application:,,,/IOSOverlay.Common;Component/Resources/Icons/Icon_Back.png"));

			public static BBO Get(BottomBarConfigurations bbc) {
				switch(bbc) {
					case BottomBarConfigurations.AdminButton: return BBO.Predefined.Admin;
					case BottomBarConfigurations.BackButton: return BBO.Predefined.Back;
					case BottomBarConfigurations.DemoButton: return BBO.Predefined.Demo;
					case BottomBarConfigurations.ExitAppButton: return BBO.Predefined.ExitApplication;
					case BottomBarConfigurations.HelpButton: return BBO.Predefined.Help;
					case BottomBarConfigurations.LoginButton: return BBO.Predefined.Login;
					case BottomBarConfigurations.MainMenuButton: return BBO.Predefined.MainMenu;
					case BottomBarConfigurations.ExitOrShutdownButton: return BBO.Predefined.ExitOrShutdown;
					case BottomBarConfigurations.DirectShutdownButton: return BBO.Predefined.DirectShutdown;
				}
				return null;
			}
		}

		private ICommand _Execute;
		private INotifyPropertyChanged _Updater;
		private Action<PropertyChangedEventArgs, BBO> _UpdateAction;
		private string _Text;
		private string _ToolTip;
		private Uri _Icon;
		public event PropertyChangedEventHandler PropertyChanged;

		public ICommand Execute {
			get {
				return _Execute;
			}
			set {
				_Execute = value;
				RaisePropertyChanged();
			}
		}
		public string Text {
			get {
				return _Text;
			}
			set {
				_Text = value;
				RaisePropertyChanged();
			}
		}
		public string ToolTip {
			get {
				return _ToolTip;
			}
			set {
				_ToolTip = value;
				RaisePropertyChanged();
			}
		}
		public Uri Icon {
			get {
				return _Icon;
			}
			set {
				_Icon = value;
				RaisePropertyChanged();
				RaisePropertyChanged("HasIcon");
			}
		}
		public bool HasIcon => _Icon != null;

		public BBO(string text, string toolTip, ICommand execute) {
			this.Text = text;
			this.Execute = execute;
			this.ToolTip = toolTip;

			RaisePropertyChanged("HasIcon");
		}
		public BBO(string text, string toolTip, ICommand execute, Uri icon) : this(text, toolTip, execute) {
			this.Icon = icon;
		}
		public BBO(string text, string toolTip, ICommand execute, INotifyPropertyChanged updater, Action<PropertyChangedEventArgs, BBO> updateCallback) {
			this.Text = text;
			this.Execute = execute;
			this.ToolTip = toolTip;
			this._Updater = updater;
			this._UpdateAction = updateCallback;

			if(_Updater != null) {
				_Updater.PropertyChanged += (s, e) => updateCallback(e, this);
			}

			RaisePropertyChanged("HasIcon");
		}
		public BBO(string text, string toolTip, ICommand execute, INotifyPropertyChanged updater, Action<PropertyChangedEventArgs, BBO> updateCallback, Uri icon) : this(text, toolTip, execute, updater, updateCallback) {
			this.Icon = icon;
		}

		private void _Updater_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			throw new NotImplementedException();
		}

		private void RaisePropertyChanged([CallerMemberName] string propertyName = "") {
			if(PropertyChanged != null) {
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
