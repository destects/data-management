using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace IOSOverlay.Converters {
	[ValueConversion(typeof(ICommand), typeof(Visibility))]
	public class CommandToVisibilityConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value is ICommand) {
				var x = (ICommand)value;
				return x.CanExecute(parameter) ? Visibility.Visible : Visibility.Collapsed;
			}
			return Visibility.Visible;
		}
	}
}
