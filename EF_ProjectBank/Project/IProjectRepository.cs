
using System.Collections.Generic;
using Shared;

namespace EF_PB {
public interface IProjectRepository
    {
        int Create(IProject project);
        IReadOnlyCollection<IProject> ReadAllNames();

        IProject Read(int id);
        void Update(IProject project);

        void Delete(IProject project);


    }
}