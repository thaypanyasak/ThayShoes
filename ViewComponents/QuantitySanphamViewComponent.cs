using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Data;
using TMDTFINAL.Models;

namespace TMDTFINAL.ViewComponents
{
    public class QuantitySanphamViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public QuantitySanphamViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke(GioHang gioHangItem)
        {
            return View(gioHangItem);
        }
    }
}
