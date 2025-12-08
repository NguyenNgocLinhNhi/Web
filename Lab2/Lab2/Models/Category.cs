using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace Lab2.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Ticket>? Tickets { get; set; }
    }
}