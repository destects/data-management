using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Data {
	public delegate void ActiveSelectionChangedEventHandler(object sender, ActiveSelectionChangedEventArgs e);

	public class ActiveSelectionChangedEventArgs:EventArgs {
		public static new readonly ActiveSelectionChangedEventArgs Empty = new ActiveSelectionChangedEventArgs();
	}
}
