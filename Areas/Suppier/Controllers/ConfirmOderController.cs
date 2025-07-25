using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WareHouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient; // Thêm using này

namespace WareHouse.Areas.Suppier.Controllers
{
    [Area("Suppier")]
    public class ConfirmOderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ConfirmOderController> _logger;

        public ConfirmOderController(ILogger<ConfirmOderController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            // Giả sử supplier id = 1, thực tế lấy từ đăng nhập
            int supplierId = 1;
            var orders = _db.Orders
                .Include(o => o.Suppliers)
                .Include(o => o.DetailOrders)
                    .ThenInclude(d => d.Products)
                .Where(o => o.Suppliers.IdSupplier == supplierId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            return View(orders);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int id, string newStatus, DateTime? deliveryDate)
        {
            // Lấy connection string từ DbContext
            var connectionString = _db.Database.GetConnectionString();

            // Lấy trạng thái hiện tại của đơn hàng bằng ADO.NET
            string? currentStatus = null;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Status FROM Orders WHERE IdOrder = @id";
                    var pId = command.CreateParameter();
                    pId.ParameterName = "@id";
                    pId.Value = id;
                    command.Parameters.Add(pId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentStatus = reader["Status"]?.ToString();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }

            if (newStatus == "Đang giao")
            {
                if (currentStatus != "Đã thanh toán")
                {
                    TempData["Error"] = "Chỉ được chuyển sang 'Đang giao' khi đơn đã thanh toán!";
                    return RedirectToAction("Index");
                }
            }

            // Cập nhật trạng thái và ngày giao hàng (nếu có)
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    if (deliveryDate.HasValue)
                    {
                        command.CommandText = "UPDATE Orders SET Status = @status, DeliveryDate = @deliveryDate WHERE IdOrder = @id";
                        var pStatus = command.CreateParameter();
                        pStatus.ParameterName = "@status";
                        pStatus.Value = newStatus;
                        command.Parameters.Add(pStatus);

                        var pDeliveryDate = command.CreateParameter();
                        pDeliveryDate.ParameterName = "@deliveryDate";
                        pDeliveryDate.Value = deliveryDate.Value;
                        command.Parameters.Add(pDeliveryDate);

                        var pId = command.CreateParameter();
                        pId.ParameterName = "@id";
                        pId.Value = id;
                        command.Parameters.Add(pId);
                    }
                    else
                    {
                        command.CommandText = "UPDATE Orders SET Status = @status WHERE IdOrder = @id";
                        var pStatus = command.CreateParameter();
                        pStatus.ParameterName = "@status";
                        pStatus.Value = newStatus;
                        command.Parameters.Add(pStatus);

                        var pId = command.CreateParameter();
                        pId.ParameterName = "@id";
                        pId.Value = id;
                        command.Parameters.Add(pId);
                    }

                    command.ExecuteNonQuery();
                }
            }
            TempData["Success"] = "Cập nhật trạng thái thành công!";
            return RedirectToAction("Index");
        }
    }
}