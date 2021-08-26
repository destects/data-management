using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(decimal), typeof(decimal))]
	public class ScaledFloatConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return value;
			//var param = (string)parameter;
			//var val = (decimal)value;

			//if(!string.IsNullOrWhiteSpace(param)) {
			//   decimal multiple = 1.0m;
			//   System.Diagnostics.Debug.Assert(decimal.TryParse(param, out multiple), "Failed to cast parameter to float!");

			//   return (float)(val * multiple);
			//}

			//return 0.0f;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
			//return value;
			//var param = (string)parameter;
			//var val = (decimal)((double)value);

			//if(!string.IsNullOrWhiteSpace(param)) {
			//   decimal multiple = 1.0m;
			//   System.Diagnostics.Debug.Assert(decimal.TryParse(param, out multiple), "Failed to cast parameter to float!");

			//   return (float)(val / multiple);
			//}

			//return 0.0f;
		}
	}
}
