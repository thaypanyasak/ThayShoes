using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMDTFINAL.Data;
using TMDTFINAL.Models;
using X.PagedList;

namespace TMDTFINAL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class HoaDonController : Controller
    {
        public readonly ApplicationDbContext _db;
        public HoaDonController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 8;
            if (page < 1)
            {
                page = 1;
            }
            List<HoaDon> hoadon = _db.HoaDon.Where(x => x.OrderStatus != "Thành Công").ToList();

            IPagedList<HoaDon> listhoadon = hoadon.ToPagedList(page, pageSize);
            return View(listhoadon);
        }
        public IActionResult chitiethoadon(int id)
        {
            int page = 1;
            int pageSize = 8;
            if (page < 1)
            {
                page = 1;
            }
            var data = _db.ChiTietHoaDon.Include("SanPham").Where(x => x.HoaDonId == id).ToList();
            IPagedList<ChiTietHoaDon> listhoadon = data.ToPagedList(page, pageSize);
            return View(listhoadon);

        }
        public IActionResult updatetrangthai(string trangthai, int id)
        {
            var data = _db.HoaDon.Find(id);
            if (data != null)
            {
                if (trangthai == "Thành Công")
                {
                    lshoadon ls = new lshoadon
                    {
                        Applicationuserid = data.ApplicationUserId,
                        mahoadon = data.Id,
                        OrderDate = DateTime.Now,
                        total = data.Total
                    };
                    _db.lshoadons.Add(ls);
                    data.OrderStatus = trangthai;
                    _db.HoaDon.Update(data);
                }
                else
                {
                    data.OrderStatus = trangthai;
                    _db.HoaDon.Update(data);

                }

                _db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
