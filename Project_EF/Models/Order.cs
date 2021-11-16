using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    public class Order
    {
        /*
         [Id]
      ,[fullname]
      ,[phone]
      ,[email]
      ,[address]
      ,[datesub]
      ,[status]
      ,[into]
      ,[UserId]
         */
        [Key]
        [DisplayName("Mã đơn hàng")]
        public string Id { get; set; }
        [DisplayName("Họ tên")]
        public string fullname { get; set; }
        [DisplayName("SĐT")]
        public string phone { get; set; }
        [DisplayName("Email")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }
        [DisplayName("Địa chỉ")]
        public string address { get; set; }
        [DisplayName("Ngày bán")]
        public DateTime datesub { get; set; }
        [DisplayName("Tình trạng")]
        public string status { get; set; }
        [DisplayName("Tổng tiền")]
        public double into { get; set; }
        [DisplayName("ID KH")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
