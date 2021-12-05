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

        public (Status, int id) Create(StudentDTO student)
        {
            throw new NotImplementedException();
        }

        public Status Delete(int id)
        {
            throw new NotImplementedException();
        }

        public (Status, StudentDTO) Read(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<StudentDTO> ReadAllNames()
        {
            throw new NotImplementedException();
        }

        public Status Update(int id, StudentDTO student)
        {
            throw new NotImplementedException();
        }
    }
}