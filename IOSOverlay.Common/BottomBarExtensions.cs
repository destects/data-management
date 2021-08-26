using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace IOSOverlay.Common {
	public class BottomBarExtensions {
		/// <summary>
		/// Buffer for setting bottom bar buttons when the proxy hasn't been defined
		/// </summary>
		private static Queue<BBO[]> _SettingsBuffer = new Queue<BBO[]>();
		private static Action<BBO[]> _Proxy_AddOption;

		public static Action<BBO[]> Proxy_AddOption {
			get { return _Proxy_AddOption; }
			set {
				if(_Proxy_AddOption == null) {
					_Proxy_AddOption = value;
					ExecuteQueue();
				} else {
					_Proxy_AddOption = value;
				}
			}
		}

		/// <summary>
		/// Adds the BBO option(s) to the bottom bar.
		/// </summary>
		/// <param name="options">The options.</param>
		public static void AddOption(params BBO[] options) {
			if(Proxy_AddOption != null) {
				Console.WriteLine("Proxy defined");
				Proxy_AddOption.Invoke(options);
			} else {
				Console.WriteLine("Proxy not defined");
				_SettingsBuffer.Enqueue(options);
			}
		}

		/// <summary>
		/// Executes the queue.
		/// </summary>
		private static void ExecuteQueue() {
			while(_SettingsBuffer != null && _SettingsBuffer.Count > 0) {
				var s = _SettingsBuffer.Dequeue();
				if(_Proxy_AddOption != null) _Proxy_AddOption.Invoke(s);
			}
		}
	}
}
