using FinTasker.Application.Common.Interfaces;
using FinTasker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTasker.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        public DbSet<Projects> Projects { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<TaskTimeLogs> TaskTimeLogs { get; set; }

        public DbSet<TaskMilestones> TaskMilestones { get; set; }

        public DbSet<TaskActivities> TaskActivities { get; set; }

        public DbSet<TaskTags> TaskTags { get; set; }
        public DbSet<TaskTagRelations> TaskTagRelations { get; set; }

        public DbSet<TaskChecklists> TaskChecklists { get; set; }

        public DbSet<Resources> Resources { get; set; }

        public DbSet<TaskResources> TaskResources { get; set; }


        public DbSet<ProductivityReports> ProductivityReports { get; set; }

        public DbSet<PomodoroSession> PomodoroSession { get; set; }

        public DbSet<Notifications> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasIndex(e => e.Token).IsUnique();

                entity.HasOne(e => e.Users)
                    .WithMany() // atau WithMany(u => u.RefreshTokens) jika mau tambahkan collection di Users
                    .HasForeignKey(e => e.UsersId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Users>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Users>()
                .HasIndex(x => new { x.Provider, x.ProviderId })
                .IsUnique();

            modelBuilder.Entity<Projects>()
                .HasOne(x => x.Users)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.UsersId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tasks>()
                .HasOne(x => x.Project)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskMilestones>()
                .HasOne(x => x.Project)
                .WithMany(x => x.TaskMilestones)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskTimeLogs>()
                .HasOne(x => x.Tasks)
                .WithMany(x => x.TaskTimeLogs)
                .HasForeignKey(x => x.TasksId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskActivities>()
                .HasOne(x => x.Tasks)
                .WithMany(x => x.TaskActivities)
                .HasForeignKey(x => x.TasksId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskChecklists>()
                .HasOne(x => x.Tasks)
                .WithMany(x => x.TaskChecklists)
                .HasForeignKey(x => x.TasksId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PomodoroSession>()
                .HasOne(x => x.Tasks)
                .WithMany(x => x.PomodoroSession)
                .HasForeignKey(x => x.TasksId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskTagRelations>()
                .HasKey(x => new { x.TasksId, x.TagId });

            modelBuilder.Entity<TaskTagRelations>()
                .HasOne(x => x.Tasks)
                .WithMany(x => x.TaskTagRelations)
                .HasForeignKey(x => x.TasksId);

            modelBuilder.Entity<TaskTagRelations>()
                .HasOne(x => x.Tag)
                .WithMany(x => x.TaskTagRelations)
                .HasForeignKey(x => x.TagId);

            modelBuilder.Entity<TaskResources>()
                .HasOne(x => x.Tasks)
                .WithMany(x => x.TaskResources)
                .HasForeignKey(x => x.TasksId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskResources>()
                .HasOne(x => x.Resources)
                .WithMany(x => x.TaskResources)
                .HasForeignKey(x => x.ResourcesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductivityReports>()
                .HasOne(x => x.User)
                .WithMany(x => x.ProductivityReports)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notifications>()
                .HasOne(x => x.Users)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.UsersId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}