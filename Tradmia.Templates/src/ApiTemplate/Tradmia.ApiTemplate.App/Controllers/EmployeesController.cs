using Application.Data.Employees;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;
using Microsoft.AspNetCore.Mvc;
using Tradmia.ApiTemplate.Application.Data.Employees.Dto;
using Tradmia.ApiTemplate.Domain.Entities.Employees;

namespace Tradmia.ApiTemplate.App.Controllers;

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


    [HttpPost("publish")]
    public async Task<ActionResult> Publish(string data)
    {
        await _employeeAppService.Publish(data);
        return NoContent();
    }

    [HttpPost("handleEvent")]
    public IActionResult HandleEvent([FromBody] EventGridEvent[] events)
    {

        foreach (var eventGridEvent in events)
        {

            if (eventGridEvent.TryGetSystemEventData(out object eventData))
            {
                // Handle the subscription validation event
                if (eventData is SubscriptionValidationEventData subscriptionValidationEventData)
                {
                    _logger.LogInformation($"Got SubscriptionValidation event data, validation code: {subscriptionValidationEventData.ValidationCode}, topic: {eventGridEvent.Topic}");
                    // Do any additional validation (as required) and then return back the below response
                    var responseData = new
                    {
                        ValidationResponse = subscriptionValidationEventData.ValidationCode
                    };

                    return new OkObjectResult(responseData);
                }
            }
            else if (eventGridEvent.EventType == "MyEvent")
            {
                // Handle your event here
                Console.WriteLine($"Data: {eventGridEvent.Data}");
            }
        }

        return Ok();
    }


}