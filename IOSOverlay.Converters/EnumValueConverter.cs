using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(Enum), typeof(int))]
	public class EnumValueConverter:BaseConverter {

		#region IValueConverter Members

		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value != null && parameter != null) {
				return Enum.ToObject(parameter as Type, value);
			}
			return 0;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value != null && parameter != null) {
				return (int)value;
			}
			return null;
		}

		#endregion IValueConverter Members
	}
}
