using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(object), typeof(Visibility))]
	public class BoolToVisibilityConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			bool v = (bool)value;
			if(parameter != null) {
				int p = int.Parse(parameter as string);
				switch(p) {
					case 0:
						return (v ? Visibility.Visible : Visibility.Collapsed);
					case 1:
						return (v ? Visibility.Visible : Visibility.Hidden);
					case 2:
						return (v ? Visibility.Collapsed : Visibility.Visible);
					case 3:
						return (v ? Visibility.Hidden : Visibility.Visible);
					default:
						goto case 0;
				}
			}
			return (v ? Visibility.Visible : Visibility.Collapsed);
		}
	}
}
