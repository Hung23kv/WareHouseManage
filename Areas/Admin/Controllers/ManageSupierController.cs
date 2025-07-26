using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WareHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [RoleAuthorize(1)]
    public class ManageSupierController : Controller
    {
        private readonly ILogger<ManageSupierController> _logger;

        public ManageSupierController(ILogger<ManageSupierController> logger)
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