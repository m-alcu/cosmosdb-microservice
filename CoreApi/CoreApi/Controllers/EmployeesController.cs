using Application.Data.Employees;
using Microsoft.AspNetCore.Mvc;
using Tradmia.ApiTemplate.Application.Data.Employees.Dto;
using Tradmia.ApiTemplate.Domain.Entities.Employees;

namespace Tradmia.ApiTemplate.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {

        private readonly ILogger<EmployeesController> _logger;

        private readonly IEmployeeAppService _employeeAppService;

        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeAppService employeeAppService)
        {
            _logger = logger;
            _employeeAppService = employeeAppService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync(CreateEmployeeDto employee)
        {
            return await _employeeAppService.CreateAsync(employee);
        }

        [HttpPut]
        public async Task<IActionResult> Put(EmployeeDto employee)
        {
            await _employeeAppService.UpdateAsync(employee);
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateEmployeeDto employee)
        {
            await _employeeAppService.PatchAsync(employee);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Ok(await _employeeAppService.GetAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetByIdAsync(Guid id)
        {
            var employee = await _employeeAppService.GetByIdAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _employeeAppService.DeleteAsync(id);
            return NoContent();
        }

    }
}