using CheckListJob.Models;

namespace CheckListJob.ViewModels
{
    public class ListShiftViewModel
    {
        public Models.ShiftTask ListShift { get; set; }
        public IEnumerable<Shift> Shifts { get; set;}
    }
}
