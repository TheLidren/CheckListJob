using CheckListJob.Models;
using CheckListJob.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CheckListJob.Controllers
{
    [Authorize(Roles = "admin")]
    public class ShiftController : Controller
    {
        private readonly CheckListContext listContext = new();
        private User user = new();
        private ListLog listLog = new();
        private ShiftTask shiftTask;

        [Authorize(Roles = "admin, user")]
        public IActionResult ListShift(short shiftId)
        {
            ViewData["shiftId"] = shiftId;
            List<ShiftTask> listShifts = listContext.ShiftTasks.Where(a => a.ShiftId == shiftId && a.Status && a.LastAction.Value.Date < DateTime.Now.Date).ToList();
            return View(listShifts);
        }

        public IActionResult AdminShift()
        {
            return View(listContext.ShiftTasks.ToList());
        }


        [Authorize(Roles = "admin, user")]
        public IActionResult CompleteTask(int taskId, short shiftId)
        {
            user = listContext.Users.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            listLog = new ListLog { ShiftTaskId = taskId, UserId = user.Id, MarkAction = DateTime.Now };
            listContext.ListLogs.Add(listLog);
            shiftTask = listContext.ShiftTasks.Find(taskId);
            shiftTask.LastAction = DateTime.Now;
            listContext.Entry(shiftTask).State = EntityState.Modified;

            listContext.SaveChanges();
            return RedirectToAction("ListShift", new { shiftId });
        }

        public IActionResult JournalAction()
        {
            return View(listContext.ListLogs.Include(task => task.ShiftTask).Include(u => u.User).Include(shift => shift.ShiftTask.Shift).ToList());
        }


        [HttpGet]
        public IActionResult CreateList()
        {
            ViewBag.Shifts = new SelectList(listContext.Shifts, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateList(ShiftTask shiftTask)
        {
            ViewBag.Shifts = new SelectList(listContext.Shifts, "Id", "Name", shiftTask.ShiftId); ;
            if (shiftTask.StartTime > shiftTask.FinishTime)
                ModelState.AddModelError("FinishTime", "Время окончания указано неверно");
            if (ModelState.IsValid)
            {
                shiftTask.Status = true;
                listContext.ShiftTasks.Add(shiftTask);
                listContext.SaveChanges();
                TempData["Review"] = "Some task";
                return RedirectToAction("CreateList") ;
            }
            return View(shiftTask);
        }

        [HttpGet]
        public ActionResult EditShift(short shiftId)
        {
            ShiftTask listShift = listContext.ShiftTasks.Find(shiftId);
            ViewBag.Shifts = new SelectList(listContext.Shifts, "Id", "Name");
            return View(listShift);
        }

        [HttpPost]
        public ActionResult EditShift(ShiftTask listShift)
        {
            ViewBag.Shifts = new SelectList(listContext.Shifts, "Id", "Name", listShift.ShiftId); ;
            if (listShift.StartTime > listShift.FinishTime)
                ModelState.AddModelError("FinishTime", "Время окончания указано неверно");
            if (ModelState.IsValid)
            {
                listContext.Entry(listShift).State = EntityState.Modified;
                listContext.SaveChanges();
                return RedirectToAction("ListShift", new { shiftId = listShift.ShiftId });
            }
            return View(listShift);
        }

        [HttpGet]
        public ActionResult DeleteShift(short shiftId)
        {
            Models.ShiftTask listShift = listContext.ShiftTasks.Find(shiftId);
            listShift.Status = listShift.Status ? false : true;
            listContext.SaveChanges();
            return RedirectToAction("ListShift", new { shiftId = listShift.ShiftId });
        }


    }
}
