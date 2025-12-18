using System.ComponentModel.DataAnnotations;

namespace Lab7.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        public string? EmailAddress { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
