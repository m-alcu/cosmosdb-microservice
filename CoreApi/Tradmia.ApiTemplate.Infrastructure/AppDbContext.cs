using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Tradmia.ApiTemplate.Domain.Entities.Employees;
using Tradmia.ApiTemplate.Infrastructure.Data;

namespace Tradmia.ApiTemplate.Infrastructure;

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