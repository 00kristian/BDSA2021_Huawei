using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{

    public class StudentRepository : IStudentRepository
    {
        IProjectBankContext _context;
        public StudentRepository(IProjectBankContext context)
        {
            _context = context;
        }

        public async Task<(Status, int id)> Create(StudentDTO student) {

            foreach (Student stud in _context.students) {
                if (stud.Email == student.Email) return (Status.Conflict, -1);
            }
            try {
                var entity = new Student
                {
                    Name = student.Name!,
                    Degree = (Degree) Enum.Parse(typeof(Degree), student.Degree, true),
                    Id = student.Id,
                    Email = student.Email!,
                    DOB = student.DOB,
                    University = (University) Enum.Parse(typeof(University), student.University, true),
                    AppliedProjects = await _context.projects.Where(p => student.AppliedProjects.Contains(p.Id)).ToListAsync()
                };

                _context.students.Add(entity);

                await _context.SaveChangesAsync();

                return (Status.Created, entity.Id);
            } catch (Exception e) {
                System.Console.WriteLine(e.StackTrace);
                return (Status.Conflict, -1);
            }
        }

        public async Task<(Status, StudentDTO)> Read(int id)
        {
            var s = await _context.students.Where(s => s.Id == id).Select(s => new StudentDTO(){
                Degree = s.Degree.ToString(),
                Name = s.Name!,
                Id = s.Id,
                Email = s.Email!,
                DOB = s.DOB,
                University = s.University.ToString(),
                AppliedProjects = s.AppliedProjects.Count > 0 ? s.AppliedProjects.Select(p => p.Id).ToList() : null!}).FirstOrDefaultAsync();
;

            if (s == default(StudentDTO)) return (Status.NotFound, s);
            else return (Status.Found, s);
        }

        public async Task<Status> Update(int id, StudentDTO student)
        {
            var s = await _context.students.Where(s => s.Id == id).FirstOrDefaultAsync();

            if (s == default(Student)) return Status.NotFound;

            s.Name = student.Name!;
            s.Degree = (Degree) Enum.Parse(typeof(Degree), student.Degree, true);
            s.Email = student.Email!;
            s.DOB = student.DOB;
            s.University = (University) Enum.Parse(typeof(University), student.University, true);
            s.AppliedProjects = await _context.projects.Where(p => student.AppliedProjects.Contains(p.Id)).ToListAsync();

            await _context.SaveChangesAsync();

            return Status.Updated;
        }

        public Task<(Status, PreferencesDTO)> ReadPreferences(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> UpdatePreferences(int id, PreferencesDTO prefs)
        {   
            try {
                var s = await _context.students.Include(s => s.Preferences).FirstAsync(s => s.Id == id);

                s.Preferences.Language = (LanguageEnum) Enum.Parse(typeof(LanguageEnum), prefs.Language, true);
                s.Preferences.Workdays = prefs.WorkDays.Select(d => (WorkdayEnum) Enum.Parse(typeof(WorkdayEnum), d, true)).ToList();
                s.Preferences.Locations = prefs.Locations.Select(d => (LocationEnum) Enum.Parse(typeof(LocationEnum), d, true)).ToList();
                s.Preferences.Keywords = GetKeywords(prefs.Keywords).ToList();

                await _context.SaveChangesAsync();
                return Status.Updated;
            } catch (Exception e) {
                System.Console.WriteLine(e.StackTrace);
                return Status.BadRequest;
            }
        }

        private IEnumerable<Keyword> GetKeywords(IEnumerable<string> keywords)
        {
            var existing = _context.keywords.Where(p => keywords.Contains(p.Str)).ToDictionary(p => p.Str);

            foreach (var k in keywords)
            {
                yield return existing.TryGetValue(k, out var p) ? p : new Keyword(){Str = k};
            }
        }
    }
}