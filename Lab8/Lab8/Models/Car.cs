using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int CarModelId { get; set; }
        public virtual CarModel? CarModel { get; set; }

        // Sửa thành decimal? (có dấu hỏi)
        public decimal? Price { get; set; }
    }
}