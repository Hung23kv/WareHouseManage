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
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ProductController> _logger;

        public ProductController(AppDbContext db, ILogger<ProductController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index(string searchName)
        {
            var products = string.IsNullOrEmpty(searchName)
                ? _db.Products.ToList()
                : _db.Products.Where(p => p.NameProduct.Contains(searchName)).ToList();

            ViewBag.SearchName = searchName;
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}