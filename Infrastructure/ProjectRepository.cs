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

        public async Task<IReadOnlyCollection<ProjectDTO>> ReadAll() =>
            await _context.projects.Select(p => new ProjectDTO(){
                Name = p.Name!,
                Id = p.Id,
                Description = p.Description!,
                DueDate = p.DueDate,
                IntendedWorkHours = p.IntendedWorkHours,
                Language = p.Language,
                SkillRequirementDescription = p.SkillRequirementDescription!,
                SupervisorName = p.SupervisorName!,
                Location = p.Location!.Str,
                IsThesis = p.IsThesis,
                Keywords = p.Keywords!.Select(k => k.Str).ToList()!
            }).ToListAsync();

        public async Task<(Status, ProjectDTO)> Read(int id)
        {
            var p = await _context.projects.Where(p => p.Id == id).Select(p => new ProjectDTO(){
                Name = p.Name!,
                Id = p.Id,
                Description = p.Description!,
                DueDate = p.DueDate,
                IntendedWorkHours = p.IntendedWorkHours,
                Language = p.Language,
                SkillRequirementDescription = p.SkillRequirementDescription!,
                SupervisorName = p.SupervisorName!,
                Location = p.Location!.Str,
                IsThesis = p.IsThesis,
                Keywords = p.Keywords!.Select(k => k.Str).ToList()!
            }).FirstOrDefaultAsync();

            if (p == default(ProjectDTO)) return (Status.NotFound, p);
            else return (Status.Found, p);
        }

        //Hey dette virker men det er jo egentlig ikke i vores vertical vvv
        public async Task<Status> Update(int id, ProjectDTO project)
        {
            var p = await _context.projects.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (p == default(Project)) return Status.NotFound;

            p.Name = project.Name!;
            p.Id = project.Id;
            p.Description = project.Description!;
            p.DueDate = project.DueDate;
            p.IntendedWorkHours = project.IntendedWorkHours;
            p.Language = project.Language;
            p.SkillRequirementDescription = project.SkillRequirementDescription!;
            p.SupervisorName = project.SupervisorName!;
            p.Location = new Location() {Str = project.Location};
            p.IsThesis = project.IsThesis;
            p.Keywords = project.Keywords!.Select(k => new Keyword(){Str = k}).ToList()!;

            _context.SaveChanges();

            return Status.Updated;
        }
    }
}