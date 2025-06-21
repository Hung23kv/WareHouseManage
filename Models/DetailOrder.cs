using System.ComponentModel.DataAnnotations;

public class DetailOrder
{
    [Key]
    public int IdDetailOrder { get; set; }
    [Required, Range(1, 10000)]
    public int Quantity { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Status { get; set; }

    public virtual Order Orders { get; set; }
    public virtual Product Products { get; set; }

}