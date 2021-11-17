using EF_PB;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> _logger;
    private ProjectRepository _repo;

    public ProjectController(ILogger<ProjectController> logger)
    {
        _logger = logger;
        var db = new ProjectBankContext();
        
        _repo = new ProjectRepository(db);
        
    }

    [HttpGet(Name = "GetProject")]
    public async Task<IEnumerable<string>> Get()
    {
        return await _repo.ReadAllNamesAsync(); 
    }

    //
    [HttpPost("{name}")]
    public void Post(string name) {
        _repo.Create(name);
    }
}