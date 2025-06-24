using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    public int IdOder { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    [Required]
    public string StatusOrder { get; set; }

    [Required]
    public string? Note { get; set; }

    public virtual Supplier Suppliers { get; set; }
    public virtual ICollection<DetailOrder> DetailOrders { get; set; } = new HashSet<DetailOrder>();

    public virtual ICollection<DelayOrder> DelayOrders { get; set; } = new HashSet<DelayOrder>();
}