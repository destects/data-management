using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Common {
	using Views;
	using ViewModels;

	public class Page:IDisposable {
		private View _View;
		private ViewModel _Context;
		public int Layer;
		public readonly int Tag;
		public readonly BottomBarConfigurations[] BottomBarConfiguration;

		public View View {
			get { return _View; }
			private set { _View = value; }
		}
		public ViewModel Context {
			get { return _Context; }
			private set { _Context = value; }
		}

		public Page(int tag, View view, BottomBarConfigurations[] bottomBarConfiguration) {
			this.Tag = tag;
			this.View = view;
			this.Context = view.DataContext as ViewModel;
			this.BottomBarConfiguration = bottomBarConfiguration;
		}
		public Page(int tag, int layer, View view, BottomBarConfigurations[] bottomBarConfiguration) : this(tag, view, bottomBarConfiguration) {
			this.Layer = layer;
		}

		/// <summary>
		/// Leavings the page. Notifies this Page's View and Context that the 
		/// navigator is navigating away.
		/// </summary>
		public void LeavingPage() {
			View?.LeavingView();
			Context?.Leaving();
		}
		public void Dispose() {
			Context?.Left();

		}
	}
}
