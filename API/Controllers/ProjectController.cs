using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(ILogger<ProjectController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetProject")]
    public IEnumerable<string> Get()
    {
        // using (var db = new ProjectBankContext())
        // {
        //     Console.WriteLine($"Database path: {db.DbPath}.");
            
        //     var PR = new ProjectRepository(db);

        //     return PR.ReadAllNames();   
        // }
        return new string[] {"hallo", "hej"};
    }
}