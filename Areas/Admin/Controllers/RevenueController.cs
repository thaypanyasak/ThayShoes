using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMDTFINAL.Data;
using TMDTFINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace TMDTFINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RevenueController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RevenueController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Tính toán doanh thu theo tháng

            List<string> topProducts;
            var monthlyRevenue = CalculateMonthlyRevenue();

            // Tính toán doanh thu theo thể loại
            var productRevenue = CalculateProductRevenue(out topProducts);

            var totalRevenue = CalculateTotalRevenue();

            var productsSoldInMonth = CalculateProductsSoldInMonth();

            var productPrices = GetProductPrices(productsSoldInMonth);

            // Đưa dữ liệu vào view model
            var revenueViewModel = new RevenueViewModel
            {
                MonthlyRevenue = monthlyRevenue,
                ProductRevenue = productRevenue,
                TotalRevenue = totalRevenue,
                ProductsSoldInMonth = productsSoldInMonth,
                ProductPrices = productPrices,
                TopProducts = topProducts
            };

            return View(revenueViewModel);
        }
        private Dictionary<string, double> GetProductPrices(Dictionary<string, Dictionary<string, double>> productsSoldInMonth)
        {
            var productPrices = new Dictionary<string, double>();

            foreach (var monthEntry in productsSoldInMonth)
            {
                foreach (var productEntry in monthEntry.Value)
                {
                    var productName = productEntry.Key;
                    var productPrice = _db.SanPham.FirstOrDefault(p => p.Name == productName)?.Price ?? 0;

                    if (!productPrices.ContainsKey(productName))
                    {
                        productPrices.Add(productName, productPrice);
                    }
                }
            }

            return productPrices;
        }

        private double CalculateTotalRevenue()
        {
            // Get all invoices
            var allInvoices = _db.HoaDon.ToList();

            // Calculate total revenue from all invoices
            var totalRevenue = allInvoices.Sum(invoice => invoice.Total);

            return totalRevenue;
        }

        private Dictionary<string, double> CalculateMonthlyRevenue()
        {
            // Tạo một danh sách chứa các tháng và doanh thu tương ứng ban đầu bằng 0
            var monthlyRevenue = new Dictionary<string, double>();
            for (int month = 1; month <= 12; month++)
            {
                monthlyRevenue.Add(new DateTime(1, month, 1).ToString("MMMM"), 0);
            }

            // Truy vấn cơ sở dữ liệu để lấy thông tin đơn hàng theo tháng và tính tổng doanh thu
            var actualMonthlyRevenue = _db.HoaDon
                .GroupBy(hd => hd.OrderDate.Month) // Nhóm đơn hàng theo tháng
                .Select(group => new
                {
                    Month = group.Key,
                    TotalRevenue = group.Sum(hd => hd.Total) // Tính tổng doanh thu cho mỗi tháng
                })
                .ToList(); // Chuyển kết quả sang danh sách

            // Cập nhật giá trị doanh thu thực tế cho các tháng có hóa đơn
            foreach (var item in actualMonthlyRevenue)
            {
                monthlyRevenue[new DateTime(1, item.Month, 1).ToString("MMMM")] = item.TotalRevenue;
            }

            return monthlyRevenue;
        }

        private Dictionary<string, double> CalculateProductRevenue(out List<string> topProducts)
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
                .ToList() // Chuyển kết quả sang danh sách
                .ToDictionary(x => _db.SanPham.Find(x.ProductId)?.Name ?? "Unknown", x => x.TotalRevenue);

            // Sắp xếp danh sách theo doanh thu giảm dần
            var sortedProductRevenue = productRevenue.OrderByDescending(x => x.Value).ToList();

            // Lấy các sản phẩm hàng đầu
            topProducts = sortedProductRevenue.Take(4).Select(x => x.Key).ToList();

            return productRevenue;
        }



        private Dictionary<string, Dictionary<string, double>> CalculateProductsSoldInMonth()
        {
            // Tạo một danh sách chứa số lượng sản phẩm được bán trong mỗi tháng
            var productsSoldInMonth = new Dictionary<string, Dictionary<string, double>>();

            // Truy vấn cơ sở dữ liệu để lấy thông tin về số lượng sản phẩm được bán trong mỗi tháng
            var soldProducts = _db.ChiTietHoaDon
                .Include(cthd => cthd.HoaDon)
                .Where(cthd => cthd.HoaDon.OrderDate.Year == DateTime.Now.Year) // Lọc theo năm hiện tại
                .GroupBy(cthd => new { Month = cthd.HoaDon.OrderDate.Month, ProductId = cthd.SanPhamId })
                .Select(group => new
                {
                    Month = group.Key.Month,
                    ProductId = group.Key.ProductId,
                    Quantity = group.Sum(cthd => cthd.Quantity)
                })
                .ToList();

            // Lặp qua kết quả để cập nhật vào danh sách productsSoldInMonth
            foreach (var item in soldProducts)
            {
                var productName = _db.SanPham.Find(item.ProductId)?.Name ?? "Unknown";
                var monthName = new DateTime(1, item.Month, 1).ToString("MMMM");

                if (!productsSoldInMonth.ContainsKey(monthName))
                {
                    productsSoldInMonth[monthName] = new Dictionary<string, double>();
                }

                if (!productsSoldInMonth[monthName].ContainsKey(productName))
                {
                    productsSoldInMonth[monthName][productName] = item.Quantity;
                }
                else
                {
                    productsSoldInMonth[monthName][productName] += item.Quantity;
                }
            }

            return productsSoldInMonth;
        }










    }
}
