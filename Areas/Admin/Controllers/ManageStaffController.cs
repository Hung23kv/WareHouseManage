using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WareHouse.Models;
using Microsoft.EntityFrameworkCore;


namespace WareHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageStaffController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ManageStaffController> _logger;

        public ManageStaffController(AppDbContext db, ILogger<ManageStaffController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult HomeManageStaff()
        {
            var SelectRole = _db.Roles.AsEnumerable().Where(r => r.Name != "Chủ").ToList();
            ViewBag.Roles = SelectRole;
            var users = _db.Users == null
                ? new List<WareHouse.Models.User>()
                : _db.Users.Include(u => u.Roles).Where(u => u.Roles != null && u.Roles.IdRole != 1).ToList();
            return View("HomeManageStaff", users);
        }
        [HttpPost]
        [Route("Admin/ManageStaff/Add")]
        public IActionResult AddStaff(User user, IFormCollection filed)
        {
            user.Name = filed["Name"];
            user.Email = filed["Email"];
            user.UserPassword = filed["Password"];
            user.IsActive = true;
            user.CreatedDate = DateTime.Now;
            user.RolesidRole = int.Parse(filed["RoleId"]);
            user.Image = "default.png";
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("HomeManageStaff");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        public IActionResult DeleteStaff(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            return RedirectToAction("HomeManageStaff");
        }

        [HttpPost]
        [Route("Admin/ManageStaff/EditAll")]
        public IActionResult EditAll(List<User> users)
        {
            foreach (var u in users)
            {
                var user = _db.Users.Find(u.IdUser);
                if (user != null)
                {
                    user.IsActive = u.IsActive;
                    user.RolesidRole = u.RolesidRole;
                }
            }
            TempData["SuccessMessage"] = "Cập nhật thành công!";
            _db.SaveChanges();
            return RedirectToAction("HomeManageStaff");
        }
    }
}