using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WareHouse.Helpers;
using WareHouse.Models;

namespace WareHouse.Areas.Staff.Controllers
{
    [Area("Staff")]
    [RoleAuthorize(4,5)]
    public class ViewTimeSheetController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ViewTimeSheetController> _logger;

        public ViewTimeSheetController(AppDbContext db, ILogger<ViewTimeSheetController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index(int? month, int? year)
        {
            var now = DateTime.Now;
            int m = month ?? now.Month;
            int y = year ?? now.Year;

            int? userId = HttpContext.Session.GetInt32("idUser");
            if (userId == null)
            {
                // Handle missing session (not logged in)
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            var employees = _db.Users.Where(u => u.IdUser == userId.Value).ToList();
            var timesheets = _db.TimeSheets
                .Where(t => t.Users != null && t.Users.IdUser == userId.Value && t.Date.Month == m && t.Date.Year == y)
                .ToList();

            var viewModel = new TimeKeepingVM
            {
                Employees = employees,
                TimeSheets = timesheets,
                Month = m,
                Year = y
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}