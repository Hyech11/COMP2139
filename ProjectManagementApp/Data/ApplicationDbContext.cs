using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // 🔥필수 추가
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Data
{
    // 🚩 반드시 IdentityDbContext로 변경해야 합니다!
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }


        public DbSet<ProjectComment> ProjectComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().ToTable("projects"); 
            modelBuilder.Entity<Project>().Property(p => p.ProjectId).HasColumnName("projectid");
            modelBuilder.Entity<Project>().Property(p => p.Description).HasColumnName("description");
            modelBuilder.Entity<Project>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<Project>().Property(p => p.StartDate).HasColumnType("timestamp with time zone"); 
            modelBuilder.Entity<Project>().Property(p => p.EndDate).HasColumnType("timestamp with time zone");

            modelBuilder.Entity<TaskItem>().ToTable("TaskItems");
            modelBuilder.Entity<TaskItem>().Property(t => t.Id).HasColumnName("Id"); 
            modelBuilder.Entity<TaskItem>().Property(t => t.Title).HasColumnName("Title");
            modelBuilder.Entity<TaskItem>().Property(t => t.Description).HasColumnName("Description"); 
            modelBuilder.Entity<TaskItem>().Property(t => t.ProjectId).HasColumnName("ProjectId");
            modelBuilder.Entity<ProjectComment>().ToTable("projectcomments");

            
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
