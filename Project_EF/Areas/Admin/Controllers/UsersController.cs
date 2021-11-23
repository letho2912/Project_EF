using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_EF.Data;
using Project_EF.Models;

namespace Project_EF.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly Connect _context;

        public UsersController(Connect context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var pr = _context.User.AsNoTracking();
            return View(await PaginatedList<User>.CreateAsync(pr.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await _context.User.ToListAsync());
        }        
        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfilm(int id, User user)
        {
            var product1 = await _context.User.FindAsync(id);
            product1.status = "Disable";
            _context.User.Update(product1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditEnable(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Enable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable(int id, User user)
        {
            var product1 = await _context.User.FindAsync(id);
            product1.status = "Enable";
            _context.User.Update(product1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
