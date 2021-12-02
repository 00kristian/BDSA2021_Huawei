
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Server.Controllers;

//[Authorize]
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

    [HttpGet(Name = "GetProject")]
    public async Task<IEnumerable<ProjectDTO>> Get()
    {
        return await _repo.ReadAll(); 
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] ProjectDTO proj) {
        _repo.Update(id, proj);
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ProjectDTO), 200)]
    [HttpGet("{id}")]
    public ProjectDTO GetProject(int id) {
        return _repo.Read(id).Item2;
    }
    
    //Er ikke i vores vertical slice
    [HttpPost("{name}")]
    public void Post(string name) {
        _repo.Create(name);
    }
}