using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMDTFINAL.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int HoaDonId { get; set; }
        [ForeignKey("HoaDonId")]
        [ValidateNever]
        public HoaDon HoaDon { get; set; }
        [Required]
        public int SanPhamId { get; set; }
        [ForeignKey("SanPhamId")]
        [ValidateNever]
        public SanPham SanPham { get; set; }

        public int Quantity { get; set; }
        public double ProductPrice { get; set; }

        public double ThanhTien { get { return Quantity * ProductPrice; } }
    }
}
