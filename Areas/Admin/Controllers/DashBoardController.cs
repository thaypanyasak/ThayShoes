using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Data;
using TMDTFINAL.Models;

namespace TMDTFINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashBoardController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.SanPham = _db.SanPham.ToList();
            int totalOrders = CalculateTotalOrders();
            int totalProducts = CalculateTotalProducts();
            int totalAccounts = CalculateTotalAccounts();
            

            // Tính toán doanh thu theo tháng và theo sản phẩm
            double totalRevenue = CalculateTotalRevenue(); // Calculate total revenue
            var productRevenue = CalculateProductRevenue();

            var dashboardViewModel = new DashBoardViewModel
            {
                TotalOrders = totalOrders,
                TotalProducts = totalProducts,
                TotalAccounts = totalAccounts,
                TotalRevenue = totalRevenue, 
                ProductRevenue = productRevenue
            };

            return View(dashboardViewModel);
        }

        private double CalculateTotalRevenue()
        {
            // Get all invoices
            var allInvoices = _db.HoaDon.ToList();

            // Calculate total revenue from all invoices
            var totalRevenue = allInvoices.Sum(invoice => invoice.Total);

            return totalRevenue;
        }
        private Dictionary<string, (double TotalRevenue, int Top)>
            CalculateProductRevenue()
        {
            // Lấy danh sách tất cả các chi tiết hóa đơn từ cơ sở dữ liệu
            var allChiTietHoaDon = _db.ChiTietHoaDon.ToList();

            // Tính tổng doanh thu cho từng sản phẩm bằng cách sử dụng LINQ to Objects
            var productRevenue = allChiTietHoaDon
                .GroupBy(cthd => cthd.SanPhamId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalRevenue = group.Sum(cthd => cthd.ThanhTien)
                })
                .ToList(); // Chuyển kết quả sang danh sách

            // Tạo một danh sách để lưu thông tin sản phẩm và doanh thu
            var productRevenueInfo = new Dictionary<string, (double TotalRevenue, int Top)>();

            // Sắp xếp danh sách sản phẩm theo doanh thu giảm dần
            var sortedProducts = productRevenue.OrderByDescending(x => x.TotalRevenue);

            // Lặp qua danh sách sản phẩm để gán Top cho sản phẩm
            int topCount = 1;
            foreach (var product in sortedProducts)
            {
                // Lấy tên sản phẩm từ ID
                var productName = _db.SanPham.Find(product.ProductId)?.Name ?? "Unknown";

                // Gán thông tin vào danh sách productRevenueInfo
                productRevenueInfo.Add(productName, (product.TotalRevenue, topCount));

                // Tăng biến topCount lên 1 để đánh số TOP
                topCount++;

                // Điều kiện dừng sau khi gán TOP 3
                if (topCount > 3)
                    break;
            }

            return productRevenueInfo;
        }



        private int CalculateTotalOrders()
        {
            var donhang = _db.ChiTietHoaDon.Count(); // _dbContext là đối tượng DbContext của Entity Framework

            return donhang;
        }

        private int CalculateTotalProducts()
        {
            var products = _db.SanPham.Count(); // _dbContext là đối tượng DbContext của Entity Framework

            return products;
        }


        private int CalculateTotalAccounts()
        {
            var account = _db.Users.Count(); // _dbContext là đối tượng DbContext của Entity Framework

            return account;
        }

    }
}
