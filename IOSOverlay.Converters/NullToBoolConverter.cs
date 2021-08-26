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
	public class NullToBoolConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return value != null;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}
}
