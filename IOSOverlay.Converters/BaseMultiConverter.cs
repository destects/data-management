using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace IOSOverlay.Converters {
	public abstract class BaseMultiConverter:DependencyObject, IMultiValueConverter {

		//public override object ProvideValue(IServiceProvider serviceProvider) {
		//	return this;
		//}

		#region IMultiValueConverter Members

		public abstract object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture);

		public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture);

		#endregion
	}
}
