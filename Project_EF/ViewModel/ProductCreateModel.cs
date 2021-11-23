using Microsoft.AspNetCore.Mvc.Rendering;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.ViewModel
{
    public class ProductCreateModel
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> ParentCate { get; set; }
        public IEnumerable<SelectListItem> Category { get; set; }
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<Product> orderProduct { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public OrderDetail orderDetail { get; set; }
    }
}
