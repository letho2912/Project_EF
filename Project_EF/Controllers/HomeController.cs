using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_EF.Data;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Connect _db;

        public HomeController(ILogger<HomeController> logger,Connect db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            List<Product> product = _db.Product.OrderByDescending(b=>b.date_add).Where(b=>b.deleted == "False").Take(4).ToList();
            var saleProduct = _db.Product.Where(c => c.price > c.sale && c.deleted=="False").Take(4).ToList();
            /*var query = _db.Product.Include(p=>p.OrderDetail)
                .FromSqlRaw("SELECT TOP 3 WITH TIES name_product, sum(amount) " +
                "FROM Product, OrderDetail WHERE Product.Id = OrderDetail.ProductId " +
                "Group by name_product ORDER BY sum(amount) DESC")
                .AsNoTracking().ToList();
            ViewBag.sellProduct = query;*/
            ViewBag.saleProduct = saleProduct;
            return View("Index", product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Introducer()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
                _db.Add(contact);
                _db.SaveChanges();
                ViewBag.notice = "Chúng tôi sẽ phản hồi bạn trong thời gian sớm nhất, xin cảm ơn!";

            return View("Notice");           
        }
        public IActionResult Search(string tukhoa)
        {
            if (tukhoa == null)
                tukhoa = "";
            Product[] pr = _db.Product.Where(b => b.name_product.ToLower().Contains(tukhoa.ToLower())).ToArray();
            return View("Search", pr);
            ViewBag.tukhoa = tukhoa;
        }
    }
}
