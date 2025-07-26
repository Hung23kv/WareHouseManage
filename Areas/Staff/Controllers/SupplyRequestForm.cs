using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore; // Thêm dòng này ở đầu file nếu chưa có

namespace WareHouse.Areas.Staff.Controllers
{
    [Area("Staff")]
    [RoleAuthorize(4,5)]
    public class SupplyRequestFormController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<SupplyRequestFormController> _logger;

        public SupplyRequestFormController(ILogger<SupplyRequestFormController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("idUser");
            if (userId == null)
            {
                // Handle missing session (not logged in)
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            // Lấy tất cả các ItemRequest của user id = userId và status = "Chưa xác nhận", kèm theo các DetailRequest và mã sản phẩm
            var supplyRequestForms = _db.ItemRequests
                .Include(ir => ir.Users) 
                .Where(ir => ir.Users.IdUser == userId.Value && ir.Status == "Chưa xác nhận")
                .Select(ir => new {
                    ItemRequest = ir,   
                    DetailRequests = ir.DetailRequests.Select(dr => new {
                        dr.IdDetailRequest,
                        dr.Quantity,
                        ProductId = dr.Products.IdProduct,
                        ProductName = dr.Products.NameProduct
                    }).ToList()
                }).ToList();
            
            return View(supplyRequestForms);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}