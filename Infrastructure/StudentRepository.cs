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
                if (stud.Name == student.Name) return (Status.Conflict, stud.Id);
            }
                var entity = new Student
                {
                    Name = student.Name!,
                    Degree = student.Degree,
                    Email = student.Email!,
                    DOB = student.DOB,
                    University = student.University,
                    AppliedProjects = await _context.projects.Include(p => p.Applications).Where(p => student.AppliedProjects.Contains(p.Id)).ToListAsync()
                };

                _context.students.Add(entity);

                await _context.SaveChangesAsync();

                return (Status.Created, entity.Id);
        }

        public async Task<(Status, int)> ReadIdFromName(string name) {
            int id = await _context.students.Where(s => s.Name == name).Select(s => s.Id).FirstOrDefaultAsync();
            return (id == 0 ? Status.NotFound : Status.Found, id);
        }

        public async Task<(Status, StudentDTO)> Read(int id)
        {
            var s = await _context.students.Include(s => s.AppliedProjects).Where(s => s.Id == id).Select(s => new StudentDTO(){
                Degree = s.Degree,
                Name = s.Name!,
                Id = s.Id,
                Email = s.Email!,
                DOB = s.DOB,
                University = s.University,
                AppliedProjects = s.AppliedProjects.Select(p => p.Id).ToList()}).FirstOrDefaultAsync();
;
            if (s == default(StudentDTO)) return (Status.NotFound, s);
            else return (Status.Found, s);
        }

        public async Task<Status> Update(int id, StudentDTO student)
        {
            var s = await _context.students.Where(s => s.Id == id).FirstOrDefaultAsync();

            if (s == default(Student)) return Status.NotFound;

            s.Degree = student.Degree;
            s.Email = student.Email!;
            s.DOB = student.DOB;
            s.University = student.University;
            s.AppliedProjects = await _context.projects.Include(p => p.Applications).Where(p => student.AppliedProjects.Contains(p.Id)).ToListAsync();

            await _context.SaveChangesAsync();

            return Status.Updated;
        }

        public async Task<(Status, PreferencesDTO)> ReadPreferences(int id)
        {
            var p = await _context.preferences.Include(p => p.Keywords).FirstOrDefaultAsync(p => p.StudentId == id);
            
            if (p == default(Preferences)) return (Status.NotFound, default(PreferencesDTO));

            var prefs = new PreferencesDTO() {
                Language = p.Language,
                Workdays = p.Workdays,
                Location = p.Location,
                Keywords = p.Keywords.Select(w => w.Str).ToList()
            };

            return(Status.Found, prefs);
        }

        public async Task<Status> UpdatePreferences(int id, PreferencesDTO prefs)
        {   
            try {
                var p = await _context.preferences.Include(p => p.Keywords).FirstOrDefaultAsync(p => p.StudentId == id);

                if (p == default(Preferences)) return Status.NotFound;

                p.Language = prefs.Language;
                p.Workdays = prefs.Workdays;
                p.Location = prefs.Location;
                p.Keywords = GetKeywords(prefs.Keywords).ToList();

                await _context.SaveChangesAsync();
                return Status.Updated;
            } catch (Exception e) {
                System.Console.WriteLine(e.StackTrace);
                return Status.BadRequest;
            }
        }

        //Helper function to the other methods 
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