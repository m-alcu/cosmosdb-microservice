
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Infrastructure.Database;

public class ApplicationContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Distributor> Distributors { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> option) : base(option)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region DefaultContainer
        modelBuilder.HasDefaultContainer("Store");
        #endregion

        #region Container
        modelBuilder.Entity<Order>()
            .ToContainer("Orders");
        #endregion

        #region NoDiscriminator
        modelBuilder.Entity<Order>()
            .HasNoDiscriminator();
        #endregion

        #region PartitionKey
        modelBuilder.Entity<Order>()
            .HasPartitionKey(o => o.PartitionKey);
        #endregion

        #region ETag
        modelBuilder.Entity<Order>()
            .UseETagConcurrency();
        #endregion

        #region PropertyNames
        modelBuilder.Entity<Order>().OwnsOne(
            o => o.ShippingAddress,
            sa =>
            {
                sa.ToJsonProperty("Address");
                sa.Property(p => p.Street).ToJsonProperty("ShipsToStreet");
                sa.Property(p => p.City).ToJsonProperty("ShipsToCity");
            });
        #endregion

        #region OwnsMany
        modelBuilder.Entity<Distributor>().OwnsMany(p => p.ShippingCenters);
        #endregion

        #region ETagProperty
        modelBuilder.Entity<Distributor>()
            .Property(d => d.ETag)
            .IsETagConcurrency();
        #endregion
    }
}