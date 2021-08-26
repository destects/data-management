using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(object), typeof(object))]
	public class ViewViewModelConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value != null) {
				var view = value.GetView();
				if(view != null) {
					var val = Activator.CreateInstance(view);
					(val as UserControl).DataContext = value;
					return val;
				}
			}
			return value;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}
}
