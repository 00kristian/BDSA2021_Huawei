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
        ProjectBankContext _context;
        public StudentRepository(ProjectBankContext context)
        {
            _context = context;
        }

        public int Create(IStudent student)
        {
            throw new NotImplementedException();
        }

        public IStudent Read(int id){
            throw new NotImplementedException();
        }
        public void Update(IStudent student){}

        public void Delete(int id){}

        public IReadOnlyCollection<IStudent> ReadAllNames()
        {
             throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<StudentDTO>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void DELETE_ALL_STUDENTS_TEMPORARY()
        {
            _context.students.RemoveRange(_context.students);
            _context.SaveChanges();
        }

        public int Create(StudentDTO student)
        {
            throw new NotImplementedException();
        }

        IReadOnlyCollection<StudentDTO> IStudentRepository.ReadAllNames()
        {
            throw new NotImplementedException();
        }

        StudentDTO IStudentRepository.Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(StudentDTO student)
        {
            throw new NotImplementedException();
        }
    }
}