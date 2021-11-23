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
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ViewBag.error = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View();
            }
        }
        // GET: Admin/AdminManage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admins = await _context.Admins.FindAsync(id);
            if (admins == null)
            {
                return NotFound();
            }
            return View(admins);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "AdminManage");
        }
        // POST: Admin/AdminManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,username,displayname,password,level,status")] Admins admins)
        {
            if (id != admins.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admins);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminsExists(admins.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admins);
        }
        private bool AdminsExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
