using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Converters {
	public class SecondsToTimeConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var seconds = (int)value;
			var time = "";
			if(seconds > 3600) {
				var h = seconds / 3600;
				time += (h < 10 ? "0" : "") + h + ":";
				seconds -= h * 3600;
			} else {
				time += "00:";
			}

			if(seconds > 60) {
				var m = seconds / 60;
				time += (m < 10 ? "0" : "") + m + ":";
				seconds -= m * 60;
			} else {
				time += "00:";
			}
			time += (seconds < 10 ? "0" : "") + seconds;

			return time;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
