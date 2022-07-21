using ElectronicsWorkshop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsWorkshop.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<BaseDevice> BaseDevices { get; set; }

    public DbSet<CompositeDevice> CompositeDevices { get; set; }

    public DbSet<Connector> Connectors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CompositeDevice>(b =>
        {
            b.HasKey(k => k.Id);
            b.HasMany<Connector>(c => c.Connectors)
                .WithMany(c => c.CompositeDevices);
            b.HasOne<BaseDevice>(c => c.Basis)
                .WithMany(c => c.CompositeDevices)
                .HasForeignKey(k => k.BasisId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        Seed(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        var baseDevices = new List<BaseDevice>()
        {
            new BaseDevice {
                CompositeDevices = null, Id = 1, Name = "Laptop Motherboard", Price = 20000, Quantity = 100 },
            new BaseDevice {
                CompositeDevices = null, Id = 2, Name = "HD Monitor PCB", Price = 5000, Quantity = 100 },
            new BaseDevice {
                CompositeDevices = null, Id = 3, Name = "Smartphone PCB", Price = 10000, Quantity = 100 },
            new BaseDevice {
                CompositeDevices = null, Id = 4, Name = "Arduino", Price = 10, Quantity = 100 },
            new BaseDevice {
                CompositeDevices = null, Id = 5, Name = "Raspberry Pi", Price = 100, Quantity = 100 },
            new BaseDevice {
                CompositeDevices = null, Id = 6, Name = "Oscilloscope PCB", Price = 25000, Quantity = 100 },
            new BaseDevice {
                CompositeDevices = null, Id = 7, Name = "VNA PCB", Price = 50000, Quantity = 100 },
            new BaseDevice {
                CompositeDevices = null, Id = 8, Name = "RF Amplifier PCB", Price = 1500, Quantity = 100 }
        };

        var connectors = new List<Connector>
        {
            new Connector {
                CompositeDevices = null, Id = 1, Name = "FFC", Price = 11, Quantity = 100 },
            new Connector {
                CompositeDevices = null, Id = 2, Name = "LCEDI", Price = 9, Quantity = 100 },
            new Connector {
                CompositeDevices = null, Id = 3, Name = "USB", Price = 2, Quantity = 100 },
            new Connector {
                CompositeDevices = null, Id = 4, Name = "USB type C", Price = 2, Quantity = 100 },
            new Connector {
                CompositeDevices = null, Id = 5, Name = "General Pin", Price = 1, Quantity = 100 },
            new Connector {
                CompositeDevices = null, Id = 6, Name = "DC Power", Price = 9, Quantity = 100 },
            new Connector {
                CompositeDevices = null, Id = 7, Name = "BNC", Price = 6, Quantity = 100 },
            new Connector {
                CompositeDevices = null, Id = 8, Name = "SMA", Price = 15, Quantity = 100 }
        };

        modelBuilder.Entity<BaseDevice>().HasData(baseDevices);
        modelBuilder.Entity<Connector>().HasData(connectors);
    }
}