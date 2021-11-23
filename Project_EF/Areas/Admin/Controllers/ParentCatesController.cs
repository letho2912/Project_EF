using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ParentCate ParentCate { get; set; }

        public ParentCatesController(Connect context,IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
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
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        //There is an error here
                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                parentCate.image_prcate = fileName;
                            }

                        }
                    }
                }
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
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            //There is an error here
                            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                            if (file.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    parentCate.image_prcate = fileName;
                                }

                            }
                        }
                    }
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
        private bool ParentCateExists(string id)
        {
            return _context.ParentCate.Any(e => e.Id == id);
        }
    }
}
