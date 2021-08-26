using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;
using System.Diagnostics;

namespace IOSOverlay.Data.Models.IO {
	public static class ModelInporter {
		private static string ImportDialog() {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = true;
			ofd.CheckFileExists = false;
			ofd.CheckPathExists = true;
			ofd.DefaultExt = ".exm";
			ofd.Title = "Export Model";
			ofd.ValidateNames = true;
			ofd.FileName = "ExportedModel";
			ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

			return (ofd.ShowDialog() == true) ? ofd.FileName : null;
		}
		[Obsolete]
		public static void ImportModelSet(ModelCollection collection, bool overwrite) {
			var filename = ImportDialog();
			Debug.WriteLine($"SelectedFileName = {filename}", "Information");

			if(filename != null) {
				collection.ImportCollection(filename, overwrite);
			}
		}
	}
}
