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

            var entity = new Student
            {
                Name = student.Name!,
                Degree = (Degree) Enum.Parse(typeof(Degree), student.Degree, true),
                PreferenceId = student.PreferenceId,
                Id = student.Id,
                Email = student.Email!,
                DOB = student.DOB,
                University = (University) Enum.Parse(typeof(University), student.University, true),
                AppliedProjects = await _context.projects.Where(p => student.AppliedProjects.Contains(p.Id)).ToListAsync()
            };

        _context.students.Add(entity);

        await _context.SaveChangesAsync();

        return (Status.Created, entity.Id);
    }

        public async Task<(Status, StudentDTO)> Read(int id)
        {
            var s = await _context.students.Where(s => s.Id == id).Select(s => new StudentDTO(){
                Degree = s.Degree.ToString(),
                PreferenceId = s.PreferenceId,
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
            s.PreferenceId = student.PreferenceId;
            s.Id = student.Id;
            s.Email = student.Email!;
            s.DOB = student.DOB;
            s.University = (University) Enum.Parse(typeof(University), student.University, true);
            s.AppliedProjects = await _context.projects.Where(p => student.AppliedProjects.Contains(p.Id)).ToListAsync();

            await _context.SaveChangesAsync();

            return Status.Updated;
        }
    }
}