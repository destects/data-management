using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {
	public class EnumFlagValueConverter:BaseConverter {
		private Enum FlagsValue;

		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(value != null && parameter != null) {
				var val = value as Enum;
				var flg = parameter as Enum;
				FlagsValue = value as Enum;
				if(val == null || flg == null || FlagsValue == null) return false;
				return val.HasFlag(flg);
			}
			return false;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var val = (bool)value;
			var x = System.Convert.ToInt32(FlagsValue);
			var flg = (int)parameter;

			if(val) {
				x |= flg;
			} else {
				x &= ~flg;
			}
			var y = Enum.ToObject(targetType, x);
			return y;
		}
	}
}
