
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server.Controllers;

[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
[Route("api/Projects/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private IProjectRepository _repo;

    public SearchController(ILogger<SearchController> logger, IProjectRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [ProducesResponseType(200)]
    [HttpGet("{searchString}")]
    public async Task<IEnumerable<ProjectDTO>> Get(string searchString)
    {
        return await _repo.Search(searchString); 
    }
}