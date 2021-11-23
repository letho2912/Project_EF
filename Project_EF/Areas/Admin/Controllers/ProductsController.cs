using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Product> Product { get; set; }
        public async Task<IActionResult> Index( string sortOrder,string currentFilter,string searchString,int? pageNumber){
            ViewBag.displayname = HttpContext.Session.GetString("displayname");
            ViewBag.userId = HttpContext.Session.GetString("userId");
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var pr = _context.Product.Include(p => p.Category).Include(p => p.ParentCate).Where(s => s.deleted == "False").OrderByDescending(s=>s.Id).AsNoTracking();

            
            if (!String.IsNullOrEmpty(searchString))
            {
                pr = pr.Where(s => s.name_product.Contains(searchString)
                                       || s.ParentCate.name_prcate.Contains(searchString)
                                       || s.Category.name_cate.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Date":
                    pr = pr.OrderBy(s => s.date_add);
                    break;
                case "date_desc":
                    pr = pr.OrderByDescending(s => s.date_add);
                    break;
                default:
                    pr = pr.OrderBy(s => s.name_product);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Product>.CreateAsync(pr.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Admin/Products/Details/
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
        public async Task<IActionResult> SaveCustomerAsync(ProductCreateModel cust)
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
                                cust.Product.image_product = fileName;
                            }

                        }
                    }
                }
                cust.Product.date_add = DateTime.Now;
                _context.Add(cust.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<ParentCate> cate = new List<ParentCate>();
            cate = _context.ParentCate.ToList();
            ViewBag.nameprcate = cate;
            List<Category> prcate = new List<Category>();
            prcate = _context.Category.ToList();
            ViewBag.namecate = prcate;
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
             
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {

            if (id != product.Id)
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
                                    product.image_product = fileName;
                                }

                            }
                        }
                    }
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
        public async Task<IActionResult> DeleteConfirmed(int id,Product product)
        {
            var product1 = await _context.Product.FindAsync(id);
            product1.deleted = "true";
            _context.Product.Update(product1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
