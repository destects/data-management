using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Converters {
	public class DisplayNameAttributeRetriever:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			object v;
			if(value is Enum) {
				return Simulation.DisplayNameAttribute.GetDisplayName(value as Enum);
			} else {
				if(parameter != null) {
					v = Enum.ToObject(parameter as Type, value);
				} else {
					v = value;
				}

				if(v == null || (v as string) == "") {
					return "";
				}

				return Simulation.DisplayNameAttribute.GetDisplayName(v as Enum);
			}
		}
	}
}
