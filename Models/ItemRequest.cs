using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class ItemRequest
    {
        [Key]
        public int IdItemRequest { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public bool? IsApproved { get; set; }

        public virtual User Users { get; set; }

        public virtual ICollection<DetailRequest> DetailRequests { get; set; } = new HashSet<DetailRequest>();
    }
}