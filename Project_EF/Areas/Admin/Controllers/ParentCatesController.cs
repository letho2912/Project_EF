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
    public class ParentCatesController : Controller
    {
        private readonly Connect _context;
        public ParentCate ParentCate { get; set; }

        public ParentCatesController(Connect context)
        {
            _context = context;
        }

        // GET: Admin/ParentCates
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParentCate.ToListAsync());
        }

        // GET: Admin/ParentCates/Details/5
        public async Task<IActionResult> Details(string id)
        {          
                if (id == null)
                {
                return NotFound();
            }
            ParentCate = await _context.ParentCate
            .Include(s => s.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

                if (ParentCate == null)
                {
                return NotFound();
                }

                return View(ParentCate);
        }

        // GET: Admin/ParentCates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ParentCates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name_prcate,image_prcate")] ParentCate parentCate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parentCate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parentCate);
        }

        // GET: Admin/ParentCates/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentCate = await _context.ParentCate.FindAsync(id);
            if (parentCate == null)
            {
                return NotFound();
            }
            return View(parentCate);
        }

        // POST: Admin/ParentCates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,name_prcate,image_prcate")] ParentCate parentCate)
        {
            if (id != parentCate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parentCate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentCateExists(parentCate.Id))
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
            return View(parentCate);
        }

        // GET: Admin/ParentCates/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentCate = await _context.ParentCate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parentCate == null)
            {
                return NotFound();
            }

            return View(parentCate);
        }

        // POST: Admin/ParentCates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parentCate = await _context.ParentCate.FindAsync(id);
            _context.ParentCate.Remove(parentCate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentCateExists(string id)
        {
            return _context.ParentCate.Any(e => e.Id == id);
        }
    }
}
