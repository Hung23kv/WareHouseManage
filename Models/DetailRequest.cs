using System.ComponentModel.DataAnnotations;

public class DetailRequest
{
    [Key]
    public int IdDetailRequest { get; set; }
    [Required]
    public int ItemQuantity { get; set; }

    public virtual ItemRequest ItemRequests { get; set; }
    public virtual Product Products { get; set; }
}