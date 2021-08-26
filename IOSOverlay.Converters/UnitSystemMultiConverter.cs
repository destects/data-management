using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Utility;

namespace IOSOverlay.Converters {
	[ValueConversion(typeof(float), typeof(decimal))]
	public class UnitSystemMultiConverter:BaseMultiConverter {
		public readonly UnitSystem StorageSystem = UnitSystem.Metric;
		public readonly TimeScales StorageScale = TimeScales.Seconds;
		private UnitSystem _DisplayUnits;
		private TimeScales _DisplayScale;

		/// <summary>
		/// Converts the specified values.
		/// </summary>
		/// <param name="values">
		/// Value Structure:
		/// [0] = inputValue as float
		/// [1] = DisplayType as UnitSystem
		/// [2] = DisplayScale as TimeScale
		/// </param>
		/// <param name="targetType"></param>
		/// <param name="parameter">Type of Measurement</param>
		/// <param name="culture"></param>
		/// <returns>inputValue converted to displayValue</returns>
		public override object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if(values[0] is float) {
				decimal inputValue = (decimal)(float)values[0];
				_DisplayUnits = (int)values[1];

				if(values.Length > 2) {
					_DisplayScale = (TimeScales)((int)values[2]);
				} else {
					_DisplayScale = TimeScales.Minutes;
				}

				var mtype = MeasurementTypes.Speed;
				if(parameter is MeasurementTypes) {
					mtype = (MeasurementTypes)parameter;
				}

				var nval = UnitSystem.Convert(StorageSystem, _DisplayUnits, inputValue, mtype, StorageScale, _DisplayScale);
				var displayValue = decimal.Round(nval, 3, MidpointRounding.AwayFromZero);
				return string.Format("{0:0.000}", displayValue);
			}
			return null;
		}

		/// <summary>
		/// Converts the back.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="targetTypes">The target types.</param>
		/// <param name="parameter">The parameter.</param>
		/// <param name="culture">The culture.</param>
		/// <returns>displayValue converted back to inputValue</returns>
		public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
			decimal val;
			if(decimal.TryParse((string)value, out val)) {
				var mtype = MeasurementTypes.Speed;

				if(parameter is MeasurementTypes) {
					mtype = (MeasurementTypes)parameter;
				}
				var nval = UnitSystem.Convert(_DisplayUnits, StorageSystem, val, mtype, _DisplayScale, StorageScale);

				return new object[] { (float)nval };
			}
			return null;
		}
	}
}
