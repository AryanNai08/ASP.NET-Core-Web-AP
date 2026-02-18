using Microsoft.EntityFrameworkCore;

namespace CollegeApi.Data
{
    public class CollegeDBContext:DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options):base(options)
        {

        }
        DbSet<Student> Students { get; set; }
    }
}
