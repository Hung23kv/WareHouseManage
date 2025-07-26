using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WareHouse.Models;
using Microsoft.EntityFrameworkCore;

namespace WareHouse.Areas.StaffWareHouse.Controllers
{
    [Area("StaffWareHouse")]
    [RoleAuthorize(2)]
    public class InventoryController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(AppDbContext db, ILogger<InventoryController> logger)
        {
            _db = db;
            _logger = logger;
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

            // Tính số lượng đã xuất cho từng sản phẩm
            var exportedDict = _db.DetailRequests
                .Where(dr => dr.ItemRequests.Status == "Đã nhận")
                .GroupBy(dr => dr.Products.IdProduct)
                .ToDictionary(g => g.Key, g => g.Sum(dr => dr.Quantity));

            // Tính số lượng đã nhập cho từng sản phẩm
            var importedDict = _db.DetailOrders
                .Where(do1 => do1.Orders.Status == "Đã nhận hàng")
                .GroupBy(do1 => do1.Products.IdProduct)
                .ToDictionary(g => g.Key, g => g.Sum(do1 => do1.Quantity));

            ViewBag.ProductExported = exportedDict;
            ViewBag.ProductImported = importedDict;

            return View(listProduct);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        public IActionResult Import(int? CategoryRequest)
        {
            var category = _db.Categories.ToList();
            ViewBag.CategoryRequest = category;
            ViewBag.SelectedCategory = CategoryRequest;
            List<Product> listProduct = null;
            if (CategoryRequest.HasValue)
            {
                listProduct = _db.Products
                    .Where(p => p.Categorys != null && p.Categorys.IdCategory == CategoryRequest.Value)
                    .ToList();
            }
            // Nếu chưa chọn hạng mục, listProduct sẽ là null
            return View("Import", listProduct);
        }
        [HttpPost]
        public IActionResult AddOrder(IFormCollection f)
        {
            var selectedProducts = f["SelectedProducts"];
            if (selectedProducts.Count == 0)
            {
                TempData["Error"] = "Bạn chưa chọn sản phẩm nào!";
                return RedirectToAction("Import");
            }

            // Lấy Supplier từ sản phẩm đầu tiên
            int firstProductId = int.Parse(selectedProducts[0]);
            var firstProduct = _db.Products.Include(p => p.Suppliers).FirstOrDefault(p => p.IdProduct == firstProductId);
            if (firstProduct == null || firstProduct.Suppliers == null)
            {
                TempData["Error"] = "Không tìm thấy nhà cung cấp!";
                return RedirectToAction("Import");
            }
            var supplier = firstProduct.Suppliers;

            var order = new Order
            {
                OrderDate = DateTime.Now,
                Status = "Chờ thanh toán",
                Suppliers = supplier,
                TotalAmount = 0
            };
            _db.Orders.Add(order);
            _db.SaveChanges();

            decimal totalAmount = 0;

            foreach (var productIdStr in selectedProducts)
            {
                int productId = int.Parse(productIdStr);
                int quantity = int.Parse(f[$"Quantity_{productId}"]);
                var product = _db.Products.FirstOrDefault(p => p.IdProduct == productId);
                if (product == null) continue;
                decimal price = product.Price ?? 0;

                var detail = new DetailOrder
                {
                    Orders = order,
                    Products = product,
                    Quantity = quantity,
                    Price = price
                };
                _db.DetailOrders.Add(detail);

                totalAmount += price * quantity;
            }

            order.TotalAmount = totalAmount;
            _db.SaveChanges();

            TempData["Success"] = "Tạo đơn hàng thành công!";
            return RedirectToAction("Index");
        }

        public IActionResult Order(){
            var order = _db.Orders.Include(o => o.Suppliers).Include(o => o.DetailOrders).ToList();
            return View(order);
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

            // Chỉ cho phép cập nhật nếu trạng thái là "Đang giao"
            if (order.Status != "Đang giao")
                return BadRequest("Chỉ được cập nhật khi trạng thái là 'Đang giao'");

            order.Status = newStatus;
            _db.SaveChanges();
            return RedirectToAction("Order");
        }


        public IActionResult InventoryOut()
        {
            var requests = _db.ItemRequests
                .Include(x => x.Users)
                .Where(x => x.IsApproved == true)
                .OrderByDescending(x => x.RequestDate)
                .ToList();
            return View(requests);
        }

        [HttpGet]
        public IActionResult GetInventoryOutDetail(int id)
        {
            var request = _db.ItemRequests.FirstOrDefault(x => x.IdItemRequest == id);
            if (request == null)
                return NotFound();

            var details = _db.DetailRequests
                .Where(dr => dr.ItemRequests.IdItemRequest == id)
                .Include(dr => dr.Products)
                .Select(d => new {
                    productName = d.Products.NameProduct,
                    quantity = d.Quantity,
                }).ToList();

            return Json(new {
                isExported = request.Status == "Đã xuất kho", 
                items = details
            });
        }

        [HttpPost]
        [Route("StaffWareHouse/Inventory/UpdateInventoryOutStatus")]
        public IActionResult UpdateInventoryOutStatus(int id)
        {
            var request = _db.ItemRequests.FirstOrDefault(x => x.IdItemRequest == id);
            if (request == null)
                return NotFound();

            if (request.Status == "Đã nhận")
                return BadRequest("Phiếu đã được xuất kho!");

            request.Status = "Đã nhận";
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}