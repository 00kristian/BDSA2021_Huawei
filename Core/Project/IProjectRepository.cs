
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core {
public interface IProjectRepository
    {
        int Create(ProjectDTO project);
        Task<IReadOnlyCollection<ProjectDTO>> ReadAll();

        ProjectDTO Read(int id);
        void Update(int id, ProjectDTO project);

        void Delete(int id);


    }
}