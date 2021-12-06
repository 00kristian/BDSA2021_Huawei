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

        public async Task<IReadOnlyCollection<ProjectDTO>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public async Task<(Status, ProjectDTO)> Read(int id)
        {
            throw new NotImplementedException(); 
        }

        public async Task<Status> Update(int id, ProjectDTO project)
        {
            throw new NotImplementedException();
        }
    }
}