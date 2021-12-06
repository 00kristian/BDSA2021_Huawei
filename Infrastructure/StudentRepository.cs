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
            var p = await _context.students(s => s.Id == id).Select(s => new Student(TO){
                Degree = s.Degree,
                Name = s.Name!,
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

        public async Task<Status> Update(int id, StudentDTO student)
        {
            throw new NotImplementedException();
        } 
    }
}