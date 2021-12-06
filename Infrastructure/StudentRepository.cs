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

        public async Task<(Status, int id)> Create(StudentDTO student)
        {
            throw new NotImplementedException();
        }

        public async Task<(Status, StudentDTO)> Read(int id)
        {
            var s = await _context.students.Where(s => s.Id == id).Select(s => new StudentDTO(){
                Degree = s.Degree.ToString(),
                PreferenceId = s.Preferences!.Id,
                Name = s.Name!,
                Id = s.Id,
                Email = s.Email!,
                DOB = s.DOB,
                University = s.University.ToString(),
                //Dette er den grimmeste kode nogensinde, fix den
                AppliedProjects = (System.Collections.Generic.ICollection<Core.ProjectDTO>) s.AppliedProjects.Select(p => new ProjectDTO(){
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
                })
            }).FirstOrDefaultAsync();

            if (s == default(StudentDTO)) return (Status.NotFound, s);
            else return (Status.Found, s);
        }

        public async Task<Status> Update(int id, StudentDTO student)
        {
            throw new NotImplementedException();
        } 
    }
}