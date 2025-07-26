using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WareHouse.Models;

namespace WareHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [RoleAuthorize(1)]
    public class ManageOrderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ManageOrderController> _logger;

        public ManageOrderController(ILogger<ManageOrderController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var orders = _db.Orders.Include(o => o.Suppliers).Include(o => o.DetailOrders).ToList();
            return View(orders);
        }

        [HttpGet]
        public IActionResult GetOrderDetail(int id)
        {
            var order = _db.Orders
                .Include(o => o.DetailOrders)
                    .ThenInclude(d => d.Products)
                .Include(o => o.Suppliers)
                .FirstOrDefault(o => o.IdOrder == id);

            if (order == null)
                return NotFound();

            var details = order.DetailOrders.Select(d => new {
                productName = d.Products.NameProduct,
                quantity = d.Quantity,
                price = d.Price,
                total = (d.Price ?? 0) * d.Quantity
            }).ToList();

            return Json(new {
                status = order.Status,
                supplier = order.Suppliers?.Name,
                orderDate = order.OrderDate,
                totalAmount = order.TotalAmount,
                items = details
            });
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int id, string newStatus)
        {
            var order = _db.Orders.FirstOrDefault(o => o.IdOrder == id);
            if (order == null)
                return NotFound();

            order.Status = newStatus;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}