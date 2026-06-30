using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollegeApp.Models;

namespace CollegeApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).UseIdentityColumn();

            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.StudentName).HasMaxLength(250);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(50);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);

            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName = "San Deep",
                    Address = "123 Main St",
                    Email = "jhon@gmail.com",
                    DOB = new DateTime(2000, 1, 1)
                },
                                new Student
                {
                    Id = 2,
                    StudentName = "Man Deep",
                    Address = "321 Main St",
                    Email = "man@gmail.com",
                    DOB = new DateTime(2000, 1, 1)
                }
            });
        }
    }
}