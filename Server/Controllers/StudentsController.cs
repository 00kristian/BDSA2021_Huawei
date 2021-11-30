using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace ProjectBank.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class StudentsController : ControllerBase
{
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(ILogger<StudentsController> logger)
    {
        _logger = logger;
    }

    //[Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpGet("{id}")]
    public void GetProject(int id) {
        throw new NotImplementedException();
    }

    //[Authorize]
    [HttpPost]
    public void Post([FromBody] StudentDTO student) {
        throw new NotImplementedException();
    }

    //[Authorize]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] StudentDTO proj) {
        throw new NotImplementedException();
    }
}
public class StudentDTO
{
}