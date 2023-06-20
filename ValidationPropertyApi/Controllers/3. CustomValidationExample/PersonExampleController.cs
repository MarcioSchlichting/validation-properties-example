using Microsoft.AspNetCore.Mvc;

namespace ValidationPropertyApi.Controllers.CustomValidationExample;

[ApiVersion("3.0")]
[ApiController, Route("v{version:apiVersion}/[controller]")]
public class PersonExampleController : ControllerBase
{
    /// <summary>
    /// Create a new Person -- Custom Validation
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public ActionResult<Guid> Create([FromBody] Person person)
    {
        return Ok(Guid.NewGuid());
    }
}