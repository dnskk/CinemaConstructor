using CinemaConstructor.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaConstructor.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAudit> UserAuditEvents { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<FilmSession> FilmSessions { get; set; }

        public DbSet<CompanyMember> CompanyMembers { get; set; }

        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=localhost;Database=adminLTE;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
