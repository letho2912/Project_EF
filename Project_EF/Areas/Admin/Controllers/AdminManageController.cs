using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_EF.Data;
using Project_EF.Models;

namespace Project_EF.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminManageController : Controller
    {
        private readonly Connect _context;
        public const string CARTKEY = "cart";
        public const string adminEmail = "email";
        public AdminManageController(Connect context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Login(Admins user)
        {
            Admins us = _context.Admins.Where(s => s.username == user.username && s.password == user.password).FirstOrDefault();
            if (us != null)
            {
                string displayname = us.displayname;
                string email = us.email;
                var session = HttpContext.Session;
                session.SetString(CARTKEY, displayname);
                session.SetString(adminEmail, email);
                /*HttpContext.Session.SetString("displayname", us.displayname);
                HttpContext.Session.SetString("email", us.email);
                HttpContext.Session.SetString("adminId", us.Id.ToString());*/

                return RedirectToAction("Dashboard", "Dashboard");
            }
            else
            {
                ViewBag.error = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View();
            }
        }
        public IActionResult User_Role()
        {
            IEnumerable<Admins> objList = _context.Admins;
            return View(objList);
        }
        /*Thêm nhân viên*/
        public IActionResult CreateUser()
        {
            Admins login = new Admins();
            return PartialView("_AddUser", login);
        }
        [HttpPost]
        public IActionResult CreateUser(Admins obj)
        {
            _context.Admins.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("User_role");
        }
        // GET: Admin/Products/Delete/5
        public IActionResult Edit(int? id)
        {
            var b = _context.Admins.Find(id);
            return PartialView("_UpdateCate", b);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost]
        public IActionResult Edit(Admins obj)
        {
            _context.Admins.Update(obj);
            _context.SaveChanges();
            return PartialView("_UpdateCate", obj);
    }

        private bool AdminsExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
