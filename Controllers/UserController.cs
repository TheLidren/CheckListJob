using CheckListJob.Models;
using CheckListJob.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CheckListJob.Controllers
{
    public class UserController : Controller
    {
        private readonly CheckListContext listContext = new();



        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult ListUser()
        {
            return View(listContext.Users.Include(r => r.Role).ToList());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteUser(int userId)
        {
            User user = listContext.Users.Find(userId);
            user.Status = user.Status ? false : true;
            listContext.SaveChanges();
            return RedirectToAction("ListUser");
        }

        [HttpGet] //Авторизация
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {
            string pas = user.Password.ToSHA256String();
            User userCheck = listContext.Users.Where(u => u.Email == user.Email && u.Password == user.Password.ToSHA256String()).Include(r => r.Role).FirstOrDefault();
            if (userCheck == null)
            {
                ModelState.AddModelError("Email, Password", "Проверьте корректность ввода данных");
                return View(user);
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userCheck.Email), new Claim(ClaimTypes.Role, userCheck.Role.Name) };
            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("WelcomeIndex", "Home");
        }

        [HttpGet] //Регистрация
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost] //Регистрация
        public IActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                User userNew = listContext.Users.Where(m => m.Email == user.Email).FirstOrDefault();
                if (userNew != null)
                {
                    ModelState.AddModelError("Email", "Данный email уже зарегестрирован в системе");
                    return View(user);
                }
                user.Status = true; user.RoleId = listContext.Roles.Where(m => m.Name == "user").FirstOrDefault().Id; user.Password = user.Password.ToSHA256String();
                listContext.Users.Add(user);
                listContext.SaveChanges();
                return RedirectToAction("SignIn");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("SignIn");
        }
    }
}
