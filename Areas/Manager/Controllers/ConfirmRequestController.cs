using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace WareHouse.Areas.Manager.Controllers
{
    [Area("Manager")]
    [RoleAuthorize(3)]
    public class ConfirmRequestController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ConfirmRequestController> _logger;

        public ConfirmRequestController(ILogger<ConfirmRequestController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var Request = _db.ItemRequests
                .Include(x => x.Users)
                .OrderByDescending(x => x.RequestDate)
                .ToList();
            return View(Request);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        
        [HttpGet]
        public IActionResult GetRequestDetail(int id)
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
                isApproved = request.IsApproved,
                items = details
            });
        }

        
        [HttpPost]
        [Route("Manager/ConfirmRequest/ApproveRequest")]
        public IActionResult ApproveRequest(int id)
        {
            var request = _db.ItemRequests.FirstOrDefault(x => x.IdItemRequest == id);
            if (request == null)
                return NotFound();

            request.IsApproved = true;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}