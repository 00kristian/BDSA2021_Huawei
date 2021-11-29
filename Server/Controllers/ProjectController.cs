/* using Microsoft.AspNetCore.Mvc;


namespace Server{

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
        public async Task<IEnumerable<ProjectDTO>> Get()
        {
            return await _repo.ReadAllAsync(); 
        }

        [HttpPost("{name}")]
        public void Post(string name) {
            _repo.Create(name);
        }
    }
} */