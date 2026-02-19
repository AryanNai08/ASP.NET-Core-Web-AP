namespace CollegeApi.Data
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }


        //student key column for forigrn key 
        public virtual ICollection<Student> Students { get; set; }
    }
}
