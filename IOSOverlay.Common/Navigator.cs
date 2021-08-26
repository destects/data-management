using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOSOverlay.Common {
	using PageIndex = System.Int32;
	using ViewModels;
	using Views;
	using System.Windows;

	public class PageChangedEventArgs:EventArgs {
		public readonly Page CurrentPage;
		public readonly Page PreviousPage;

		public PageChangedEventArgs(Page newPage, Page oldPage) {
			CurrentPage = newPage;
			PreviousPage = oldPage;
		}
	}

	public static class Navigator {
		private static Stack<PageIndex> _NavigationLog = new Stack<PageIndex>();
		private static Page[] _PageLayers = new Page[2];
		private static bool _NavigateBack;
		private static EventHandler<PageChangedEventArgs> _MainPageChanged;
		private static EventHandler<PageChangedEventArgs> _MainPageChanging;
		private static EventHandler<PageChangedEventArgs> _SubPageChanged;
		public static Window MainWindowHandle;
		public static Action<string, string> ActivateAlertBox;
		public static Dictionary<PageIndex, object> Cookies = new Dictionary<PageIndex, object>();
		public static EventWaitHandle PageChanged = new EventWaitHandle(true, EventResetMode.AutoReset);

		[Obsolete("Use Layers")]
		public static PageIndex PageTag => _PageLayers[0]?.Tag ?? -1;
		[Obsolete("Use Layers")]
		public static PageIndex AltTag => _PageLayers[1]?.Tag ?? -1;
		[Obsolete("Use Layers")]
		public static ViewModel PageContext => _PageLayers[0]?.Context;
		[Obsolete("Use Layers")]
		public static ViewModel AltContext => _PageLayers[1]?.Context;
		[Obsolete("Use Layers")]
		public static View PageView => _PageLayers[0]?.View;
		[Obsolete("Use Layers")]
		public static View AltView => _PageLayers[1]?.View;


		public static Page[] Layers {
			get { return _PageLayers; }
		}
		public static Stack<PageIndex> NavigationLog => _NavigationLog;
		public static bool NavigateBack {
			get {
				var tmp = _NavigateBack;
				_NavigateBack = false;
				return tmp;
			}
			set {
				_NavigateBack = value;
			}
		}
		/// <summary>
		/// Occurs when [main page changed].
		/// </summary>
		public static event EventHandler<PageChangedEventArgs> MainPageChanged {
			add { _MainPageChanged += value; }
			remove { _MainPageChanged -= value; }
		}
		/// <summary>
		/// Occurs when [main page changing].
		/// </summary>
		public static event EventHandler<PageChangedEventArgs> MainPageChanging {
			add { _MainPageChanging += value; }
			remove { _MainPageChanging -= value; }
		}
		/// <summary>
		/// Occurs when [sub page changed].
		/// </summary>
		public static event EventHandler<PageChangedEventArgs> SubPageChanged {
			add { _SubPageChanged += value; }
			remove { _SubPageChanged -= value; }
		}

		public static void NavigateToPage(PageIndex page) {
			if(ApplicationBridge.GetInvokeRequired()) {
				ApplicationBridge.InvokeDispatcher(() => NavigateToPage(page));
				return;
			}

			if(!Navigator.NavigateBack && _PageLayers[0] != null) Navigator.NavigationLog.Push(_PageLayers[0].Tag);
			var old = _PageLayers[0];
			_MainPageChanging?.Invoke(null, new PageChangedEventArgs(_PageLayers[0], old));
			old?.LeavingPage();
			_PageLayers[0] = PageFactory.CreatePage(page, 0);
			old?.Dispose();
			PageChanged.Set();

			_MainPageChanged?.Invoke(null, new PageChangedEventArgs(_PageLayers[0], old));
		}
		public static void ShowAltContent(PageIndex page) {
			if(ApplicationBridge.GetInvokeRequired()) {
				ApplicationBridge.InvokeDispatcher(() => ShowAltContent(page));
				return;
			}
			var old = _PageLayers[0];
			_PageLayers[0]?.Context?.OnViewFocusLost();
			//old?.LeavingPage();
			_PageLayers[1] = PageFactory.CreatePage(page, 1);
			_PageLayers[1]?.Context?.OnViewFocusGained();
			//View.Refocus();

			_SubPageChanged?.Invoke(null, new PageChangedEventArgs(_PageLayers[1], old));
		}
		public static void ShowAltContent(PageTemplate template) {
			if(ApplicationBridge.GetInvokeRequired()) {
				ApplicationBridge.InvokeDispatcher(() => ShowAltContent(template));
				return;
			}
			var old = _PageLayers[0];
			_PageLayers[0]?.Context?.OnViewFocusLost();
			//old?.LeavingPage();
			_PageLayers[1] = PageFactory.CreatePage(template, 1);
			_PageLayers[1]?.Context?.OnViewFocusGained();
			//View.Refocus();

			_SubPageChanged?.Invoke(null, new PageChangedEventArgs(_PageLayers[1], old));
		}
		public static TPage GetPageContextAs<TPage>() where TPage : ViewModel {
			return _PageLayers[0]?.Context as TPage;
		}
		public static TPage GetAltContextAs<TPage>() where TPage : ViewModel {
			return _PageLayers[1]?.Context as TPage;
		}
		public static void CloseAltContent() {
			if(ApplicationBridge.GetInvokeRequired()) {
				ApplicationBridge.InvokeDispatcher(CloseAltContent);
				return;
			}
			_PageLayers[1]?.Context?.OnViewFocusLost();
			_PageLayers[1]?.View?.LeavingView();
			_PageLayers[1]?.LeavingPage();
			_PageLayers[1]?.Dispose();
			_PageLayers[1] = null;
			_PageLayers[0]?.Context?.OnViewFocusGained();

			_SubPageChanged?.Invoke(null, new PageChangedEventArgs(_PageLayers[0], _PageLayers[1]));
		}
		public static void NavigateToPreviousPage() {
			if(NavigationLog.Count > 0) {
				NavigateBack = true;
				NavigateToPage(NavigationLog.Pop());
			}
		}
		public static void ShowAlertBox(string message = "", string title = "Alert") {
			if(ApplicationBridge.GetInvokeRequired()) {
				ApplicationBridge.InvokeDispatcher(() => ShowAlertBox(message, title));
				return;
			}
			ActivateAlertBox?.Invoke(message, title);
		}
		public static void SetPageCookie(PageIndex pageTag, object value) {
			if(Cookies.ContainsKey(pageTag)) {
				Cookies[pageTag] = value;
			} else {
				Cookies.Add(pageTag, value);
			}
		}
		public static object ConsumeCurrentPageCookie(PageIndex pageTag) {
			object value = null;
			if(Cookies.ContainsKey(pageTag)) {
				value = Cookies[pageTag];
				Cookies.Remove(pageTag);
			}
			return value;
		}
		public static TCookie ConsumeCurrentPageCookie<TCookie>(PageIndex pageTag) where TCookie : class {
			TCookie value = null;
			if(Cookies.ContainsKey(pageTag)) {
				value = Cookies[pageTag] as TCookie;
				Cookies.Remove(pageTag);
			}
			return value;
		}
	}
}
