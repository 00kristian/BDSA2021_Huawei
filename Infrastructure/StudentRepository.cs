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
            throw new NotImplementedException();
        }

        public async Task<Status> Update(int id, StudentDTO student)
        {
            throw new NotImplementedException();
        }
    }
}