using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EF_PB;

public class ProjectRepository : IProjectRepository
{
    ProjectBankContext _context;
    public ProjectRepository(ProjectBankContext context)
    {
        _context = context;
    }

    public int Create(string name)
    {
        foreach (var p in _context.projects) {
            if (p.Name == name) {
                return (p.ProjectID);
            }
        }

        var proj = new Project{
            Name = name
        };
        _context.projects.Add(proj);
        _context.SaveChanges();

        return proj.ProjectID;
    }

    public IReadOnlyCollection<string> ReadAllNames()
    {
        return _context.projects.Select(p => p.Name).ToList();
    }

    public async Task<IReadOnlyCollection<string>> ReadAllNamesAsync()
    {
        return await _context.projects.Select(p => p.Name).ToListAsync();
    }

    public async Task<IReadOnlyCollection<ProjectDTO>> ReadAllAsync()
    {
        return await _context.projects.Select(p => new ProjectDTO{Name = p.Name}).ToListAsync();
    }

    public void DELETE_ALL_PROJECTS_TEMPORARY() {
        _context.projects.RemoveRange(_context.projects);
        _context.SaveChanges();
    }
}