using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Repository;

public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().HasOne(u => u.Role);
        modelBuilder.Entity<RoleModel>().HasMany(r => r.Permissions).WithOne(p => p.Role);
        modelBuilder.Entity<PermissionModel>().HasOne(p => p.Feature);
        modelBuilder.Entity<FeatureModel>().HasIndex(ft => ft.Name);
        modelBuilder.Entity<FeatureModel>().HasData(
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Vehicles },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Drivers },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Bookings },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Settings },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Expenses },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.FuelUsage },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Documents },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Reports },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Places },
            new FeatureModel { Id = Guid.NewGuid(), Name = AppFeatures.Service }
        );
    }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<RoleModel> Roles { get; set; }
    public DbSet<PermissionModel> Permissions { get; set; }
    public DbSet<FeatureModel> Features { get; set; }
}