﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMDTFINAL.Models
{
    public class GioHang
    {
        [Key]
        public int Id { get; set; }
        public int SanPhamId { get; set; }
        [ForeignKey("SanPhamId")]
        [ValidateNever]
        public SanPham SanPham { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public double ProductPrice { get; set; }
        public bool IsDeleted { get; set; }


        public IEnumerable<NhaCungCap> NhaCungCaps { get; set; }

        public List<ProductImage> SecondaryImages { get; set; }
    }
}
