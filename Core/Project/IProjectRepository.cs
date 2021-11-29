
using System.Collections.Generic;

namespace Core {
public interface IProjectRepository
    {
        int Create(ProjectDTO project);
        IReadOnlyCollection<ProjectDTO> ReadAllNames();

        ProjectDTO Read(int id);
        void Update(ProjectDTO project);

        void Delete(int id);


    }
}