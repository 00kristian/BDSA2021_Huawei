
using System.Collections.Generic;

namespace Core {
public interface IStudentRepository
    {
        (Status, int id) Create(StudentDTO student);
        IReadOnlyCollection<StudentDTO> ReadAllNames();

        (Status, StudentDTO) Read(int id);
        Status Update(StudentDTO student);

        Status Delete(int id);
    }
}