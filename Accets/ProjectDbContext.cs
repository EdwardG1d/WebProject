using Microsoft.EntityFrameworkCore;

namespace WebProject.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<MyTask> Tasks { get; set; } = null!; 
        public DbSet<TimeEntry> TimeEntries { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TimeEntry>()
                .HasOne(te => te.Task)
                .WithMany(t => t.TimeEntries)
                .HasForeignKey(te => te.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TimeEntry>()
                .Property(te => te.Hours)
                .HasColumnType("decimal(4,2)");

            modelBuilder.Entity<TimeEntry>()
                .HasIndex(te => new { te.Date, te.TaskId });
        }
    }
}
