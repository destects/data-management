using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Converters {
	public class ExpanderGroupExpansionConverter:BaseConverter {
		private int _ExpansionID = 0;

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return (System.Convert.ToInt32(value) == System.Convert.ToInt32(parameter));
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			if(System.Convert.ToBoolean(value)) {
				_ExpansionID = System.Convert.ToInt32(parameter);
				return System.Convert.ToInt32(parameter);
			} else {
				return -1;
			}
		}
	}
}
