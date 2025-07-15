using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        [Required]
        [MaxLength(255)]
        public string NameCategory { get; set; }

        public string? Description { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>(); 
    }
}