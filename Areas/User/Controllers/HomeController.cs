using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TMDTFINAL.Data;
using TMDTFINAL.Models;
using Microsoft.EntityFrameworkCore;

namespace TMDTFINAL.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var sanPhamList = _db.SanPham.Include(sp => sp.NhaCungCap).Take(6).ToList(); // เรียกดึงผลิตภัณฑ์ 6 รายการแรก
            var blogList = _db.Blogs.OrderByDescending(b => b.CreatedDate).Take(3).ToList(); // เรียกดึงบล็อกโพสต์ 3 รายการล่าสุด

            var model = new HomeModel
            {
                SanPhamList = sanPhamList,
                BlogList = blogList
            };

            return View(model);
        }
    }
}