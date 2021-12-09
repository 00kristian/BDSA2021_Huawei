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
            Degree = DegreeEnum.Master,
            Email = "AlejanThough@gmail.com",
            DOB = new DateTime(2009, 4, 4),
            University = UniversityEnum.RUC,
            AppliedProjects = null,
            Preferences = new Preferences(){
                Keywords = new List<Keyword>(){new Keyword(){Str = "AI"}, new Keyword(){Str = "Programming"}},
                Locations = new List<LocationEnum>(){LocationEnum.Onsite},
                Workdays = new List<WorkdayEnum>(){WorkdayEnum.Monday, WorkdayEnum.Tuesday, WorkdayEnum.Friday},
                Language = LanguageEnum.English
            }
        };

        Student student2 = new Student{
            Name = "Britney Spears",
            Id = 2,
            Degree = DegreeEnum.PHD,
            Email = "ItsBritney@bitch.com",
            DOB = new DateTime(1981, 12, 2),
            University = UniversityEnum.CBS,
            AppliedProjects = null,
            Preferences = new Preferences(){
                Keywords = new List<Keyword>(){new Keyword(){Str = "Doing it again"}},
                Locations = new List<LocationEnum>(){LocationEnum.Onsite, LocationEnum.Remote},
                Workdays = new List<WorkdayEnum>(){WorkdayEnum.Friday, WorkdayEnum.Saturday},
                Language = LanguageEnum.English
            }
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
                Degree = DegreeEnum.Master,
                Email = "TheLady@gmail.com",
                DOB = new DateTime(2009, 4, 4),
                University = UniversityEnum.RUC,
                AppliedProjects = new List<int>()
            };

            //Act
            var actual = await _repo.Create(create);

            //Assert 
            Assert.Equal(Status.Created, actual.Item1);
            Assert.Equal(3, actual.Item2);
            var mail = _context.students.Where(s => s.Name == "Lady Gaga").Select(s => s.Email).FirstOrDefault();
            Assert.Equal("TheLady@gmail.com", mail);
        }

        [Fact]
        public async void Create_given_existing_name_returns_conflict() {
            //Arrange
            var create = new StudentDTO{
                Name = "Britney Spears",
                Id = 1,
                Degree = DegreeEnum.Master,
                Email = "ItsBritney@bitch.com",
                DOB = new DateTime(2009, 4, 4),
                University = UniversityEnum.RUC,
                AppliedProjects = new List<int>()
            };

            //Act
            var actual = await _repo.Create(create);

            //Assert 
            Assert.Equal(Status.Conflict, actual.Item1);
            Assert.Equal(2, actual.Item2);
        }

        [Fact]
        public async void Read_given_existing_id_returns_Student()
        {
            //Arrange
            var expected = new StudentDTO{
                Name = "Alejandro",
                Id = 1,
                Degree = DegreeEnum.Master,
                Email = "AlejanThough@gmail.com",
                DOB = new DateTime(2009, 4, 4),
                University = UniversityEnum.RUC,
                AppliedProjects = new List<int>()
            };

            //Act
            var res = await _repo.Read(1);
            var status = res.Item1;
            var actual = res.Item2;

            //Assert 
            Assert.Equal(Status.Found, status);
            Assert.Equal(expected.Degree, actual.Degree);
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.Name, actual.Name);
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
        public async void Update_updates_mail_given_new_mail(){
            //Arrange
            var update = new StudentDTO{
                Name = "Alejandro",
                Id = 1,
                Degree = DegreeEnum.Master,
                Email = "AlejanThough10000@gmail.com",
                DOB = new DateTime(2009, 4, 4),
                University = UniversityEnum.RUC,
                AppliedProjects = new List<int>()
            };

            //Act
            var status = await _repo.Update(1, update);

            //Assert
            var actual = _context.students.Where(p => p.Id == 1).FirstOrDefault();
            Assert.Equal(Status.Updated, status);
            Assert.Equal(DegreeEnum.Master, actual.Degree);
            Assert.Equal("AlejanThough10000@gmail.com", actual.Email);
            Assert.Equal("Alejandro", actual.Name);
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

        [Fact]
        public async void UpdatePreferences_given_Preferences_updates_student() {
            //Arrange
            var prefs = new PreferencesDTO(LanguageEnum.Danish,
            new List<string>(){"AI", "Programming", "Python"},
            new List<WorkdayEnum>(){WorkdayEnum.Monday, WorkdayEnum.Tuesday},
            new List<LocationEnum>(){LocationEnum.Remote});

            //Act
            var status = await _repo.UpdatePreferences(1, prefs);
            var student = await _context.students.FirstOrDefaultAsync(s => s.Id == 1);

            //Assert
            Assert.Equal(Status.Updated, status);
            Assert.Equal(3, student.Preferences.Keywords.Count);
            Assert.Equal("Python", student.Preferences.Keywords.Last().Str);
            Assert.Equal(2, student.Preferences.Workdays.Count);
            Assert.Equal(1, student.Preferences.Locations.Count);
            Assert.Equal(LanguageEnum.Danish, student.Preferences.Language);

        }

        [Fact]
        public async void UpdatePreferences_given_non_existing_id_returns_NOTFOUND() {
            //Arrange
            var prefs = new PreferencesDTO(LanguageEnum.Danish,
            new List<string>(){"AI", "Programming", "Python"},
            new List<WorkdayEnum>(){WorkdayEnum.Monday, WorkdayEnum.Tuesday},
            new List<LocationEnum>(){LocationEnum.Remote});

            //Act
            var status = await _repo.UpdatePreferences(1093, prefs);

            //Assert
            Assert.Equal(Status.NotFound, status);
        }

        [Fact]
        public async void ReadPreferences_given_existing_id_returns_PreferencesDTO() {
            //Arrange
            var expected = new PreferencesDTO() {
                Keywords = new List<string>(){"Doing it again"},
                Locations = new List<LocationEnum>(){LocationEnum.Onsite, LocationEnum.Remote},
                Workdays = new List<WorkdayEnum>(){WorkdayEnum.Friday, WorkdayEnum.Saturday},
                Language = LanguageEnum.English
            };

            //Act
            var res = await _repo.ReadPreferences(2);
            var status = res.Item1;
            var prefs = res.Item2;

            //Assert
            Assert.Equal(Status.Found, status);
            Assert.Equal(expected.Language, prefs.Language);
            Assert.Equal(expected.Language, prefs.Language);
            Assert.True(expected.Keywords.SequenceEqual(prefs.Keywords));
            Assert.True(expected.Locations.SequenceEqual(prefs.Locations));
            Assert.True(expected.Workdays.SequenceEqual(prefs.Workdays));
        }

        [Fact]
        public async void ReadPreferences_given_non_existing_id_returns_NOTFOUND() {
            //Arrange
            var expectedStatus = Status.NotFound;
            var expectedObject = default(PreferencesDTO);

            //Act
            var res = await _repo.ReadPreferences(7958);
            var actualStatus = res.Item1;
            var actualObject = res.Item2;

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal(expectedObject, actualObject);
        }
    }
}