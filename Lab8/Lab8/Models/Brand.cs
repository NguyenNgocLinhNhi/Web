using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab8.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
       

        [Required]
        public string Name { get; set; } = null!; 

        public string? Country { get; set; }
       

        // Quan hệ 1-N với CarModel
        public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>(); 
    }
}