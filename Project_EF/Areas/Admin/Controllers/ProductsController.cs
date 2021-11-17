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
using Project_EF.ViewModel;

namespace Project_EF.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly Connect _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductsController(Connect context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var connect = _context.Product.Include(p => p.Category).Include(p => p.ParentCate).AsNoTracking();
            return View(await connect.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.ParentCate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create

        [HttpGet]
        public ActionResult GetCities(string ParentCateId)
        {
            if (!string.IsNullOrWhiteSpace(ParentCateId) && ParentCateId.Length == 3)
            {
                List<SelectListItem> citiesSel = _context.Category
                    .Where(c => c.ParentCateId == ParentCateId)
                    .OrderBy(n => n.name_cate)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.Id,
                        Text = n.name_cate
                    }).ToList();
                return Json(citiesSel);
            }
            return null;
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public IActionResult Create()
        {
            ProductCreateModel productCreateModel = new ProductCreateModel();
            productCreateModel.Product = new Product();
            List<SelectListItem> prcate = _context.ParentCate
                .Select(n =>
                new SelectListItem
                {
                    Value = n.Id,
                    Text = n.name_prcate
                }).ToList();
            productCreateModel.ParentCate = prcate;
            productCreateModel.Category = new List<SelectListItem>();
            return View(productCreateModel);
        }
        private string UploadedFile(ProductCreateModel cust)
        {
            string fileName = null;

            if (cust.Product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "-" + cust.Product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    cust.Product.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public IActionResult SaveCustomer(ProductCreateModel cust)
        {
            cust.Product.image_product=UploadedFile(cust);
            _context.Add(cust.Product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "name_cate", product.CategoryId);
            ViewData["ParentCateId"] = new SelectList(_context.ParentCate, "Id", "name_prcate", product.ParentCateId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name_product,describe,price,sale,quarantee,producer,status,date_add,image_product,ParentCateId,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "name_cate", product.CategoryId);
            ViewData["ParentCateId"] = new SelectList(_context.ParentCate, "Id", "name_prcate", product.ParentCateId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.ParentCate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
