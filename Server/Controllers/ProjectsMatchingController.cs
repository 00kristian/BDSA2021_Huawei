
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server.Controllers;

[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
[Route("api/Projects/[controller]")]
public class MatchingController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private IProjectRepository _repo;

    public MatchingController(ILogger<ProjectsController> logger, IProjectRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [ProducesResponseType(200)]
    [HttpGet("{studentId}")]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> Get(int studentId)
    {
        var res = await _repo.Match(studentId);
        if (res.Item1 == Status.NotFound) {
            return res.Item1.ToActionResult();
        } else {
            return new ActionResult<IEnumerable<ProjectDTO>>(res.Item2);
        }
    }
}