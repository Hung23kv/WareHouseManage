using System.ComponentModel.DataAnnotations;
namespace WareHouse.Models
{
    public class OutHistory
    {
        [Key]
        public int IdOutHistory { get; set; }
        [Required, Range(1, 10000)]
        public int OutQuantity { get; set; }
        public DateTime OutDate { get; set; }

        public virtual Product Products { get; set; }

    }
}