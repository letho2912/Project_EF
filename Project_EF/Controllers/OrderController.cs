using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_EF.Data;
using Project_EF.Models;
using Project_EF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Controllers
{
    public class OrderController : Controller
    {
        private readonly Connect _context;

        public OrderController(Connect context)
        {
            _context = context;
        }
       
        [HttpGet]
        public async Task<IActionResult> ListOrder(string userId,int? orderId)
        {
            var viewModel = new ProductCreateModel();
            var session = HttpContext.Session;
            string user = session.GetString("userId");
            userId = user;
            viewModel.Order = await _context.Order
                .Include(c => c.OrderDetail)
                .ThenInclude(c=>c.Product).Where
                (m => m.UserId.ToString() == userId).ToListAsync();
            if (orderId != null)
            {
                ViewBag.orderId = orderId.Value;
                viewModel.OrderDetail = viewModel.Order
                    .Where(i => i.OrderId == orderId.Value).Single().OrderDetail;
                
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Huy(int? orderId)
        {
           
            var order =  _context.Order.Find(orderId);
            return View(order);
    }
        public async Task<IActionResult> Huy(int? orderId, Order order)
        {
            var order1 = await _context.Order.Where(b=>b.OrderId==orderId).FirstOrDefaultAsync();
            order1.status = "Đã hủy";
            _context.Order.Update(order1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListOrder));
        }
        [HttpGet]
        public IActionResult Xacnhan(int? orderId)
        {

            var order = _context.Order.Find(orderId);
            return View(order);
        }
        public async Task<IActionResult> Xacnhan(int? orderId, Order order)
        {
            var order1 = await _context.Order.Where(b => b.OrderId == orderId).FirstOrDefaultAsync();
            order1.status = "Hoàn thành";
            _context.Order.Update(order1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListOrder));
        }
    }
}
