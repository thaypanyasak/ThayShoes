using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMDTFINAL.Models
{
    public class Mess
    {
        public int Id { get; set; }
        public int RoomID { get; set; }
        [ForeignKey("RoomID")]
        [ValidateNever]
        public Room Room { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public string nguoinhan { get; set; }
    }
}
