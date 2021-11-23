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
    public class ParentCate
    {
        [Key]
        [MaxLength(3)]
        [DisplayName("Mã danh mục")]
        public String Id { get; set; }
        [DisplayName("Tên danh mục")]
        public string name_prcate { get; set; }
        [DisplayName("Ảnh minh họa")]
        public string image_prcate { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public ICollection<Category> Category{ get; set; }
        public ICollection<Product> Product { get; set; }

    }
}
