using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TMDTFINAL.Data;
using TMDTFINAL.Models;
using Microsoft.EntityFrameworkCore;

namespace TMDTFINAL.Areas.User.Controllers
{
    [Area("User")]
    public class UserMagementController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserMagementController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var inden = (ClaimsIdentity)User.Identity;
                var datauser = inden.FindFirst(ClaimTypes.NameIdentifier);
                var tk = datauser.Value;

                var lichsumuahang = _db.HoaDon.Where(x => x.ApplicationUserId == tk).ToList();
                ViewBag.lichsumuahang = lichsumuahang;

                ViewBag.data = _db.ApplicationUser.FirstOrDefault(x => x.Id == tk);
                var data = _db.HoaDon.Where(x => x.ApplicationUserId == tk);
                ViewBag.choxacnhan = data.Where(x => x.OrderStatus == "Đang Xác Nhận").ToList();
                ViewBag.cholayhang = data.Where(x => x.OrderStatus == "Chờ Lấy Hàng").ToList();
                ViewBag.danggiao = data.Where(x => x.OrderStatus == "Đang Giao").ToList();
                ViewBag.danhgia = _db.lshoadons.Where(x => x.Applicationuserid == tk).ToList();
                return View();

            }
            return Redirect("/Identity/Account/Login");
        }
        public IActionResult ChangeProfile(string iduser, IFormFile file)
        {

            var a = _db.ApplicationUser.FirstOrDefault(x => x.Id == iduser);
            if (file != null && file.Length > 0)
            {
                string folderPath = Path.Combine("wwwroot/profile/", file.FileName);

                a.ImageUrl = Url.Content("~/profile/" + file.FileName);
                //updatedanhgia.img = Url.Content("~/uploads/" + file.FileName);


                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            _db.ApplicationUser.Update(a);
            //_db.danhgia.Update(updatedanhgia);

            _db.SaveChanges();
            return Json(new { success = true });

        }
        public IActionResult ChangeUser(string Name, string Email, string phone, string Address)
        {
            var inden = (ClaimsIdentity)User.Identity;
            var datauser = inden.FindFirst(ClaimTypes.NameIdentifier);
            var tk = datauser.Value;

            var data = _db.ApplicationUser.FirstOrDefault(x => x.Id == tk);
            if (Name != null)
            {
                data.Name = Name;
                _db.Update(data);
            }
            if (Email != null)
            {
                data.Email = Email;
                _db.Update(data);
            }
            if (phone != null)
            {
                data.PhoneNumber = phone;
                _db.Update(data);
            }
            if (Address != null)
            {
                data.Address = Address;
                _db.Update(data);

            }
            _db.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult getchitiet(int id)
        {
            var a = _db.ChiTietHoaDon.Include("SanPham").Where(x => x.HoaDonId == id).ToList();
            return Json(a);
        }
    }
}
