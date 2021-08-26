using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Simulation;
namespace IOSOverlay.Converters {
	public class ListHiddenVisibilityConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value is Enum) {
			}

			return Visibility.Visible;
		}
	}
}
