using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IOSOverlay.Common.Views {
	public class View:UserControl {
		public static bool DESIGN_MODE => ApplicationBridge.IsInDesignMode;

#if DEBUG
		public static bool DEBUG => true;
#else
		public static bool DEBUG => false;
#endif
		private static List<View> ViewFocusStack = new List<View>();
		public static List<View> ViewStrack = new List<View>();
		public static int _TOPLAYERNUM = 0;
		private Control _FocusTarget;
		private int _LayerOrder = -1;

		public static int TOPLAYERNUM {
			get { return _TOPLAYERNUM; }
			set { _TOPLAYERNUM = value; }
		}
		public Control FocusTarget {
			get { return _FocusTarget; }
			set {
				_FocusTarget = value;
				if(_FocusTarget != null) ViewFocusStack.Add(this);
			}
		}
		public int LayerOrder {
			get { return _LayerOrder; }
			set { _LayerOrder = value; }
		}

		static View() {
			// Call default static contructor
			RuntimeHelpers.RunClassConstructor((typeof(UserControl).TypeHandle));
		}
		public static void AddViewToStack(View v) {

		}
		public static void RemoveViewFromStack(View v) {

		}
		public static void Refocus() {
			if(ViewFocusStack.Count == 0) return;

			var v = ViewFocusStack.OrderByDescending((a) => a.LayerOrder).First();

			Keyboard.ClearFocus();
			FocusManager.SetFocusedElement(v, v.FocusTarget);
			Keyboard.Focus(v.FocusTarget);
		}
		public View() : base() {
			LayerOrder = TOPLAYERNUM++;

			this.Loaded += (a, b) => Refocus();
			this.Unloaded += (a, b) => Refocus();

			AddViewToStack(this);
		}
		public virtual void LeavingView() {
			ViewFocusStack.Remove(this);
			TOPLAYERNUM--;
			RemoveViewFromStack(this);
		}

		protected override void OnInitialized(EventArgs e) {
			base.OnInitialized(e);
		}

		protected override void OnMouseEnter(MouseEventArgs e) {
			base.OnMouseEnter(e);
		}
	}
}
