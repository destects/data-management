using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(Enum), typeof(bool))]
	public class EnumToBoolConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null || parameter == null) return false;
			string enumValue = value.ToString();
			string targetValue = parameter.ToString();
			bool outputValue = enumValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
			return outputValue;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null || parameter == null) return null;
			bool useValue = (bool)value;
			string targetValue = parameter.ToString();
			if(useValue) return Enum.Parse(targetType, targetValue);
			return null;
		}
	}
}
