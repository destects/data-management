using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using Simulation;

namespace IOSOverlay.Converters {
	public class ViewpointFilterConverter:BaseMultiConverter {
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			if(values == null || values[0] == null || values.Length < 2 || values[1] == null) return values?[0];

			//return values[0];

			//throw new NotImplementedException();
			var filteredList = new List<object>();
			var inputMethod = (ControlInputMethodTypes)values[1];
			var viewpoints = (object[])values[0];
			for(int i = 0; i < viewpoints.Length; i++) {
				var x = (OperatorViewPointPositions)viewpoints[i];
				var clv = InputTypeLinkAttribute.GetLinkedInputType(x);
				if(clv == inputMethod) {
					filteredList.Add(viewpoints[i]);
				}
			}

			return filteredList.ToArray();
		}

		public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			//throw new NotImplementedException();
			return null;
		}
	}
}
