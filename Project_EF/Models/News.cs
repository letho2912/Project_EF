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
    public class News
    {
        [Key]
        [Required, DisplayName("Mã bài báo")]
        public int Id { get; set; }
        [DisplayName("Tiêu đề")]
        public string title { get; set; }
        [DisplayName("Nội dung")]
        public string content { get; set; }
        [DisplayName("Tình trạng")]
        public string status { get; set; }
       // public string deleted { get; set; }
        [DisplayName("Ngày đăng")]
        public DateTime date_add { get; set; }
        [DisplayName("Hình ảnh")]
        public string image_new { get; set; }
        /*Thuộc tính bắt buộc*/
        [NotMapped]
        public IFormFile ImageFile { get; set; }
      
        /*[DisplayName("Người đăng")]
        public string AdId { get; set; }
        public Ad Ad { get; set; }*/
    }
}
