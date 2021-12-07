
using System.Collections.Generic;

namespace Core {
public interface IStudentRepository
    {
        Task<(Status, int id)> Create(StudentDTO student);
        Task<(Status, StudentDTO)> Read(int id);
        Task<Status> Update(int id, StudentDTO student);
        Task<(Status, PreferencesDTO)> ReadPreferences(int id);
        Task<Status> UpdatePreferences(int id, PreferencesDTO prefs);
    }
}