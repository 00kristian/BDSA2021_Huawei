
using System.Collections.Generic;
using Shared;

namespace EF_PB {
public interface IStudentRepository
    {
        int Create(IStudent student);
        IReadOnlyCollection<IStudent> ReadAllNames();

        IStudent Read(int id);
        void Update(IStudent student);

        void Delete(int id);
    }
}