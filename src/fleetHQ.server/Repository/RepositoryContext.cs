using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Repository;

public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().HasOne(u => u.Role);
        modelBuilder.Entity<RoleModel>().HasMany(r => r.Permissions).WithOne(p => p.Role);
    }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<RoleModel> Roles { get; set; }
    public DbSet<PermissionModel> Permissions { get; set; }
}