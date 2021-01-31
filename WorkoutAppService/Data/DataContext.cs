using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutAppService.Entities;

namespace WorkoutAppService.Data
{
    public class DataContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<LinkedAccount> LinkedAccounts => Set<LinkedAccount>();
        public DbSet<Equipment> Equipment => Set<Equipment>();
        public DbSet<Muscle> Muscles => Set<Muscle>();
        public DbSet<Exercise> Exercises => Set<Exercise>();
        public DbSet<ExerciseCategory> ExerciseCategories => Set<ExerciseCategory>();
        public DbSet<Gym> Gyms => Set<Gym>();

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<RefreshToken>(rToken =>
            {
                rToken.HasKey(k => new { k.UserId, k.DeviceId });
            });

            modelBuilder.Entity<LinkedAccount>(linkedAccount =>
            {
                linkedAccount.HasKey(account => new { account.Id, account.LinkedAccountType });
                linkedAccount.Property(account => account.LinkedAccountType).HasConversion<string>();
            });

            modelBuilder.Entity<MuscleUsage>()
                .HasKey(k => new { k.ExerciseId, k.MuscleId });

            modelBuilder.Entity<ExerciseStep>()
                .HasKey(k => new { k.ExerciseId, k.ExerciseStepNumber });
        }
    }
}