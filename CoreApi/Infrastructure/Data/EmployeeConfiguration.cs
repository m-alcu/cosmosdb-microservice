using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tradmia.ApiTemplate.Domain.Entities.Employees;

namespace Tradmia.ApiTemplate.Infrastructure.Data;
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToContainer("employees").HasNoDiscriminator().HasPartitionKey(o => o.TenantId);
        builder.Property(p => p.Id).ToJsonProperty("id");
        builder.Property(p => p.Name).ToJsonProperty("name");
        builder.Property(p => p.Surname).ToJsonProperty("surname");
        builder.Property(p => p.Dni).ToJsonProperty("dni");
        builder.Property(p => p.Email).ToJsonProperty("email");
        builder.Property(p => p.TenantId).ToJsonProperty("tenantId");

        builder.HasKey(e => new { e.Id, e.TenantId });
    }
}
