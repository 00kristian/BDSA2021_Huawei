/* using EF_PB;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Shared;
using Xunit;

namespace Model.Tests{

    public class StudentRepositoryTests
    {
        private readonly ProjectBankContext _context;
        private readonly ProjectRepository _repo;
        public StudentRepositoryTests(){
            var connection = new SqliteConnection("Filename=:memory:");
            var builder = new DbContextOptionsBuilder<ProjectBankContext>();
            builder.UseSqlite(connection);

            var context = new ProjectBankContext(builder.Options);
            connection.Open();

            var student1 = new Student{
                Degree = Degree.Bachelor,
                Preferences = new Preferences(),
                Name = "Camille Gonnsen",
                Id = 1,
                Email = "cgon@itu.dk",
                DOB = new System.DateTime(1999,08,10),
                University = University.ITU
            };

            context.students.AddRange(
                student1
            );

            context.SaveChanges();
            _context = context;
            _repo = new ProjectRepository(_context);
        }
        
        [Fact]
        public void Test1()
        {

        }
    }
}
 */