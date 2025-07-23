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
                ? _db.Products.Include(p => p.Categorys).ToList()
                : _db.Products.Include(p => p.Categorys).Where(p => p.NameProduct.Contains(searchName)).ToList();

            ViewBag.SearchName = searchName;
            return View(products);
        }

        [HttpGet]
        public JsonResult SearchProductNames(string term)
        {
            var names = _db.Products
                .Where(p => p.NameProduct.Contains(term))
                .Select(p => p.NameProduct)
                .Take(10)
                .ToList();
            return Json(names);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        public IActionResult AddProduct()
        {
            var categories = _db.Categories.ToList();
            ViewBag.Categories = categories;
            var suppliers = _db.Suppliers.ToList();
            ViewBag.Suppliers = suppliers;
            return View("AddProduct");
        }
        [HttpPost]
        public IActionResult AddProduct(Product product, IFormCollection filed)
        {
            product.NameProduct = filed["ProductName"];
            product.Description = filed["Description"];
            product.Unit = filed["Unit"];

            string priceStr = filed["Price"].FirstOrDefault();
            product.Price = !string.IsNullOrWhiteSpace(priceStr) ? int.Parse(priceStr) : 0;

            string quantityStr = filed["Quantity"].FirstOrDefault();
            product.remainingQuantity = !string.IsNullOrWhiteSpace(quantityStr) ? int.Parse(quantityStr) : 0;

            string categoryIdStr = filed["CategoryId"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(categoryIdStr))
            {
                int categoryId = int.Parse(categoryIdStr);
                product.Categorys = _db.Categories.FirstOrDefault(c => c.IdCategory == categoryId);
            }

            string supplierIdStr = filed["SupplierId"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(supplierIdStr))
            {
                int supplierId = int.Parse(supplierIdStr);
                product.Suppliers = _db.Suppliers.FirstOrDefault(s => s.IdSupplier == supplierId);
            }
            _db.Products.Add(product);
            _db.SaveChanges();
           return RedirectToAction("Index");
        }
    }
}