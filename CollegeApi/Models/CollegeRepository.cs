namespace CollegeApi.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; }= new List<Student>(){
            new Student
            {
                Id = 1,
                Studentname = "AryanNai",
                Email = "aryannai841@gmail.com",
                Address = "Ahmedabad,Gujarat"
            },
            new Student
            {
                Id = 2,
                Studentname = "KartikAhir",
                Email = "kartik@gmail.com",
                Address = "Surat,Gujarat"
            } };

    }
}
