using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {
	public class EnumSelectionConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value != null && parameter != null) {
				var val = value as Enum;
				var flg = parameter as Enum;
				return val.Equals(flg);
			}
			return false;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value != null && parameter != null) {
				var val = (bool)value;
				if(val) {
					return parameter;
				}
			}
			return Enum.ToObject(targetType, -1);
		}
	}
}
