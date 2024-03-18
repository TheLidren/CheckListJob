using CheckListJob.Models;
using CheckListJob.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CheckListJob.Controllers
{
    public class HomeController : Controller
    {
        private readonly CheckListContext listContext = new();
        public IActionResult WelcomeIndex()
        {
            return View();
        }

    }
}
