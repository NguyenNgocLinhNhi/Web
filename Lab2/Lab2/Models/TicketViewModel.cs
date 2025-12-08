using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab2.Models
{
    public class TicketViewModel
    {
        public List<Ticket>? Tickets { get; set; }

        // Dùng cho Dropdownlist Category
        public SelectList? Categories { get; set; }
        public int? SelectedCategoryId { get; set; }

        // Dùng cho Textbox tìm kiếm Subject
        public string? SearchString { get; set; }
    }
}