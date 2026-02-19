using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApi.Data
{
    public class Student
    {
   
        public int Id { get; set; }

        public String Studentname { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }

        public DateTime DOB { get; set; }



        //Department table column for forigen key creation
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
