using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Data;
using TMDTFINAL.Models;

namespace TMDTFINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminDashBoardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminDashBoardController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index(string selectedMenuItem)
        {

            var model = new DashBoardViewModel
            {
                SelectedMenuItem = selectedMenuItem,
                Users = _db.Users.ToList(),
                SanPhams = _db.SanPham.ToList(),
                TheLoais = _db.TheLoai.ToList(),
                NhaCungCaps = _db.NhaCungCap.ToList(),
                Blogs = _db.Blogs.ToList(),
                Rooms = _db.Room.ToList(),
                ProductWarehouses = _db.ProductWarehouse.ToList(),
                HoaDons = _db.HoaDon.ToList(),


                // Load data for other items as needed
            };

            return View(model);
        }
    }
}
