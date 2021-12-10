using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server.Controllers;

//[Authorize]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
[Route("api/Students/[controller]")]
public class PreferencesController : ControllerBase
{
    private readonly ILogger<StudentsController> _logger;

    private IStudentRepository _repo;

    public PreferencesController(ILogger<StudentsController> logger, IStudentRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PreferencesDTO>> Get(int id) {
        var res = await _repo.ReadPreferences(id);
        if (res.Item1 == Status.NotFound) {
            return res.Item1.ToActionResult();
        } else {
            return new ActionResult<PreferencesDTO>(res.Item2);
        }
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpPut("{studentId}")]
    public async Task<IActionResult> Put(int studentId, [FromBody] PreferencesDTO preferences) =>
        (await _repo.UpdatePreferences(studentId, preferences)).ToActionResult();
}