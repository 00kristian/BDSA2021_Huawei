using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core;

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

        public ProjectDTO Read(int projectId)
    {
        var projects = from p in _context.projects
                         where p.Id == projectId
                         select new ProjectDTO(
                             p.Name!,
                             p.Id,
                             p.Description!,
                             p.DueDate,
                             p.IntendedWorkHours,
                             //p.Language,
                             //p.Keywords,
                             p.SkillRequirementDescription!,
                             //p.Supervisor,
                             //p.WorkDays,
                             //p.Locations,
                             p.isThesis
                         );

        return projects.FirstOrDefault();
    }
        public void Update(int id, ProjectDTO project){}

        public void Delete(int id){}

        public IReadOnlyCollection<ProjectDTO> ReadAllNames()
        {
             throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<ProjectDTO>> ReadAll()
        {
            return await _context.projects.Select(p => new ProjectDTO(p.Name!, p.Id, p.Description!, p.DueDate,
            p.IntendedWorkHours, p.SkillRequirementDescription!, p.isThesis)).ToListAsync();
        }

        public async Task<IReadOnlyCollection<ProjectDTO>> ReadAllAsync()
        {
            return await _context.projects.Select(p => new ProjectDTO { Name = p.Name! }).ToListAsync();
        }

        public void DELETE_ALL_PROJECTS_TEMPORARY()
        {
            _context.projects.RemoveRange(_context.projects);
            _context.SaveChanges();
        }
    }
}