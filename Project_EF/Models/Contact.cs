using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Project_EF.Models
{
    public class Contact
    {
        /*[Id]
      ,[fullname]
      ,[email]
      ,[phone]
      ,[content]
      ,[status]*/
        [Key]
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Họ và tên")]
        public string fullname { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Địa chỉ email không hợp lệ")]
        public string email { get; set; }
        [DisplayName("SĐT")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Số điện thoại không hợp lệ")]
        public string phone { get; set; }
        [DisplayName("Nôi dung")]
        public string content { get; set; }
        [DisplayName("Tình trạng")]
        public string status { get; set; }
    }
}
