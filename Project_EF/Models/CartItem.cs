using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    public class CartItem
    {
        public int quantity { set; get; }
        public Product Product { get; set; }
    }
}
