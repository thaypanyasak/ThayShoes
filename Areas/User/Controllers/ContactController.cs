using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TMDTFINAL.Data;
using TMDTFINAL.Models;

namespace TMDTFINAL.Areas.User.Controllers
{
    [Area("User")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactController(ApplicationDbContext db)
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
                var a = _db.Room.SingleOrDefault(x => x.user == tk);
                ViewBag.user = tk;
                ViewBag.datauer = _db.ApplicationUser.SingleOrDefault(x => x.Id == tk);
                ViewBag.dataad = _db.ApplicationUser.SingleOrDefault(x => x.Id == "05cad685-43fa-47cf-89ea-1ec622607996");
                if (a != null)
                {
                    List<Mess> messages = _db.mess.Where(x => x.RoomID == a.ID).ToList();
                    return View(messages);
                }
                return View();
            }
            else
            {
                // Người dùng chưa đăng nhập, hãy chuyển hướng họ đến trang đăng nhập
                return Redirect("/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Mess(string content)
        {
            var inden = (ClaimsIdentity)User.Identity;
            var datauser = inden.FindFirst(ClaimTypes.NameIdentifier);
            var tk = datauser.Value;
            var a = _db.Room.SingleOrDefault(room => room.user == tk);
            if (a == null)
            {
                var room = new Room
                {
                    user = tk,
                    name = datauser.Subject.Name,
                    img = "https://tse2.mm.bing.net/th?id=OIP.TJnee9m8snDXdz-0UjFC6gHaHa&pid=Api&P=0&h=220",
                    date = DateTime.Now,
                };
                _db.Room.Add(room);
                _db.SaveChanges();
                var mess = new Mess
                {
                    date = DateTime.Now,
                    content = content,
                    RoomID = room.ID,
                    nguoinhan = tk
                };
                _db.mess.Add(mess);
            }
            else
            {
                var mess = new Mess
                {
                    date = DateTime.Now,
                    content = content,
                    RoomID = a.ID,
                    nguoinhan = tk

                };
                _db.mess.Add(mess);
            }
            _db.SaveChanges();
            return Json(new { success = true });
        }
        public IActionResult chat()
        {
            return View();
        }
    }
}
