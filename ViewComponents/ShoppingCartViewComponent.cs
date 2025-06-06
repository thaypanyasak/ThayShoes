using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Data;
using TMDTFINAL.Models;

namespace TMDTFINAL.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(GioHang gioHangItem)
        {
            return View(gioHangItem);
        }
    }
}
