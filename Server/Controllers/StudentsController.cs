using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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
    public ActionResult<StudentDTO> Get(int id) {
        var res = _repo.Read(id);
        if (res.Item1 == Status.NotFound) {
            return res.Item1.ToActionResult();
        } else {
            return new ActionResult<StudentDTO>(res.Item2);
        }
    }

    [ProducesResponseType(201)]
    [HttpPost]
    public IActionResult Post([FromBody] StudentDTO student) {
        var created = _repo.Create(student);

        return CreatedAtAction(nameof(Get), new { created.id }, created);
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] StudentDTO student) =>
        _repo.Update(id, student).ToActionResult();
}