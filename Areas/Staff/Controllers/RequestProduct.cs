using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WareHouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WareHouse.Helpers;

namespace WareHouse.Areas.Staff.Controllers
{
    [Area("Staff")]
    [RoleAuthorize(4,5)]
    public class RequestProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<RequestProductController> _logger;

        public RequestProductController(ILogger<RequestProductController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int? CategoryRequest)
        {
            var category = _db.Categories.ToList();
            ViewBag.CategoryRequest = category;
            List<Product> listProduct;
            if (CategoryRequest.HasValue)
            {
                listProduct = _db.Products.Where(p => p.Categorys != null && p.Categorys.IdCategory == CategoryRequest.Value).ToList();
            }
            else
            {
                listProduct = _db.Products.ToList();
            }
            return View(listProduct);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public List<RequestProduct> GetRequestProduct()
        {
            var listRequestProduct = HttpContext.Session.GetObjectFromJson<List<RequestProduct>>("RequestCart");
            if (listRequestProduct == null)
            {
                listRequestProduct = new List<RequestProduct>();
                HttpContext.Session.SetObjectAsJson("RequestCart", listRequestProduct);
            }
            return listRequestProduct;
        }

        public IActionResult AddToCart(int id, string url)
        {
            var listRequestProduct = GetRequestProduct();
            var value = listRequestProduct.FirstOrDefault(p => p.IdProduct == id);
            if (value == null)
            {
                var product = _db.Products.Find(id);
                if (product != null)
                {
                    value = new RequestProduct
                    {
                        IdProduct = product.IdProduct,
                        RequestName = product.NameProduct,
                        quantity = product.remainingQuantity,
                        RequestQuantity = 1,
                        Count = 1
                    };

                    listRequestProduct.Add(value);
                }
            }
            else
            {
                value.Count++;
            }
            HttpContext.Session.SetObjectAsJson("RequestCart", listRequestProduct);
            return Redirect(url);
        }

        public IActionResult Cart()
        {
            var listRequestProduct = GetRequestProduct();
            if (listRequestProduct.Count == 0)
            {
                return RedirectToAction("Index", "RequestProduct", new { area = "Staff" });
            }
            return View(listRequestProduct);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var listRequestProduct = GetRequestProduct();
            var value = listRequestProduct.FirstOrDefault(p => p.IdProduct == id);
            if (value != null)
            {
                listRequestProduct.RemoveAll(p => p.IdProduct == id);
                HttpContext.Session.SetObjectAsJson("RequestCart", listRequestProduct);
                if (listRequestProduct.Count == 0)
                {
                    HttpContext.Session.Remove("RequestCart");
                    return Json(new { success = true, redirect = Url.Action("Index", "RequestProduct", new { area = "Staff" }) });
                }
            }
            else
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var listRequestProduct = GetRequestProduct();
            int count = listRequestProduct.Sum(p => p.Count);
            return Json(new { count });
        }

        [HttpPost]
        public IActionResult UpdateCart(int id,IFormCollection f)
        {
            var listRequestProduct = GetRequestProduct();
            var value = listRequestProduct.FirstOrDefault(p => p.IdProduct == id);
            if (value != null)
            {
                value.RequestQuantity = int.Parse(f["quantity"]);
            }
            HttpContext.Session.SetObjectAsJson("RequestCart", listRequestProduct);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult ConfirmRequest(IFormCollection f)
        {
            var listRequestProduct = GetRequestProduct();
            int? userId = HttpContext.Session.GetInt32("idUser");
            if (userId == null)
            {
                // Handle missing session (not logged in)
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            ItemRequest itemRequest = new ItemRequest();
            itemRequest.Users = _db.Users.FirstOrDefault(u => u.IdUser == userId.Value);
            var RqDate = string.Format("{0:MM/dd/yyyy}", "2025-06-30");
            itemRequest.RequestDate = DateTime.Parse(RqDate);
            itemRequest.Status = "Chưa xác nhận";
            itemRequest.Note = f["Description"];
            itemRequest.IsApproved = false;
            _db.ItemRequests.Add(itemRequest);
            _db.SaveChanges();
            foreach (var product in listRequestProduct)
            {
                DetailRequest detailRequest = new DetailRequest();
                detailRequest.ItemRequests = _db.ItemRequests.OrderByDescending(i => i.IdItemRequest == itemRequest.IdItemRequest).FirstOrDefault();
                detailRequest.Quantity = product.RequestQuantity;
                detailRequest.Products = _db.Products.FirstOrDefault(p => p.IdProduct == product.IdProduct);
                _db.DetailRequests.Add(detailRequest);
                _db.SaveChanges();
            }
            HttpContext.Session.Remove("RequestCart");
            return RedirectToAction("Index", "RequestProduct", new { area = "Staff" });
        }
    }
}