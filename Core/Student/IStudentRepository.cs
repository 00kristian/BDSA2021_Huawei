
using System.Collections.Generic;
using Infrastructure;

namespace Core {
public interface IStudentRepository
    {
        int Create(IStudent student);
        IReadOnlyCollection<IStudent> ReadAllNames();

        IStudent Read(int id);
        void Update(IStudent student);

        void Delete(int id);
    }
}