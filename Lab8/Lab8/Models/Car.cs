using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên xe không được để trống")]
        public string Name { get; set; } = "";

        public int CarModelId { get; set; }
        public virtual CarModel? CarModel { get; set; }

        public decimal? Price { get; set; }

        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        // --- THÊM DÒNG NÀY ---
        [Display(Name = "Số lượng trong kho")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
        public int StockQuantity { get; set; } = 0;
    }
}