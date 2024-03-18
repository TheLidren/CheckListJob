using CheckListJob.Models;
using CheckListJob.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CheckListJob.Controllers
{
    public class ShiftController : Controller
    {
        private readonly CheckListContext listContext = new();
        public IActionResult ListShift(short shiftId)
        {
            List<ListShift> listShifts = listContext.ListShifts.Where(a => a.ShiftId == shiftId).ToList();
            return View(listShifts);
        }

        [HttpGet]
        public IActionResult CreateList()
        {
            ViewBag.Shifts = new SelectList(listContext.Shifts, "Id", "Tittle");
            return View();
        }

        [HttpPost]
        public IActionResult CreateList(ListShift listShift)
        {
            ViewBag.Shifts = new SelectList(listContext.Shifts, "Id", "Tittle", listShift.ShiftId); ;
            if (listShift.StartTime > listShift.FinishTime)
                ModelState.AddModelError("FinishTime", "Время окончания указано неверно");
            if (ModelState.IsValid)
            {
                listShift.Status = true;
                listContext.ListShifts.Add(listShift);
                listContext.SaveChanges();
                return RedirectToAction("CreateList");
            }
            return View(listShift);
        }

        [HttpGet]
        public ActionResult EditShift(short shiftId)
        {
            ListShift listShift = listContext.ListShifts.Find(shiftId);
            ViewBag.Shifts = new SelectList(listContext.Shifts, "Id", "Tittle");
            return View(listShift);
        }

        [HttpPost]
        public ActionResult EditShift(ListShift listShift)
        {
            ViewBag.Shifts = new SelectList(listContext.Shifts, "Id", "Tittle", listShift.ShiftId); ;
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
            ListShift listShift = listContext.ListShifts.Find(shiftId);
            listShift.Status = listShift.Status ? false : true;
            listContext.SaveChanges();
            return RedirectToAction("ListShift", new { shiftId = listShift.ShiftId });
        }


    }
}
