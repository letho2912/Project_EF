using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    public class OrderDetail
    {
        /*[Id]
      ,[amount]
      ,[OrderId]
      ,[ProductId]
      ,[SizeId]
      ,[price]*/
        [Key]
        [DisplayName("Mã đơn hàng")]
        public int OrderId { get; set; }
        [DisplayName("Mã sản phẩm")]
        public int ProductId { get; set; }
        [DisplayName("Số lượng"),Range(0,100)]
        public int amount { get; set; }
        public Order Order { get; set; }
        
        public Product Product { get; set; }
        [DisplayName("Giá bán")]
        public double price { get; set; }
    }
}
