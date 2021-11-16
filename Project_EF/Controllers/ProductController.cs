using Microsoft.AspNetCore.Mvc;
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
    }
}
