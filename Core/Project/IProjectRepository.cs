
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core {
public interface IProjectRepository
    {
        Task<int> Create(string name); //Ikke i vores vertical slice ifølge min menig - Lukas
        Task<IReadOnlyCollection<ProjectDTO>> ReadAll();

        Task<(Status, ProjectDTO)> Read(int id);
        Task<Status> Update(int id, ProjectDTO project);
    }
}