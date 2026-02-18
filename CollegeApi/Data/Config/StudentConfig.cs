using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApi.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students");
            builder.HasKey(t => t.Id);

            builder.Property(x=>x.Id).UseIdentityColumn();

            builder.Property(n => n.Studentname).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            builder.HasData(new List<Student>()
            {
                new Student { Id = 1, Studentname="Aryan",Email="aryan@gmail.com",DOB=new DateTime(2004,9,3)},
                 new Student { Id = 2, Studentname="kartik",Email="k@gmail.com",DOB=new DateTime(2004,9,3)}
            });
        }
    }
}
