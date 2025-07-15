using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class Supplier
    {
        [Key]
        public int IdSupplier { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Contact { get; set; }
        public string Description { get; set; }
        [Required]
        public string Password { get; set; }

        public string Account { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    }
}