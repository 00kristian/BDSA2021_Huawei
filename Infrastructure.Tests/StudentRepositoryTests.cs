using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.Data.Sqlite;
using Core;
using System.Linq;

namespace Infrastructure.Tests{

    public class StudentRepositoryTests
    {
        private readonly IProjectBankContext _context;
        private readonly IStudentRepository _repo;

        Student student1 = new Student{
            Name = "Alejandro",
            Id = 1,
            Degree = Degree.Master,
            Email = "AlejanThough@gmail.com",
            DOB = new DateTime(2009, 4, 4),
            University = University.RUC,
            AppliedProjects = null
        };

        Student student2 = new Student{
            Name = "Britney Spears",
            Id = 2,
            Degree = Degree.PHD,
            Email = "ItsBritney@bitch.com",
            DOB = new DateTime(1981, 12, 2),
            University = University.CBS,
            AppliedProjects = null
        };
    
        public StudentRepositoryTests(){
            
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<ProjectBankContext>();
            builder.UseSqlite(connection);
            var context = new ProjectBankContext(builder.Options);
            context.Database.EnsureCreated();
            
            context.students.AddRange(
                student1,
                student2
            );

            context.SaveChanges();
            _context = context;
            _repo = new StudentRepository(_context);
        }

        [Fact]
        public async void Create_creates_new_student_in_repository() {
            //Arrange
            var create = new StudentDTO{
                Name = "Lady Gaga",
                Id = 0,
                Degree = "Master",
                Email = "AlejanThough@gmail.com",
                DOB = new DateTime(2009, 4, 4),
                University = "RUC",
                AppliedProjects = null
            };

            //Act
            var actual = await _repo.Create(create);

            //Assert 
            Assert.Equal(Status.Created, actual.Item1);
            Assert.Equal(3, actual.Item2);
            var mail = _context.students.Where(s => s.Name == "Lady Gaga").Select(s => s.Email).FirstOrDefault();
            Assert.Equal("Lady@Gaga.com", mail);
        }

        [Fact]
        public async void Create_given_existing_email_returns_conflict() {
            //Arrange
            var create = new StudentDTO{
                Name = "Lady Gaga",
                Id = 1,
                Degree = "Master",
                Email = "ItsBritney@bitch.com",
                DOB = new DateTime(2009, 4, 4),
                University = "RUC",
                AppliedProjects = null
            };

            //Act
            var actual = await _repo.Create(create);

            //Assert 
            Assert.Equal(Status.Conflict, actual.Item1);
            Assert.Equal(3, actual.Item2);
        }

        [Fact]
        public async void Read_given_existing_id_returns_Student()
        {
            //Arrange
            var expected = new StudentDTO{
                Name = "Alejandro",
                Id = 1,
                Degree = "Master",
                Email = "AlejanThough@gmail.com",
                DOB = new DateTime(2009, 4, 4),
                University = "RUC",
                AppliedProjects = null
            };

            //Act
            var actual = await _repo.Read(1);

            //Assert 
            Assert.Equal(Status.Found, actual.Item1);
            Assert.Equal(expected, actual.Item2);
        }

        [Fact]
        public async void Read_given_non_existing_id_returns_NotFound() {
            //Arrange
            var expected = default(StudentDTO);

            //Act
            var actual = await _repo.Read(55);

            //Assert
            Assert.Equal(expected, actual.Item2);
            Assert.Equal(Status.NotFound, actual.Item1);
        }

        [Fact]
        public async void Update_updates_name_given_new_name(){
            //Arrange
            var update = new StudentDTO{
                Name = "Lady Gaga",
                Id = 1,
                Degree = "Master",
                Email = "AlejanThough@gmail.com",
                DOB = new DateTime(2009, 4, 4),
                University = "RUC",
                AppliedProjects = null
            };

            //Act
            var status = await _repo.Update(1, update);

            //Assert
            var actual = _context.projects.Where(p => p.Id == 1).Select(p => p.Name).FirstOrDefault();
            Assert.Equal(Status.Updated, status);
            Assert.Equal("Lady Gaga", actual);
        }

        [Fact]
        public async void Update_given_non_existing_id_returns_NotFound() {
            //Arrange
            var expected = Status.NotFound;

            //Act
            var actual = await _repo.Update(2920, default(StudentDTO));

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}