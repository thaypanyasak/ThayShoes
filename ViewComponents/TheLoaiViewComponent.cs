using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Data;

namespace TMDTFINAL.ViewComponents
{
    public class TheLoaiViewComponent : ViewComponent
    {

        public readonly ApplicationDbContext _db;
        public TheLoaiViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var theloai = _db.TheLoai.ToList();
            return View(theloai);
        }
    }
}
