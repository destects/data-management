using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(object), typeof(string))]
	public class DateTimeToStringConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return "NULL";
			if(value is DateTime && ((DateTime)value).Ticks == 0) return "N/A";
			string format = (string)parameter;
			if(!string.IsNullOrEmpty(format)) {
				return string.Format(culture, format, value);
			} else {
				return value.ToString();
			}
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}
}
