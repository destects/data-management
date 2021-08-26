using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {
	public class RoundNumberConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			//return value;
			var decimalPlaces = 0;
			if(parameter != null && parameter is string) {
				int p = 0;
				if(int.TryParse((string)parameter, out p)) {
					decimalPlaces = p;
				}
			}
			if(value is double) return Math.Round((double)value, decimalPlaces);
			if(value is float) return (float)Math.Round((float)value, decimalPlaces);
			return value;
		}

		//public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
		//	return value;
		//}
	}
}
