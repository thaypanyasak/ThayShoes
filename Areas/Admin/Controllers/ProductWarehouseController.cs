using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Data;
using TMDTFINAL.Models;

namespace TMDTFINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductWarehouseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductWarehouseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var productsInWarehouse = _db.ProductWarehouse.ToList();
            return View(productsInWarehouse);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductWarehouse productWarehouse)
        {
            if (ModelState.IsValid)
            {
                _db.ProductWarehouse.Add(productWarehouse);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productWarehouse);
        }
        public IActionResult Edit(int id)
        {
            var productWarehouse = _db.ProductWarehouse.Find(id);
            if (productWarehouse == null)
            {
                return NotFound();
            }
            return View(productWarehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductWarehouse productWarehouse)
        {
            if (ModelState.IsValid)
            {
                _db.ProductWarehouse.Update(productWarehouse);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productWarehouse);
        }
        public IActionResult Detail(int id)
        {
            var productWarehouse = _db.ProductWarehouse.Find(id);
            if (productWarehouse == null)
            {
                return NotFound();
            }
            return View(productWarehouse);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var productWarehouse = _db.ProductWarehouse.Find(id);
            if (productWarehouse == null)
            {
                return NotFound();
            }

            _db.ProductWarehouse.Remove(productWarehouse);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
