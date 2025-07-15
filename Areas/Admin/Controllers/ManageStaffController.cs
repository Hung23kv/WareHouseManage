using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WareHouse.Areas.Admin.Controllers
{
    [Route("[controller]")]
    public class ManageStaffController : Controller
    {
        private readonly ILogger<ManageStaffController> _logger;

        public ManageStaffController(ILogger<ManageStaffController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}