using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMDTFINAL.Models
{
    public class danhgia
    {
        [Key]
        public int id { get; set; }
        public int idsanpham { get; set; }
        [ForeignKey("idsanpham")]
        [ValidateNever]
        public SanPham sanpham { get; set; }

        public DateTime date { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string content { get; set; }
        public string sosao { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
