using EF_PB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace API.Controllers;

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

    //[Authorize]
    [HttpGet(Name = "GetProject")]
    public async Task<IEnumerable<ProjectDTO>> Get()
    {
        return await _repo.ReadAll(); 
    }

    //[Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public void Put(int id, [FromBody] ProjectDTO proj) {
        _repo.Update(id, proj);
    }
    
    //Er ikke i vores vertical slice
    [HttpPost("{name}")]
    public void Post(string name) {
        _repo.Create(new ProjectDTO(name));
    }
}