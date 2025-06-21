using System.ComponentModel.DataAnnotations;

public class Role
{
    [Key]
    public int IdRole { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Salary { get; set; }

    public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
}