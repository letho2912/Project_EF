using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    public class Category
    {
        [Key]
        [MaxLength(3)]
        [DisplayName("Mã danh mục")]
        public String Id { get; set; }
        [DisplayName("Tên danh mục")]
        public string name_cate { get; set; }
        [DisplayName("Hình ảnh minh họa")]
        public string image_cate { get; set; }
        [ForeignKey("ParentCateId")]
        public String ParentCateId { get; set; }
        public ParentCate ParentCate { get; set; }
        public ICollection<Product> Product { get; set; }

    }
}
