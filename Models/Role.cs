using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Salary { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}