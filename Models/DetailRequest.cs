using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class DetailRequest
    {
        [Key]
        public int IdDetailRequest { get; set; }
        public int Quantity { get; set; }

        public virtual ItemRequest ItemRequests { get; set; }
        public virtual Product Products { get; set; }
    }
}