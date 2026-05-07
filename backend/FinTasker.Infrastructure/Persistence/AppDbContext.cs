using Microsoft.EntityFrameworkCore;
using FinTasker.Domain.Entities;
using FinTasker.Application.Common.Interfaces;

namespace FinTasker.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔐 Unique Email
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // 🔐 Provider + ProviderId unique (Google login)
            modelBuilder.Entity<Users>()
                .HasIndex(u => new { u.Provider, u.ProviderId })
                .IsUnique();
        }
    }
}

