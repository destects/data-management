using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using Simulation;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace IOSOverlay.Converters {
	public class SuperstructureModelEnumFilter:BaseMultiConverter {
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			if(values == null || values.Length < 2 || values[0] == null || values[1] == null) return values?[0];

			var filteredList = new ArrayList();
			var initial = Enum.GetValues(values[0] as Type);
			var targetModel = SuperstructureModelAttribute.GetSuperstructureModel(values[1] as Enum);
			foreach(Enum val in initial) {
				var model = SuperstructureModelAttribute.GetSuperstructureModel(val);
				if(model == targetModel) {
					filteredList.Add(val);
				}
			}

			return filteredList.ToArray();
		}

		public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			return null;
		}
	}
}
