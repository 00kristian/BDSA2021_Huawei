
using System.Collections.Generic;

namespace EF_PB {
public interface IProjectRepository
    {
        int Create(string name);
        IReadOnlyCollection<string> ReadAllNames();
    }
}