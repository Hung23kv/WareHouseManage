using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WareHouse.Helpers;
using WareHouse.Models;

namespace WareHouse.Areas.Manager.Controllers
{
    [Area("Manager")]
    [RoleAuthorize(3)]
    public class TimeKeepingController : Controller
    {
        private readonly AppDbContext _db;

        public TimeKeepingController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? month, int? year)
        {
            var now = DateTime.Now;
            int m = month ?? now.Month;
            int y = year ?? now.Year;

            var employees = _db.Users.Where(u => u.Roles != null && u.Roles.IdRole != 1).ToList();
            var timesheets = _db.TimeSheets
                .Where(t => t.Date.Month == m && t.Date.Year == y)
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

        [HttpPost]
        public IActionResult CheckIn(int[] checkedInUserIds)
        {
            var today = DateTime.Today;
            foreach (var userId in checkedInUserIds)
            {
                // Kiểm tra đã có chấm công chưa
                var exists = _db.TimeSheets.Any(t => t.Users != null && t.Users.IdUser == userId && t.Date == today);
                if (!exists)
                {
                    var ts = new TimeSheet
                    {
                        Users = _db.Users.FirstOrDefault(u => u.IdUser == userId),
                        Date = today,
                        CheckIn = DateTime.Now.TimeOfDay
                    };
                    _db.TimeSheets.Add(ts);
                }
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}