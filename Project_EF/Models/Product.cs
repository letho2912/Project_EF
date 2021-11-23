using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    public class Product
    {
        [Key]
        /*Id int identity primary key,
name_product nvarchar(100),
describe nvarchar(max),
price float,
sale float,
count_pr int,
producer nvarchar(100),
[status] nvarchar(50) default N'Có sẵn',
date_add date,
image_product varchar(50),
ParentCateId int,
CategoryId int,*/
        [Required, DisplayName("Mã sản phẩm")]
        public int Id { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string name_product { get; set; }
        [DisplayName("Mô tả")]
        public string describe { get; set; }
        [DisplayName("Giá gốc")]
        public double price { get; set; }
        [DisplayName("Giá bán")]
        public double sale { get; set; }
        [DisplayName("Bảo hành")]
        public string quarantee { get; set; }
        [DisplayName("Chất liệu")]
        public string material { get; set; }
        [DisplayName("Nhà sản xuất")]
        public string producer { get; set; }
        [DisplayName("Tình trạng")]
        public string status { get; set; }
        public string deleted { get; set; }
        [DisplayName("Ngày nhập")]
        public DateTime date_add { get; set; }
        [DisplayName("Hình ảnh")]
        public string image_product { get; set; }
        /*Thuộc tính bắt buộc*/
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Danh mục cha")]
        public string ParentCateId { get; set; }
        [DisplayName("Danh mục con")]
        public string CategoryId { get; set; }
        public ParentCate ParentCate { get; set; }
        public Category Category { get; set; }
        public IList<OrderDetail> OrderDetail { get; set; }
    }
}
