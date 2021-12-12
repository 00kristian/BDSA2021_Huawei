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
                Location = p.Location,
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
                Location = p.Location,
                IsThesis = p.IsThesis,
                Meetingday = p.Meetingday,
                Keywords = p.Keywords!.Select(k => k.Str).ToList()!
            }).FirstOrDefaultAsync();

            if (p == default(ProjectDTO)) return (Status.NotFound, p);
            else return (Status.Found, p);
        }

        //Hey dette virker men det er jo egentlig ikke i vores vertical vvv
        public async Task<Status> Update(int id, ProjectDTO project)

        {
            var p = await _context.projects.Include(p => p.Keywords).FirstOrDefaultAsync(p => p.Id == id);


            if (p == default(Project)) return Status.NotFound;

            p.Name = project.Name!;
            p.Description = project.Description!;
            p.DueDate = project.DueDate;
            p.IntendedWorkHours = project.IntendedWorkHours;
            p.Language = project.Language;
            p.SkillRequirementDescription = project.SkillRequirementDescription!;
            p.SupervisorName = project.SupervisorName!;
            p.Location = project.Location;
            p.IsThesis = project.IsThesis;
            p.Keywords = GetKeywords(project.Keywords).ToList();

            _context.SaveChanges();

            return Status.Updated;
        }
        private IEnumerable<Keyword> GetKeywords(IEnumerable<string> keywords)
        {
            var existing = _context.keywords.Where(p => keywords.Contains(p.Str)).ToDictionary(p => p.Str);

            foreach (var k in keywords)
            {
                yield return existing.TryGetValue(k, out var p) ? p : new Keyword(){Str = k};
            }
        }

        public async Task<IEnumerable<string>> ReadAllKeywords()
        {
            return await _context.keywords.Select(k => k.Str).Distinct().ToListAsync();
        }

        public async Task<(Status, IEnumerable<ProjectDTO>)> Match(int id)
        {
            var prefs = await _context.preferences.Include(p => p.Keywords).FirstOrDefaultAsync(p => p.StudentId == id);

            if (prefs == default(Preferences)) return (Status.NotFound, new List<ProjectDTO>());

            var list = await _context.projects.Include(p => p.Keywords)//.OrderByDescending(p => p.Match(prefs))
            .Select(p => new ProjectDTO(){
                Name = p.Name!,
                Id = p.Id,
                Description = p.Description!,
                DueDate = p.DueDate,
                IntendedWorkHours = p.IntendedWorkHours,
                Language = p.Language,
                SkillRequirementDescription = p.SkillRequirementDescription!,
                SupervisorName = p.SupervisorName!,
                Location = p.Location,
                IsThesis = p.IsThesis,
                Meetingday = p.Meetingday,
                Keywords = p.Keywords!.Select(k => k.Str).ToList()!,
                Rating = p.Match(prefs)
            }).ToListAsync();

            return (Status.Found, list);
        }

        public async Task<IReadOnlyCollection<ProjectDTO>> Search(string searchString)
        {
            return await _context.projects.Include(p => p.Keywords)
            .Where(p => p.SupervisorName!.Contains(searchString) ||
            p.Keywords!.Select(k => k.Str).Contains(searchString))
            .Select(p => new ProjectDTO() {
                Name = p.Name!,
                Id = p.Id,
                Description = p.Description!,
                DueDate = p.DueDate,
                IntendedWorkHours = p.IntendedWorkHours,
                Language = p.Language,
                SkillRequirementDescription = p.SkillRequirementDescription!,
                SupervisorName = p.SupervisorName!,
                Location = p.Location,
                IsThesis = p.IsThesis,
                Meetingday = p.Meetingday,
                Keywords = p.Keywords!.Select(k => k.Str).ToList()!
            }).ToListAsync();
        }
    }
}