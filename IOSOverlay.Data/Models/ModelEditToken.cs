using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Data.Models {
	internal class ModelEditToken:IDisposable {
		private bool _PreTokenEditMode;
		// Using a WeakReference so we don't hold stop the GC from collecting the model, should collection occur.
		private Model _Model;

		public ModelEditToken(Model model) {
			_Model = model;
			_PreTokenEditMode = model.EditMode;
			if(!_PreTokenEditMode) {
				model.BeginEdit();
			}
		}
		public void Dispose() {
			if(!_PreTokenEditMode) {
				_Model.EndEdit();
			}
		}
	}
}
