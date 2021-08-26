using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSOverlay.Data {
	public interface ICommonInformation {
		string Name { get; set; }
		string Description { get; set; }
		string Category { get; set; }
		DateTime DateCreated { get; set; }
		DateTime LastModified { get; set; }
		DateTime LastUsed { get; set; }
		void SetLastUsed(DateTime used);
	}
}
