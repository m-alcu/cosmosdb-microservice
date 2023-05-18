using CoreApi.Domain.Primitives;

namespace Domain.Entities.Employees;
public class Employee : Entity
{
    public Employee() : base()
    {

    }

    public Employee(Guid id, string tenantId, string? name, string? surname, string? email, string? dni) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Surname = surname;
        Email = email;
        Dni = dni;
    }

    public void UpdateEmployee(string tenantId, string? name, string? surname, string? email, string? dni)
    {
        TenantId = tenantId;
        Name = name;
        Surname = surname;
        Email = email;
        Dni = dni;
    }

    public string TenantId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Dni { get; set; }
}
