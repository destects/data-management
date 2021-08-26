using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Drawing;

namespace IOSOverlay.Common {
	public static class BitmapConverter {
		//public static Bitmap ImageFileSelector() {
		//	using(var dlg = new System.Windows.Forms.OpenFileDialog()) {
		//		dlg.ShowDialog();
		//		try {
		//			var img = (Bitmap)Bitmap.FromFile(dlg.FileName);
		//			return img;
		//		} catch {
		//			MessageBox.Show("Failed to load image");
		//		}
		//	}
		//	return null;
		//}

		public static string BitmapToString(Bitmap img) {
			var bytes = new byte[0];
			using(var ms = new MemoryStream()) {
				img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				ms.Close();
				bytes = ms.ToArray();
			}
			return Convert.ToBase64String(bytes);
		}

		public static Bitmap StringToBitmap(string data) {
			var bytes = Convert.FromBase64String(data);
			using(var ms = new MemoryStream(bytes)) {
				return (Bitmap)Bitmap.FromStream(ms);
			}
		}
	}
}
