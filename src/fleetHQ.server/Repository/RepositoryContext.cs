using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Repository;

public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().HasOne(u => u.Role);
        modelBuilder.Entity<CompanyModel>().HasMany(c => c.Users).WithOne(u => u.Company).IsRequired(false);
        modelBuilder.Entity<CompanyModel>().HasMany(c => c.Drivers).WithOne(d => d.Company);
        modelBuilder.Entity<CompanyModel>().HasMany(c => c.Vehicles).WithOne(v => v.Company);
        modelBuilder.Entity<VehicleModel>().HasOne(v => v.Driver).WithOne(d => d.Vehicle)
            .HasForeignKey<DriverModel>(d => d.VehicleId).IsRequired(false);
        modelBuilder.Entity<BookingModel>().HasOne(b => b.Vehicle).WithMany(v => v.Bookings)
            .HasForeignKey(b => b.VehicleId).IsRequired(false);
        modelBuilder.Entity<BookingModel>().HasOne(b => b.Driver).WithMany(d => d.Bookings)
            .HasForeignKey(b => b.DriverId).IsRequired(false);

        modelBuilder.Entity<RoleModel>()
            .Property(r => r.Permissions)
            .HasColumnType("jsonb")
            .IsRequired();
    }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<RoleModel> Roles { get; set; }
    public DbSet<CompanyModel> Companies { get; set; }
    public DbSet<VehicleModel> Vehicles { get; set; }
    public DbSet<DriverModel> Drivers { get; set; }
    public DbSet<BookingModel> Bookings { get; set; }

}