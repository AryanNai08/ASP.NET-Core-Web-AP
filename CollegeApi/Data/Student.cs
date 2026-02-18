using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApi.Data
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Studentname { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }

        public DateTime DOB { get; set; }
    }
}
