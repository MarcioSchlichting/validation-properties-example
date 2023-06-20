using Microsoft.AspNetCore.Mvc;
using ValidationPropertyApi.Controllers.DataAnnotationExample;

namespace ValidationPropertyApi.Controllers.ModelStateExample;

[ApiVersion("2.0")]
[ApiController, Route("v{version:apiVersion}/[controller]")]
public class PersonExampleController : ControllerBase
{
    /// <summary>
    /// Create a new Person -- Fluent Validation
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public ActionResult<Guid> Create([FromBody] Person person)
    {
        var validator = new PersonValidator();
        var validationResult = validator.Validate(person);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        
        return Ok(Guid.NewGuid());
    }
}