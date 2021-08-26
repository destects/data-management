using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace IOSOverlay.Converters {
	[ValueConversion(typeof(float), typeof(string))]
	public class TimeSpanConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return TimeSpan.FromSeconds(System.Convert.ToSingle(value)).ToString();
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			TimeSpan t;
			if(TimeSpan.TryParse(value as string, out t)) {

				return (float)t.TotalSeconds;
			}
			return null;
		}
	}
}
