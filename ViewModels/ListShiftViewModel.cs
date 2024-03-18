using CheckListJob.Models;

namespace CheckListJob.ViewModels
{
    public class ListShiftViewModel
    {
        public ListShift ListShift { get; set; }
        public IEnumerable<Shift> Shifts { get; set;}
    }
}
