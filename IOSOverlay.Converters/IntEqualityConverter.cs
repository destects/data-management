using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(int), typeof(bool))]
	public class IntEqualityConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			int param = -2;
			int.TryParse((string)parameter, out param);

			return (param == ((int)value));
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
		}
	}
}
