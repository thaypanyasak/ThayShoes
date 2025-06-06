using System.ComponentModel.DataAnnotations;

namespace TMDTFINAL.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        // Thêm khóa ngoại để liên kết với sản phẩm
        public int SanPhamId { get; set; }
        public SanPham SanPham { get; set; }


        // Thêm trường để lưu trữ URL của hình ảnh
        public string ImageUrl { get; set; }

        public bool IsMainImage { get; set; }
    }
}
