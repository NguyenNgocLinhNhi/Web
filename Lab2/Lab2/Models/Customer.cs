using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace Lab2.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        // Navigation property
        public ICollection<Ticket>? Tickets { get; set; }
    }
}