using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    public class ProductModelInterface
    {
        [DisplayName("Mã sản phẩm")]
        public int Id { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string name_product { get; set; }
        [DisplayName("Mô tả")]
        public string describe { get; set; }
        [DisplayName("Giá gốc")]
        public double price { get; set; }
        [DisplayName("Giá bán")]
        public double sale { get; set; }
        [DisplayName("Số lượng")]
        public int count_pr { get; set; }
        [DisplayName("Nhà sản xuất")]
        public string producer { get; set; }
        [DisplayName("Tình trạng")]
        public string status { get; set; }
        [DisplayName("Ngày nhập")]
        public DateTime date_add { get; set; }
        [DisplayName("Hình ảnh")]
        public IFormFile image_product { get; set; }
        [DisplayName("Danh mục cha")]
        public string ParentCateId { get; set; }
        public ParentCate ParentCate { get; set; }
        [DisplayName("Danh mục con")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
