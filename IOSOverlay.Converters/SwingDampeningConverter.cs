using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IOSOverlay.Converters {
	public class SwingDampeningConverter:BaseMultiConverter {
		private const float MIN_ANGLE = 0;
		private const float MIN_COEFF = 0.1f;
		private const float MAX_ANGLE = 5;
		private const float MAX_COEFF = 10;

		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			if(values != null && values.Length == 2) {
				if(values[0] is float && values[1] is float) {
					var angle = (float)values[0];
					var coeff = (float)values[1];

					if(angle == MAX_ANGLE && coeff == MIN_COEFF) {
						return 0d;
					}
					if(angle == 0.1f && coeff == MAX_COEFF) {
						return 9d;
					}
					if(angle == MIN_ANGLE && coeff == MAX_COEFF) {
						return 10d;
					}
					return coeff * 10d;
				}
			}
			return 0d;
		}

		public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			if(value != null) {
				float angle = 0;
				float coeff = 0;
				var tick = (int)((double)value);
				switch(tick) {
					case 0:
						angle = MAX_ANGLE;
						coeff = MIN_COEFF;
						break;
					case 9:
						angle = 0.1f;
						coeff = MAX_COEFF;
						break;
					case 10:
						angle = MIN_ANGLE;
						coeff = MAX_COEFF;
						break;
					default: {
							var x = tick / 10.0f;
							angle = MAX_ANGLE - (x * MAX_ANGLE);
							coeff = x;
						}
						break;
				}

				return new object[] { angle, coeff };
			}
			return null;
		}
	}
}
