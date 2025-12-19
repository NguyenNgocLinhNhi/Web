using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!; 

        // Khóa ngoại liên kết với Brand
        [Required]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; } = null!;

        // Danh sách các xe cụ thể thuộc dòng xe này
        public ICollection<Car> Cars { get; set; } = new List<Car>(); 
    }
}