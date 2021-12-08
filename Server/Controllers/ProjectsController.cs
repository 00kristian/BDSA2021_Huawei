
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server.Controllers;

//[Authorize]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private IProjectRepository _repo;

    public ProjectsController(ILogger<ProjectsController> logger, IProjectRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [ProducesResponseType(200)]
    [HttpGet(Name = "GetProjects")]
    public async Task<IEnumerable<ProjectDTO>> Get()
    {
        return await _repo.ReadAll(); 
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProjectDTO proj) =>
    (await _repo.Update(id, proj)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ProjectDTO), 200)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDTO>> GetProject(int id) {
        var res = await _repo.Read(id);
        if (res.Item1 == Status.NotFound) {
            return res.Item1.ToActionResult();
        } else {
            return new ActionResult<ProjectDTO>(res.Item2);
        }
    }
}