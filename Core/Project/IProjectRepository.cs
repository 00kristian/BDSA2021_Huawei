
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core {
public interface IProjectRepository
    {
 Task<IReadOnlyCollection<ProjectDTO>> ReadAll();

        Task<(Status, ProjectDTO)> Read(int id);
        Task<Status> Update(int id, ProjectDTO project);
    }
}