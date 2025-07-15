using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class DelayOrder
    {
        [Key]
        public int IdDelayOrder { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public DateTime DelayDate { get; set; }

        public virtual Order Orders { get; set; }
    }
}