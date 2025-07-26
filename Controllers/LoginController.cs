using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WareHouse.Models;

namespace WareHouse.Controllers;

public class LoginController : Controller
{
    private readonly AppDbContext _db;
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string loginType, string email, string password, string account, string supplierPassword)
    {
        if (loginType == "user")
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email && u.UserPassword == password && u.IsActive);
            if (user != null)
            {
                HttpContext.Session.SetInt32("idUser", user.IdUser);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetInt32("idRole", user.RolesidRole);
                // Điều hướng theo role
                switch (user.RolesidRole)
                {
                    case 1:
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    case 2:
                        return RedirectToAction("Index", "Home", new { area = "StaffWareHouse" });
                    case 3:
                        return RedirectToAction("Index", "Home", new { area = "Manager" });
                    case 4:
                    case 5:
                        return RedirectToAction("Index", "Home", new { area = "Staff" });
                }
            }
            ViewBag.Error = "Email hoặc mật khẩu không đúng, hoặc tài khoản bị khóa.";
            return View();
        }
        else if (loginType == "supplier")
        {
            var supplier = _db.Suppliers.FirstOrDefault(s => s.Account == account && s.Password == supplierPassword);
            if (supplier != null)
            {
                HttpContext.Session.SetInt32("idSupplier", supplier.IdSupplier);
                return RedirectToAction("Index", "Home", new { area = "Suppier" });
            }
            ViewBag.Error = "Tài khoản hoặc mật khẩu nhà cung cấp không đúng.";
            return View();
        }
        ViewBag.Error = "Vui lòng chọn loại đăng nhập và nhập đầy đủ thông tin.";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        // Hiển thị form quên mật khẩu (chưa xử lý logic gửi mail)
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
