using CodeLaboratory.Enteties;
using Microsoft.EntityFrameworkCore;

namespace CodeLaboratory.Data.Contexts
{
    public class CodeLabDbContext : DbContext
    {
        public CodeLabDbContext(DbContextOptions<CodeLabDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<UserProjectEntity> UserProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserProjectEntity>().HasKey(up => new { up.UserId, up.ProjectId });
            modelBuilder.Entity<UserEntity>().HasAlternateKey(u => u.Login);
        }
    }
}
