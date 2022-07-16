using ElectronicsWorkshop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsWorkshop.Infrastructure.Data;

public class ApplicationDbContext: DbContext
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

    }
}