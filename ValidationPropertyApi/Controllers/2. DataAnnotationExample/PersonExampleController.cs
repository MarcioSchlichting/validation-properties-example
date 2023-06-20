using Microsoft.AspNetCore.Mvc;

namespace ValidationPropertyApi.Controllers.DataAnnotationExample;

[ApiVersion("1.0")]
[ApiController, Route("v{version:apiVersion}/[controller]")]
public class PersonExampleController : ControllerBase
{
    /// <summary>
    /// Create a new Person -- DATA ANNOTATION TEST
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public ActionResult<Guid> Create([FromBody] Person person)
    {
        return Ok(Guid.NewGuid());
    }
}

