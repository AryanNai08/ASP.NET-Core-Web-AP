using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApi.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.DepartmentName).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Description)  .HasMaxLength(500).IsRequired(false);

            builder.HasData(new List<Department>()
            {
                new Department { Id = 1, DepartmentName="CE",Description="Computer department"},
                 new Department { Id = 2, DepartmentName="IT",Description="IT department"}
            });
        }
    }
}
