using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UltimateAspCoreProject.Areas.Dashboard.Controllers
{
    [Authorize("DashboardPolicy")]
    [Area("Dashboard")]
    [Route("Dashboard/[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
