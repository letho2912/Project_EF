using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_EF.Data;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Project_EF.Controllers
{
    public class UserController : Controller
    {
        private readonly Connect _db;
        public UserController(Connect db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Id,username,fullname,email,phone,password,status")] User user)
        {
            if (ModelState.IsValid)
            {
                _db.Add(user);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User us = _db.User.Where(s => s.username == user.username && s.password == user.password).FirstOrDefault();
                if (us != null)
                {
                    HttpContext.Session.SetString("displayname", us.fullname);
                    HttpContext.Session.SetString("userId", us.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Đăng nhập thất bại";
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
