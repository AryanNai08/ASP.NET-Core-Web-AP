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
            modelBuilder.Entity<Student>().HasData
                (
                new Student
                {
                    Id = 1,
                    Studentname = "Aryan",
                    Address = "India",
                    Email = "aryan@gmail.com",
                    DOB = new DateTime(2003, 9, 3)
                },

                new Student
                {
                    Id = 2,
                    Studentname = "kartik",
                    Address = "India",
                    Email = "k@gmail.com",
                    DOB = new DateTime(2002, 8, 3)
                }
            );
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(n=>n.Studentname).IsRequired().HasMaxLength(250);
                entity.Property(n=>n.Address).IsRequired(false).HasMaxLength(500);
                entity.Property(n => n.Email).IsRequired().HasMaxLength(250);
            });

        }

    }
}
