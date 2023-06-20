using Flunt.Validations;
using Microsoft.AspNetCore.Mvc;

namespace ValidationPropertyApi.Controllers.FluntValidator;

[ApiVersion("4.0")]
[ApiController, Route("v{version:apiVersion}/[controller]")]
public class PersonExampleController : ControllerBase
{
    /// <summary>
    /// Create a new Person -- Flunt Validation
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public ActionResult<Guid> Create([FromBody] Person person)
    {
        var contract = new Contract<Person>()
            .Requires()
            .IsNotNullOrEmpty(person.Name, nameof(person.Name), "Name is required.")
            .IsGreaterThan(person.Age, 0, nameof(person.Age), "Age must be greater than 0.");

        if (!contract.IsValid)
        {
            return BadRequest(contract.Notifications);
        }
        
        return Ok(Guid.NewGuid());
    }
}