using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace IOSOverlay.Converters {

	public abstract class BaseConverter:DependencyObject, IValueConverter {

		//public override object ProvideValue(IServiceProvider serviceProvider) {
		//	return this;
		//}

		#region IValueConverter Members

		public abstract object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);

		public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
		}

		#endregion IValueConverter Members
	}
}
