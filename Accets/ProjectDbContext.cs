using Microsoft.EntityFrameworkCore;



namespace WebProject.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        { }
        public DbSet<Project> Projects { get; set; } = null!;

        public DbSet<MyTask> Takss {  get; set; } = null!;
        public DbSet<TimeEntry> TimeEntries { get; set; } = null!;    
        public ProjectDbContext()
        {
            Database.EnsureCreated();
           
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=myapp;Trusted_Connection=True;",
              sqlServerOptionsAction: sqlOptions =>
              {
                  sqlOptions.EnableRetryOnFailure(
                  maxRetryCount: 5,
                  maxRetryDelay: TimeSpan.FromSeconds(30),
                  errorNumbersToAdd: null);
              });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyTask>()
                .HasOne(t => t.Projects)
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
    