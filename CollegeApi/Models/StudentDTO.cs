using System.ComponentModel.DataAnnotations;

namespace CollegeApi.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Student name is required")]
        public String Studentname { get; set; }

        [EmailAddress(ErrorMessage = "Enter valid Email")]
        public String Email { get; set; }

        [Required (ErrorMessage = "Student Address is required")]
        public String Address { get; set; }
    }
}
