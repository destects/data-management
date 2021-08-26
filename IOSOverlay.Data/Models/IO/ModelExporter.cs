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
	public static class ModelExporter {
		private static string ExportDialog() {
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.AddExtension = true;
			sfd.CheckFileExists = false;
			sfd.CheckPathExists = true;
			sfd.DefaultExt = ".exm";
			sfd.OverwritePrompt = true;
			sfd.Title = "Export Model";
			sfd.ValidateNames = true;
			sfd.FileName = "ExportedModel";
			//sfd.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyComputer);
			sfd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

			//string fn = null;
			//if(sfd.ShowDialog() == true) fn = sfd.FileName;
			//return fn;
			return (sfd.ShowDialog() == true) ? sfd.FileName : null;
		}
		[Obsolete]
		public static void ExportSingleModel(ModelCollection collection, int id) {
			var filename = ExportDialog();
			Debug.WriteLine($"SelectedFileName = {filename}", "Information");

			if(filename != null) {
				//model._FileName = filename;
				//model.Save();
				//model.SerializeInternal(filename);
				collection.ExportSubCollection(filename, new[] { id });
			}
		}
		[Obsolete]
		public static void ExportModelSet(ModelCollection collection, IEnumerable<int> ids) {
			var filename = ExportDialog();
			Debug.WriteLine($"SelectedFileName = {filename}", "Information");

			if(filename != null) {
				//model._FileName = filename;
				//model.Save();
				//model.SerializeInternal(filename);
				collection.ExportSubCollection(filename, ids);
			}
		}
	}
}
