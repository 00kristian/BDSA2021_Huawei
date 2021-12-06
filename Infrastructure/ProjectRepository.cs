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
            throw new NotImplementedException();
        }

        public async Task<(Status, ProjectDTO)> Read(int id)
        {
            throw new NotImplementedException(); 
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