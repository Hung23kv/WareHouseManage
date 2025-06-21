using System.ComponentModel.DataAnnotations;

public class DelayOrder
{
    [Key]
    public int IdDelayOrder { get; set; }
    [Required]
    public string Reason { get; set; }
    [Required]
    public DateTime DelayDate { get; set; }

    public virtual Order Orders { get; set; }
}