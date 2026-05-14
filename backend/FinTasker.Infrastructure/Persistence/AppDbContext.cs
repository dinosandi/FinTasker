using FinTasker.Application.Common.Interfaces;
using FinTasker.Domain.Entities;
using FinTasker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinTasker.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

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

            // ──────────────────────────────────────────
            // USERS — enum columns
            // ──────────────────────────────────────────
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(x => x.Email).IsUnique();
                entity.HasIndex(x => new { x.Provider, x.ProviderId }).IsUnique();

                // Role: Admin, Member, dll → simpan sebagai "Admin", bukan 0/1
                entity.Property(x => x.Role)
                    .HasConversion<string>()
                    .HasColumnType("text");

                // AuthProvider: Google, Manual → simpan sebagai text
                entity.Property(x => x.Provider)
                    .HasConversion<string>()
                    .HasColumnType("text");
            });

            // ──────────────────────────────────────────
            // PROJECTS — enum columns
            // ──────────────────────────────────────────
            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasOne(x => x.Users)
                    .WithMany(x => x.Projects)
                    .HasForeignKey(x => x.UsersId)
                    .OnDelete(DeleteBehavior.Cascade);

                // StatusProjects: Active, Archived, dll
                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasColumnType("text");
            });

            // ──────────────────────────────────────────
            // TASKS — enum columns
            // ──────────────────────────────────────────
            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasOne(x => x.Project)
                    .WithMany(x => x.Tasks)
                    .HasForeignKey(x => x.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                // StatusTask: Todo, InProgress, Done, dll
                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasColumnType("text");

                // TaskPriority: Low, Medium, High, Critical
                entity.Property(x => x.Priority)
                    .HasConversion<string>()
                    .HasColumnType("text");
            });

            // ──────────────────────────────────────────
            // TASK MILESTONES — enum columns
            // ──────────────────────────────────────────
            modelBuilder.Entity<TaskMilestones>(entity =>
            {
                entity.HasOne(x => x.Project)
                    .WithMany(x => x.TaskMilestones)
                    .HasForeignKey(x => x.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                // MilestonesStatus: NotStarted, InProgress, Completed
                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasColumnType("text");
            });

            // ──────────────────────────────────────────
            // TASK ACTIVITIES — enum columns
            // ──────────────────────────────────────────
            modelBuilder.Entity<TaskActivities>(entity =>
            {
                entity.HasOne(x => x.Tasks)
                    .WithMany(x => x.TaskActivities)
                    .HasForeignKey(x => x.TasksId)
                    .OnDelete(DeleteBehavior.Cascade);

                // ActivityType: Created, Updated, Commented, dll
                entity.Property(x => x.ActivityType)
                    .HasConversion<string>()
                    .HasColumnType("text");
            });

            // ──────────────────────────────────────────
            // RESOURCES — enum columns
            // ──────────────────────────────────────────
            modelBuilder.Entity<Resources>(entity =>
            {
                // ResourceStatus: Available, InUse, dll
                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasColumnType("text");
            });

            // ──────────────────────────────────────────
            // POMODORO SESSION — enum columns
            // ──────────────────────────────────────────
            modelBuilder.Entity<PomodoroSession>(entity =>
            {
                entity.HasOne(x => x.Tasks)
                    .WithMany(x => x.PomodoroSession)
                    .HasForeignKey(x => x.TasksId)
                    .OnDelete(DeleteBehavior.Cascade);

                // PomodoroSessionStatus: Running, Paused, Completed
                entity.Property(x => x.SessionStatus)
                    .HasConversion<string>()
                    .HasColumnType("text");
            });

            // ──────────────────────────────────────────
            // Relasi lain (tidak berubah dari sebelumnya)
            // ──────────────────────────────────────────
            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired().HasMaxLength(512);
                entity.HasIndex(e => e.Token).IsUnique();
                entity.HasOne(e => e.Users)
                    .WithMany()
                    .HasForeignKey(e => e.UsersId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskTimeLogs>()
                .HasOne(x => x.Tasks)
                .WithMany(x => x.TaskTimeLogs)
                .HasForeignKey(x => x.TasksId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskChecklists>()
                .HasOne(x => x.Tasks)
                .WithMany(x => x.TaskChecklists)
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