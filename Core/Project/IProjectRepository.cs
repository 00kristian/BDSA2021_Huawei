
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core {
public interface IProjectRepository
    {
        int Create(string name); //Ikke i vores vertical slice ifølge min menig - Lukas
        Task<IReadOnlyCollection<ProjectDTO>> ReadAll();

        ProjectDTO Read(int id);
        void Update(int id, ProjectDTO project);

        void Delete(int id);


    }
}