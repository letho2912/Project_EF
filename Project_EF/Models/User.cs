﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    public class User
    {
        /*[Id]
      ,[username]
      ,[fullname]
      ,[email]
      ,[phone]
      ,[password]
      ,[status]*/
        [Key]
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Họ tên")]
        public string fullname { get; set; }
        [DisplayName("Tên đăng nhập")]
        public string username { get; set; }
        [DisplayName("Email")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }
        [DisplayName("Số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage="Số điện thoại không hợp lệ")]
        public string phone { get; set; }
        [DisplayName("Mật khẩu")]
        public string password { get; set; }
        [DisplayName("Tình trạng")]
        public string status { get; set; }

    }
}
