using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_EF.Data;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Controllers
{
    public class ProductController : Controller
    {
        private readonly Connect _db;
        public Product Product { get; set; }
        public ProductController(Connect db)
        {
            _db = db;
        }
        public IActionResult ListProduct(string id)
        {
            IEnumerable<Product> b = _db.Product.Where(b => b.ParentCateId == id);
            return View(b);
        }
        public IActionResult ListProducts(string id)
        {
            IEnumerable<Product> b = _db.Product.Where(b => b.CategoryId == id);
            return View(b);
        }
        [HttpGet]         
        public IActionResult Details(int id)
        {
            var detail = getDetails(id);
            int idd = id;
            var query = _db.Product
                .FromSqlRaw("select top 4 * from Product where ParentCateId = (select ParentCateId from Product where Id ={0})", idd)
                .ToList();

            ViewBag.listrelated = query;


            return View("Details", detail);
           

        }

        private Product getDetails(int id)
        {
            var detail = _db.Product.
                Include(s=>s.ParentCate)
                .ThenInclude(m=>m.Category)
                .Where(b => b.Id == id).FirstOrDefault();
            return detail;
        }
        
    }
}
