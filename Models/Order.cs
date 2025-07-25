using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public decimal? TotalAmount { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public virtual Supplier Suppliers { get; set; }

        public virtual ICollection<DetailOrder> DetailOrders { get; set; } = new HashSet<DetailOrder>();
    }
}