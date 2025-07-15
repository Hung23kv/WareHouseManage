using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class TimeSheet
    {
        [Key]
        public int IdTimeSheet { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }

        public virtual User Users { get; set; }
    }
}