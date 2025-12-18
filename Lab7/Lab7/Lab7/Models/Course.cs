using System.ComponentModel.DataAnnotations.Schema;

namespace Lab7.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Cho phép nhập ID thủ công
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string? Description { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
