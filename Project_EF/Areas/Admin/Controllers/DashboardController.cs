using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_EF.Data;
using Project_EF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly Connect _context;

        public DashboardController(Connect context)
        {
            _context = context;
        }
        public async Task<IActionResult> Dashboard(int? orderId)
        {
            int a = _context.Order.Where(p => p.status == "Đang xử lý").Count();
            int b = _context.Contact.Where(p => p.status == "Chưa phản hồi").Count();
            int usernew = _context.Order.Select(m => m.UserId).Distinct().Count();
            double m = _context.OrderDetail.
                Where(m => m.Order.status == "Hoàn thành" || m.Order.status == "Xác nhận...")
                .Select(m => m.amount * m.price).Sum();
                ViewBag.totalDT = m;
                ViewBag.usernew = usernew;
                ViewBag.contact = b;
                ViewBag.total = a;
          //var ornew = _context.Order.Where(m => m.status == "Đang xử lý").ToList();

            var viewModel = new ProductCreateModel();
            viewModel.Order = await _context.Order.OrderByDescending(p => p.OrderId)
                .Where(m=>m.status=="Đang xử lý")
                .Include(c => c.OrderDetail)
                .ThenInclude(c => c.Product).Include(m => m.User).ToListAsync();
            if (orderId != null)
            {
                ViewBag.orderId = orderId.Value;
                viewModel.OrderDetail = viewModel.Order
                    .Where(i => i.OrderId == orderId.Value).Single().OrderDetail;

            }
          //ViewBag.ornew = ornew;
            return View(viewModel);
        }
    }
}
