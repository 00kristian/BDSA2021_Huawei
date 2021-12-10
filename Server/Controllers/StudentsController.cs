using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server.Controllers;

//[Authorize]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly ILogger<StudentsController> _logger;

    private IStudentRepository _repo;

    public StudentsController(ILogger<StudentsController> logger, IStudentRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> Get(int id) {
        var res = await _repo.Read(id);
        if (res.Item1 == Status.NotFound) {
            return res.Item1.ToActionResult();
        } else {
            return new ActionResult<StudentDTO>(res.Item2);
        }
    }

    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<IActionResult> Post(StudentDTO student) {
        var created = await _repo.Create(student);
        var id = created.Item2;
        if (created.Item1 == Status.Conflict) return new ConflictObjectResult(id);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] StudentDTO student) =>
        (await _repo.Update(id, student)).ToActionResult();
}