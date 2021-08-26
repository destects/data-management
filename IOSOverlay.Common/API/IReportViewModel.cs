using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOSOverlay.Data.Models;

namespace IOSOverlay.Common.API {
	public interface IReportViewModel {
		void LoadReport(ReportModel report);
	}
}
