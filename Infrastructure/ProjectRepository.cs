using System.Linq;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{

    public class ProjectRepository : IProjectRepository
    {
        IProjectBankContext _context;
        public ProjectRepository(IProjectBankContext context)
        {
            _context = context;
        }

        //Ikke i vores vertical slice
        public async Task<int> Create(string name)
        {
            foreach (var p in _context.projects)
            {
                if (p.Name == name)
                {
                    return (p.Id);
                }
            }

            var proj = new Project
            {
                Name = name
            };
            _context.projects.Add(proj);
            await _context.SaveChangesAsync();

            return proj.Id;
        }

        public async Task<IReadOnlyCollection<ProjectDTO>> ReadAll()
        {
            return await _context.projects.Select(p => new ProjectDTO(p.Name!, p.Id, p.Description!, p.DueDate,
            p.IntendedWorkHours, /*p.Language, p.Keywords, */p.SkillRequirementDescription!, p.SupervisorName!, /*p.WorkDays, p.Location,*/ p.isThesis)).ToListAsync();
        }

        public async Task<(Status, ProjectDTO)> Read(int id)
        {
            var projects = from p in _context.projects
                         where p.Id == id
                         select new ProjectDTO(
                             p.Name!,
                             p.Id,
                             p.Description!,
                             p.DueDate,
                             p.IntendedWorkHours,
                             //p.Language,
                             //p.Keywords,
                             p.SkillRequirementDescription!,
                             p.SupervisorName!,
                             //p.WorkDays,
                             //p.Location,
                             p.isThesis
                         );

            var project = await projects.FirstOrDefaultAsync();

            return project == default(ProjectDTO) ? (Status.NotFound, project) : (Status.Found, project);
        }

        public async Task<Status> Update(int id, ProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public void DELETE_ALL_PROJECTS_TEMPORARY()
        {
            _context.projects.RemoveRange(_context.projects);
            _context.SaveChanges();
        }
    }
}