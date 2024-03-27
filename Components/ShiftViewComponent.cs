using CheckListJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckListJob.Components
{
    [Authorize]
    public class ShiftViewComponent : ViewComponent
    {
        private readonly CheckListContext listContext = new();


        public IViewComponentResult Invoke()
        {
            ViewBag.Shifts = listContext.Shifts;
            return View("ShiftInvoke");
        }
    }
}
