using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace IOSOverlay.Converters {
	public class StringToBitmapConverter:BaseConverter {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if(value is string && !string.IsNullOrWhiteSpace(value as string)) {
				return StringToBitmapImage(value as string);
			}
			return null;
		}

		private static Bitmap StringToBitmap(string data) {
			var bytes = System.Convert.FromBase64String(data);
			using(var ms = new MemoryStream(bytes)) {
				return (Bitmap)Bitmap.FromStream(ms);
			}
		}

		private static BitmapImage StringToBitmapImage(string data) {
			var g = new BitmapImage();
			var bytes = System.Convert.FromBase64String(data);
			g.BeginInit();
			g.StreamSource = new MemoryStream(bytes);
			g.EndInit();
			return g;
		}
	}
}
