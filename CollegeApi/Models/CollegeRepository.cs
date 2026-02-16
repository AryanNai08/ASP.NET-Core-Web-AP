namespace CollegeApi.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; }= new List<Student>(){
            new Student
            {
                Id = 1,
                Studentname = "Aryan Nai",
                Email = "aryannai841@gmail.com",
                Address = "Ahmedabad,Gujarat"
            },
            new Student
            {
                Id = 2,
                Studentname = "Kartik Ahir",
                Email = "kartik@gmail.com",
                Address = "Surat,Gujarat"
            } };

    }
}
