using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WareHouse.Models;
using Microsoft.AspNetCore.Http;

namespace WareHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [RoleAuthorize(1)]
    public class RoleController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<RoleController> _logger;

        public RoleController(AppDbContext db, ILogger<RoleController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [Route("Admin/Role")]
        public IActionResult HomeRole()
        {
            var roles = _db.Roles.AsEnumerable().Where(r => r.Name != "Chủ").ToList();
            return View("HomeRole",roles);
        }
        [HttpPost]
        [Route("Admin/Role/Add")]
        public IActionResult AddRole(Role roles,IFormCollection filed)
        {
            roles.Name = filed["Name"];
            roles.Salary = int.Parse(filed["Salary"]);
            _db.Roles.Add(roles);
            _db.SaveChanges();
            return RedirectToAction("HomeRole");
        }
        [Route("Admin/Role/Edit/{id}")]
        public IActionResult EditRole(int id)
        {
            var rolez = _db.Roles.Find(id);
            if (rolez == null)
            {
                return NotFound();
            }
            return View("EditRole", rolez);
        }
        [HttpPost]
        [Route("Admin/Role/Edit/{id}")]
        public IActionResult Edit(int id, Role roles)
        {
            var existingRole = _db.Roles.Find(id);
            if (existingRole == null)
            {
                return NotFound();
            }
            existingRole.Name = roles.Name;
            existingRole.Salary = roles.Salary;
            _db.SaveChanges();
            return RedirectToAction("HomeRole");
        }

        [Route("Admin/Role/Delete/{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            _db.Roles.Remove(role);
            _db.SaveChanges();
            return RedirectToAction("HomeRole");
        }
    }
}