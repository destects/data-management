using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xaml;

namespace IOSOverlay.Converters {

	public class DBNullToNullConverter:BaseConverter {

		#region IValueConverter Members

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(DBNull.Value.Equals(value)) {
				return null;
			}
			return value;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}

		#endregion IValueConverter Members
	}
}
