using System.ComponentModel.DataAnnotations;

public class OutHistory
{
    [Key]
    public int IdOutHistory { get; set; }
    [Required, Range(1, 10000)]
    public int OutQuantity { get; set; }
    public DateTime OutDate { get; set; }

    public virtual Product Products { get; set; }

}