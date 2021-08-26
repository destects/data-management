using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Converters {
	public class EnumHasFlagConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value != null && parameter != null) {
				return (value as Enum).HasFlag((parameter as Enum));
			}
			return false;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
