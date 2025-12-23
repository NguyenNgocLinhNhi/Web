using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [ForeignKey("CarId")]
        public virtual Car? Car { get; set; }
    }
}