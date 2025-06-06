using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMDTFINAL.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string user { get; set; }

        [ForeignKey("user")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public DateTime date { get; set; }
    }
}
