using CheckListJob.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckListJob.Components
{
    public class ShiftViewComponent : ViewComponent
    {
        private readonly CheckListContext listContext = new();


        public IViewComponentResult Invoke()
        {
            ViewBag.Shifts = listContext.Shifts.ToList();
            return View("ShiftInvoke");
        }
    }
}
