using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; } // 🔹 새롭게 추가

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("projects"); 
            modelBuilder.Entity<Project>().Property(p => p.ProjectId).HasColumnName("projectid");
            modelBuilder.Entity<Project>().Property(p => p.Description).HasColumnName("description");
            modelBuilder.Entity<Project>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<Project>().Property(p => p.StartDate).HasColumnType("timestamp with time zone"); 
            modelBuilder.Entity<Project>().Property(p => p.EndDate).HasColumnType("timestamp with time zone");

            modelBuilder.Entity<ProjectTask>().ToTable("tasks");

            modelBuilder.Entity<ProjectTask>().Property(t => t.TaskId).HasColumnName("taskid");
            modelBuilder.Entity<ProjectTask>().Property(t => t.Title).HasColumnName("title");
            modelBuilder.Entity<ProjectTask>().Property(t => t.Description).HasColumnName("description"); 
            modelBuilder.Entity<ProjectTask>().Property(t => t.IsCompleted).HasColumnName("iscompleted");
            modelBuilder.Entity<ProjectTask>().Property(t => t.ProjectId).HasColumnName("projectid");
            
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}