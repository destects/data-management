using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IOSOverlay.Converters {
	[ValueConversion(typeof(string), typeof(ImageSource))]
	public class URIToImageConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value != null && value is string) {
				ImageSource b = new BitmapImage(new Uri(value as string));
				return b;
			}
			return null;
		}
	}
}
