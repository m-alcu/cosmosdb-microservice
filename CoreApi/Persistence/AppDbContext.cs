
using Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CoreApi.Infrastructure.Database;

[ExcludeFromCodeCoverage]
public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
}