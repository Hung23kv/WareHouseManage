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
    [RoleAuthorize(1)]
    public class ManageWareHouseController : Controller
    {
        private readonly ILogger<ManageWareHouseController> _logger;

        public ManageWareHouseController(ILogger<ManageWareHouseController> logger)
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