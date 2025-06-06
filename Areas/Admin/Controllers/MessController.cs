using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TMDTFINAL.Data;
using TMDTFINAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace TMDTFINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessController : Controller
    {

        public readonly ApplicationDbContext _db;
        public MessController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var a = _db.Room.ToList();
            return View(a);
        }
        [HttpGet]
        public IActionResult mess(int id)
        {

            var inden = (ClaimsIdentity)User.Identity;
            var datauser = inden.FindFirst(ClaimTypes.NameIdentifier);
            var tk = datauser.Value;
            ViewBag.Id = id;
            ViewBag.tk = tk;
            var a = _db.mess.Where(x => x.RoomID == id).ToList();
            return View(a);
        }
        [HttpPost]
        public IActionResult Mess(string content, int idroom)
        {
            var inden = (ClaimsIdentity)User.Identity;
            var datauser = inden.FindFirst(ClaimTypes.NameIdentifier);
            var tk = datauser.Value;
            var a = _db.Room.SingleOrDefault(room => room.user == tk);

            var mess = new Mess
            {
                date = DateTime.Now,
                content = content,
                RoomID = idroom,
                nguoinhan = tk

            };
            _db.mess.Add(mess);
            //}
            _db.SaveChanges();
            return Json(new { success = true });
        }
    }
}
