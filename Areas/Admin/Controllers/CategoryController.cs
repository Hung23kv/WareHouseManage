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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(AppDbContext db, ILogger<CategoryController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [Route("Admin/Category")]
        public IActionResult Home()
        {
            var categories = _db.Categories;
            return View(categories);
        }
        [Route("Admin/Category/Add")]
        [HttpPost]
        public IActionResult Add(Category category,IFormCollection filed)
        {
            category.NameCategory = filed["NameCategory"];
            category.Description = filed["Description"];
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Home");
        }
        [Route("Admin/Category/Edit/{id}")]
        public IActionResult EditCategory(int id)
        {
            var category = _db.Categories.Find(id);
            return View("EditCategory",category);
        }
        [HttpPost]
        [Route("Admin/Category/Edit/{id}")]
        public IActionResult Edit(int id,Category category)
        {
            var existingCategory = _db.Categories.Find(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.NameCategory = category.NameCategory;
            existingCategory.Description = category.Description;
            _db.SaveChanges();
            return RedirectToAction("Home");
        }
        [Route("Admin/Category/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.Find(id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Home");
        }
    }
}