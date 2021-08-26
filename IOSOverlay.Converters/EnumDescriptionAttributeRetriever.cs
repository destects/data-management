using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Converters {
	public class EnumDescriptionAttributeRetriever:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value is Enum) {
				return Simulation.EnumDescriptionAttribute.GetDescription(value as Enum);
			} else {
				return null;
			}
		}
	}
}
