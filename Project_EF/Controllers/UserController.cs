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
        public async Task<IActionResult> Register()
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

    }
}
