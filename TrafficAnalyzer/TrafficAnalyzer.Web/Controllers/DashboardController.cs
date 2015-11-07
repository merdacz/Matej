using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrafficAnalyzer.Web.Controllers
{
    using TrafficAnalyzer.Shared;

    public class DashboardController : Controller
    {
        private readonly ILogStorage logStorage;

        public DashboardController(ILogStorage logStorage)
        {
            this.logStorage = logStorage;
        }

        public ActionResult Index()
        {
            var entries = this.logStorage.GetDashboardReport();
            return this.View(entries);
        }
    }
}