
using System.Collections.Generic;
using Shared;

namespace EF_PB {
public interface IProjectRepository
    {
        int Create(ProjectDTO project);
        IReadOnlyCollection<ProjectDTO> ReadAllNames();

        ProjectDTO Read(int id);
        void Update(ProjectDTO project);

        void Delete(int id);


    }
}