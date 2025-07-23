using WareHouse.Models;

namespace WareHouse.Helpers
{ 
    public class TimeKeepingVM
    {
        public List<User> Employees { get; set; }
        public List<TimeSheet> TimeSheets { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
