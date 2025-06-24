using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public int IdProduct { get; set; }
    [Required]
    [MaxLength(255)]
    public string NameProduct { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    [Required]
    [MaxLength(30)]
    public string Unit { get; set; }
    public decimal? Price { get; set; }
    [Required, Range(0, 10000)]
    public int remainingQuantity { get; set; }

    public virtual Category Categorys { get; set; }
    public virtual Supplier Suppliers { get; set; }

    public virtual ICollection<DetailOrder> DetailOrders { get; set; } = new HashSet<DetailOrder>();
    public virtual ICollection<OutHistory> OutHistories { get; set; } = new HashSet<OutHistory>();

    public virtual ICollection<DetailRequest> DetailRequests { get; set; } = new HashSet<DetailRequest>();
}