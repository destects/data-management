using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IOSOverlay.Converters {
	public class EnumFlagCheckToVisibilityConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value is Enum && parameter is Enum) {
				return (value as Enum).HasFlag((parameter as Enum)) ? Visibility.Visible : Visibility.Collapsed;
			}
			return Visibility.Collapsed;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
