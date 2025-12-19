using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab8.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string Name { get; set; } = null!;

        [Display(Name = "Số điện thoại")]
        // ĐỔI PhoneNumber THÀNH Phone
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}