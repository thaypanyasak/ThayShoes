using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TMDTFINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DonHangController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var hoadon = _db.HoaDon.Include(hd => hd.ApplicationUser).ToList();

            return View(hoadon);
        }

        public IActionResult ChiTietDonHang(int id)
        {
            var chiTietHoaDon = _db.ChiTietHoaDon.Include(ct => ct.SanPham).Where(ct => ct.HoaDonId == id).ToList();
            return View(chiTietHoaDon);
        }
    }
}
