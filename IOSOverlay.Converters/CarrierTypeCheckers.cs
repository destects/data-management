using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Simulation;


namespace IOSOverlay.Converters {
	[ValueConversion(typeof(Enum), typeof(bool))]
	public class CarrierTypeTruckChecker:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return false;
			try {
				var a = Enum.ToObject(typeof(CraneModels), value);
				var x = CarrierTypeAttribute.GetCarrierType((CraneModels)a);
				return x == CarrierTypes.Truck;
			} catch {

			}
			return false;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

	[ValueConversion(typeof(Enum), typeof(Visibility))]
	public class CarrierTypeTruckVisibilityChecker:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return Visibility.Collapsed;
			try {
				var a = Enum.ToObject(typeof(CraneModels), value);
				var x = CarrierTypeAttribute.GetCarrierType((CraneModels)a);
				return (x == CarrierTypes.Truck) ? Visibility.Visible : Visibility.Collapsed;
			} catch {

			}
			return Visibility.Collapsed;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

	[ValueConversion(typeof(Enum), typeof(bool))]
	public class CarrierTypeCrawlerChecker:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return false;
			try {
				var a = Enum.ToObject(typeof(CraneModels), value);
				var x = CarrierTypeAttribute.GetCarrierType((CraneModels)a);
				return x == CarrierTypes.Crawler;
			} catch {

			}
			return false;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

	[ValueConversion(typeof(Enum), typeof(Visibility))]
	public class CarrierTypeCrawlerVisibilityChecker:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return Visibility.Collapsed;
			try {
				var a = Enum.ToObject(typeof(CraneModels), value);
				var x = CarrierTypeAttribute.GetCarrierType((CraneModels)a);
				return (x == CarrierTypes.Crawler) ? Visibility.Visible : Visibility.Collapsed;
			} catch {

			}
			return Visibility.Collapsed;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

	[ValueConversion(typeof(Enum), typeof(bool))]
	public class CarrierTypeRoughTerrainChecker:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return false;
			try {
				var a = Enum.ToObject(typeof(CraneModels), value);
				var x = CarrierTypeAttribute.GetCarrierType((CraneModels)a);
				return x == CarrierTypes.RoughTerrain;
			} catch {

			}
			return false;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

	[ValueConversion(typeof(Enum), typeof(Visibility))]
	public class CarrierTypeRoughTerrainVisibilityChecker:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return Visibility.Collapsed;
			try {
				var a = Enum.ToObject(typeof(CraneModels), value);
				var x = CarrierTypeAttribute.GetCarrierType((CraneModels)a);
				return (x == CarrierTypes.RoughTerrain) ? Visibility.Visible : Visibility.Collapsed;
			} catch {

			}
			return Visibility.Collapsed;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

	[ValueConversion(typeof(Enum), typeof(bool))]
	public class CarrierTypeCarryDeckChecker:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return false;
			try {
				var a = Enum.ToObject(typeof(CraneModels), value);
				var x = CarrierTypeAttribute.GetCarrierType((CraneModels)a);
				return x == CarrierTypes.CarryDeck;
			} catch {

			}
			return false;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

	[ValueConversion(typeof(Enum), typeof(Visibility))]
	public class CarrierTypeCarryDeckVisibilityChecker:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value == null) return Visibility.Collapsed;
			try {
				var a = Enum.ToObject(typeof(CraneModels), value);
				var x = CarrierTypeAttribute.GetCarrierType((CraneModels)a);
				return (x == CarrierTypes.CarryDeck) ? Visibility.Visible : Visibility.Collapsed;
			} catch {

			}
			return Visibility.Collapsed;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

}
