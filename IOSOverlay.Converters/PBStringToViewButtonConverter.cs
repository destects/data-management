using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using IOSOverlay.Data;

namespace IOSOverlay.Converters {

	[ValueConversion(typeof(object), typeof(bool))]
	internal class PBStringToViewButtonConverter:BaseConverter {

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			var o = value as string;

			if(string.IsNullOrWhiteSpace(o)) {
				return false;
			}
			if(!System.IO.File.Exists(o)) {
				return false;
			}
			return true;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}
}
