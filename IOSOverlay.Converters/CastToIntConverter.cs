using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {
	public class CastToIntConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return System.Convert.ToInt32(value);
			//if(value is double) return (int)value;
			//if(value is float) return ((int)((float)value));
			//return value;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return value;
		}
	}
}