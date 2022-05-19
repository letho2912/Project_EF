using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    public class Ad
    {
        /*[Id]
     ,[username]
     ,[displayname]
     ,[password]
     ,[level]
       ,[status]*/
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống"), DisplayName("Tên đăng nhập")]
        public string username { get; set; }
        [DisplayName("Họ và tên")]
        public string displayname { get; set; }
        [Required(ErrorMessage = "Vui lòng điền mật khẩu")]
        public string password { get; set; }
        [DisplayName("Email")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }
        [DisplayName("Quyền")]
        public string level { get; set; }
        [DisplayName("Tình trạng")]
        public string status { get; set; }
        public ICollection<News> News { get; set; }

    }
}
