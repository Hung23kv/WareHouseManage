using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int RolesidRole { get; set; }

        [Required]
        public string UserPassword { get; set; }
        public string Image { get; set; }

        public virtual Role Roles { get; set; }

        public virtual ICollection<ItemRequest> ItemRequests { get; set; } = new HashSet<ItemRequest>();
        public virtual ICollection<TimeSheet> TimeSheets { get; set; } = new HashSet<TimeSheet>();
    }
}