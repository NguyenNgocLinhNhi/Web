using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab8.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên hãng xe không được để trống")]
        [Display(Name = "Tên Hãng")]
        public string Name { get; set; } = null!;

        [Display(Name = "Quốc gia")]
        public string? Country { get; set; }

        [Display(Name = "Mô tả hãng")]
        public string? Description { get; set; }

        // Quan hệ 1-N với CarModel
        public virtual ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}