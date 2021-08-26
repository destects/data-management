using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {
	public class AdvancedCraneConfigurationConverter:BaseMultiConverter {
		private const int TICK_OFFSET = 5;

		private float _StandardValue;
		private float _DeviationPercent;
		private bool _Inited = false;

		// Convert from real value to "normalized 0-10 value; 5 = standard values.
		//public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		//	if(_DeviationPercent == 0f) return value;
		//	var current = (float)value;
		//	var x = _StandardValue * _DeviationPercent;
		//	var y = current - _StandardValue;
		//	var ticks = (y / x) + TICK_OFFSET;
		//	return ticks;
		//}

		//public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
		//	if(_DeviationPercent == 0f) return null;

		//	var ticks = (((double)value) - TICK_OFFSET);

		//	return ((ticks * _DeviationPercent) + 1f) * _StandardValue;
		//}

		private float CalcTicks(float current) {
			var x = _StandardValue * _DeviationPercent;
			var y = current - _StandardValue;
			return (y / x);
		}

		private object[] CalcReal(float ticks) {
			var nc = ((ticks * _DeviationPercent) + 1f) * _StandardValue;
			return new object[] {
				nc,
				_StandardValue
			};
		}

		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			if(values != null && values.Length == 2) {
				if(values[0] is float) {

					var current = (float)values[0];
					if(!_Inited) {
						try {
							_StandardValue = (float)values[1];
						} catch {
							System.Diagnostics.Debug.WriteLine("Couldn't get values1");
							_StandardValue = 0.5f;
						}
						_DeviationPercent = float.Parse((string)parameter);
						_Inited = true;
					}
					var d = CalcTicks(current) + TICK_OFFSET;
					return (double)d;
				}
			}
			return (double)TICK_OFFSET;
		}

		public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			if(value != null) {

				var ticks = ((double)value) - TICK_OFFSET;
				return CalcReal((float)ticks);
			} else {
				return null;
			}
		}
	}
}
