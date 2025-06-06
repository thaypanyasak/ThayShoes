using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMDTFINAL.Models
{
    public class lshoadon
    {
        [Key]
        public int Id { get; set; }
        public int mahoadon { get; set; }

        public DateTime OrderDate { get; set; }

        [ForeignKey("mahoadon")]
        [ValidateNever]
        public HoaDon hoadon { get; set; }
        public string Applicationuserid { get; set; }

        public double total { get; set; }
    }
}
