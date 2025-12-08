using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Open"; // Open, In Progress, Closed

        // Foreign Keys
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Navigation properties
        public Customer? Customer { get; set; }
        public Category? Category { get; set; }
    }
}