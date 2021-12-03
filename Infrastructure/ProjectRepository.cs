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
        public int Create(string name)
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
            _context.SaveChanges();

            return proj.Id;
        }

        public async Task<IReadOnlyCollection<ProjectDTO>> ReadAll()
        {
            return await _context.projects.Select(p => new ProjectDTO(p.Name!, p.Id, p.Description!, p.DueDate,
            p.IntendedWorkHours, p.Language, p.Keywords!, p.SkillRequirementDescription!, p.SupervisorName!, p.WorkDays!,p.Location, p.isThesis)).ToListAsync();
        }

        (Status, ProjectDTO) IProjectRepository.Read(int id)
        {
            var projects = from p in _context.projects
                         where p.Id == id
                         select new ProjectDTO(
                             p.Name!,
                             p.Id,
                             p.Description!,
                             p.DueDate,
                             p.IntendedWorkHours,
                             p.Language,
                             p.Keywords!,
                             p.SkillRequirementDescription!,
                             p.SupervisorName!,
                             p.WorkDays!,
                             p.Location,
                             p.isThesis
                         );

            var project = projects.FirstOrDefault();

            return project == default(ProjectDTO) ? (Status.NotFound, project) : (Status.Found, project);
        }

        public Status Update(int id, ProjectDTO project)
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