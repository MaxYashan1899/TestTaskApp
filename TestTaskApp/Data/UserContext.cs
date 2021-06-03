using Microsoft.EntityFrameworkCore;
using TestTaskApp.Models;

namespace TestTaskApp.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroups> UsersGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Login).IsUnique(); });
            modelBuilder.Entity<UserGroups>()
                    .HasMany(c => c.Users)
                    .WithMany(s => s.UsersGroups)
                    .UsingEntity(j => j.ToTable("Enrollments"));
        }
    }
}
