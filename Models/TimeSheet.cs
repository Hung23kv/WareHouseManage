using System.ComponentModel.DataAnnotations;

public class TimeSheet
{
    [Key]
    public int IdTimeSheet { get; set; }
    [Required]
    public bool IsPresent { get; set; }
    [Required]
    public DateTime WorkingDate { get; set; }
    public string Note { get; set; }

    public virtual User Users { get; set; }

}