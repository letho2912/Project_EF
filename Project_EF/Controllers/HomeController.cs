using Microsoft.AspNetCore.Mvc;
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
            List<Product> product = _db.Product.OrderByDescending(b => b.date_add).Where(b=>b.deleted == "False").Take(4).ToList();
            var saleProduct = _db.Product.Where(c => c.price > c.sale && c.deleted=="False").Take(4).ToList();
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
    }
}
