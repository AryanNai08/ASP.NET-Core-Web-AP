using CollegeApi.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace CollegeApi.Data
{
    public class CollegeDBContext:DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options):base(options)
        {

        }
        DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
                modelBuilder.ApplyConfiguration(new StudentConfig());
            

        }

    }
}
