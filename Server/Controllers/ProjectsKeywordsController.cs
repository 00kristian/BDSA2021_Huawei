
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server.Controllers;

//[Authorize]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
[Route("api/Projects/[controller]")]
public class KeywordsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private IProjectRepository _repo;

    public KeywordsController(ILogger<ProjectsController> logger, IProjectRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [ProducesResponseType(200)]
    [HttpGet(Name = "Keywords")]
    public async Task<IEnumerable<string>> Get()
    {
        return await _repo.ReadAllKeywords(); 
    }
}