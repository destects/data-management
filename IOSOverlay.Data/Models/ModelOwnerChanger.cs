using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Data.Models {
	public class ModelOwnerChanger:IDisposable {
		private Model _Model;

		public ModelOwnerChanger(Model model) {
			_Model = model;
		}

		public void SetOwner(Guid ownerID) {
			if(_Model != null) {
				using(_Model.EditToken) {
					_Model.CreatorUID = ownerID;
				}
			}
		}

		public void Dispose() {
		}
	}
}
