using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ManagOre.Api.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ManagOre.Api.Models.Employee", b =>
            {
                b.Property<Guid>("Id");
                b.Property<string>("FirstName").IsRequired().HasMaxLength(100);
                b.Property<string>("LastName").IsRequired().HasMaxLength(100);
                b.Property<string>("Email").HasMaxLength(200);
                b.HasKey("Id");
                b.ToTable("Employees");
            });

            modelBuilder.Entity("ManagOre.Api.Models.ProjectGroup", b =>
            {
                b.Property<Guid>("Id");
                b.Property<string>("Name").IsRequired().HasMaxLength(200);
                b.Property<string>("Description").HasMaxLength(1000);
                b.HasKey("Id");
                b.ToTable("ProjectGroups");
            });

            modelBuilder.Entity("ManagOre.Api.Models.Project", b =>
            {
                b.Property<Guid>("Id");
                b.Property<string>("Name").IsRequired().HasMaxLength(200);
                b.Property<string>("Description");
                b.Property<Guid?>("ProjectGroupId");
                b.HasKey("Id");
                b.HasIndex("ProjectGroupId");
                b.ToTable("Projects");
            });

            modelBuilder.Entity("ManagOre.Api.Models.TimeEntry", b =>
            {
                b.Property<Guid>("Id");
                b.Property<Guid>("EmployeeId");
                b.Property<Guid>("ProjectId");
                b.Property<DateTime>("Date");
                b.Property<double>("Hours");
                b.Property<string>("Description");
                b.HasKey("Id");
                b.HasIndex("EmployeeId");
                b.HasIndex("ProjectId");
                b.ToTable("TimeEntries");
            });

            modelBuilder.Entity("ManagOre.Api.Models.Project", b =>
            {
                b.HasOne("ManagOre.Api.Models.ProjectGroup", null)
                    .WithMany("Projects")
                    .HasForeignKey("ProjectGroupId")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity("ManagOre.Api.Models.TimeEntry", b =>
            {
                b.HasOne("ManagOre.Api.Models.Employee", null)
                    .WithMany("TimeEntries")
                    .HasForeignKey("EmployeeId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("ManagOre.Api.Models.Project", null)
                    .WithMany("TimeEntries")
                    .HasForeignKey("ProjectId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
