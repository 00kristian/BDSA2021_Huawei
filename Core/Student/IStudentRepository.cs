
using System.Collections.Generic;

namespace Core {
public interface IStudentRepository
    {
        int Create(StudentDTO student);
        IReadOnlyCollection<StudentDTO> ReadAllNames();

        StudentDTO Read(int id);
        void Update(StudentDTO student);

        void Delete(int id);
    }
}