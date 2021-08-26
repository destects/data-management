using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(object), typeof(bool))]
	public class BoolToInvertBoolConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return !((bool)value);
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			if(parameter is string) {
				if(((string)parameter).Equals("rev", StringComparison.CurrentCultureIgnoreCase)) {
					return !((bool)value);
				} else {
					return ((bool)value);
				}
			}
			return null;
		}
	}
}
