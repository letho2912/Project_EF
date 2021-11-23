using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_EF.Data;
using Project_EF.Models;
using Project_EF.ViewModel;

namespace Project_EF.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly Connect _context;

        public OrdersController(Connect context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int? orderId)
        {
            var viewModel = new ProductCreateModel();
            viewModel.Order = await _context.Order.OrderByDescending(p=>p.OrderId)
                .Include(c => c.OrderDetail)
                .ThenInclude(c => c.Product).Include(m=>m.User).ToListAsync();
            if (orderId != null)
            {
                ViewBag.orderId = orderId.Value;
                viewModel.OrderDetail = viewModel.Order
                    .Where(i => i.OrderId == orderId.Value).Single().OrderDetail;

            }

            return View(viewModel);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order product)
        {
            var product1 = await _context.Order.FindAsync(id);
            product1.status = "Đang giao";
            _context.Order.Update(product1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost, ActionName("Edit2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(int id, Order product)
        {
            var product1 = await _context.Order.FindAsync(id);
            product1.status = "Xác nhận...";
            _context.Order.Update(product1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int orderId, [Bind("status")] Order order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
            return PartialView("_UpdatesStatus", order);
        }*/

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
