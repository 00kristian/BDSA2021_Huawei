using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_PB
{

    public class ProjectRepository : IProjectRepository
    {
        ProjectBankContext _context;
        public ProjectRepository(ProjectBankContext context)
        {
            _context = context;
        }

        public int Create(IProject project)
        {
            foreach (var p in _context.projects)
            {
                if (p.Name == project.Name)
                {
                    return (p.Id);
                }
            }

            /*var proj = new Project
            {
                Name = project.Name
            };
            _context.projects.Add(proj);
            _context.SaveChanges();

            return proj.ProjectID;*/
            return 0;
        }

        public IProject Read(int id){
            throw new NotImplementedException();
        }
        public void Update(IProject project){}

        public void Delete(IProject project){}

        public IReadOnlyCollection<IProject> ReadAllNames()
        {
             throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<string>> ReadAllNamesAsync()
        {
            return await _context.projects.Select(p => p.Name).ToListAsync();
        }

        public async Task<IReadOnlyCollection<ProjectDTO>> ReadAllAsync()
        {
            return await _context.projects.Select(p => new ProjectDTO { Name = p.Name }).ToListAsync();
        }

        public void DELETE_ALL_PROJECTS_TEMPORARY()
        {
            _context.projects.RemoveRange(_context.projects);
            _context.SaveChanges();
        }
    }
}