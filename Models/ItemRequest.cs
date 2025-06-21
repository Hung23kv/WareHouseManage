using System.ComponentModel.DataAnnotations;

public class ItemRequest
{
    [Key]
    public int idItemRequest { get; set; }
    [Required]
    public string Purpose { get; set; }
    [Required]
    public bool IsApproved { get; set; }

    public virtual User Users { get; set; }

    public virtual ICollection<DetailRequest> DetailRequests { get; set; } = new HashSet<DetailRequest>();

}