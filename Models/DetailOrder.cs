using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class DetailOrder
    {
        [Key]
        public int IdDetailOrder { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Order Orders { get; set; }
        public virtual Product Products { get; set; }
    }
}