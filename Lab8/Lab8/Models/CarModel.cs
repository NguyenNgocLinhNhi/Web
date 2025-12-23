using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên dòng xe không được để trống")]
        [MaxLength(100)]
        [Display(Name = "Tên Dòng Xe")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn hãng xe")]
        [Display(Name = "Hãng Xe")]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand? Brand { get; set; }

        [Display(Name = "Mô tả dòng xe")]
        public string? ModelDescription { get; set; } // Thuộc tính đã thêm ở các bước trước

        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}