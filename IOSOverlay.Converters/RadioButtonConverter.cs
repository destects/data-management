using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IOSOverlay.Converters {

	public class RadioButtonConverter:BaseConverter {

		#region IValueConverter Members

		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value == null || parameter == null) {
				return false;
			}
			bool IncomingValue;
			try {
				IncomingValue = (bool)value;
			} catch {
				return false;
			}
			bool targetValue = bool.Parse(parameter.ToString());

			if(IncomingValue == targetValue) {
				return true;
			} else {
				return null;
			}
			//return IncomingValue == targetValue;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value == null || parameter == null) {
				return false;
			}

			bool ourCheckValue = (bool)value;
			bool targetValue = bool.Parse(parameter.ToString());

			if(ourCheckValue == targetValue) {
				return true;
			} else {
				return null;
			}
			//return ourCheckValue == targetValue;
		}

		#endregion IValueConverter Members
	}
}
