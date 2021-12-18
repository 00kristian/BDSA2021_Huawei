using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.Data.Sqlite;
using Core;
using System.Linq;

namespace Infrastructure.Tests{

    public class ProjectRepositoryTests
    {
        private readonly IProjectBankContext _context;
        private readonly IProjectRepository _repo;

        Project project1 = new Project{
            Name = "AI-Project",
            Id = 1,
            Description = "If you like artificial intelligence this project is for you",
            DueDate = new System.DateTime(2021,12,30),
            IntendedWorkHours = 50,
            Language = LanguageEnum.English,
            Keywords = new HashSet<Keyword>{new Keyword(){Str = "Machine Learning"}, new Keyword(){Str = "Python"}},
            SkillRequirementDescription = "Intro to AI",
            SupervisorName = "Kåre",
            Location = LocationEnum.Onsite,
            IsThesis = false
        };

        Project project2 = new Project{
            Name = "Algorithms-Thesis",
            Id = 2,
            Description = "If you like Algorithms this thesis is for you",
            DueDate = new System.DateTime(2021,12,25),
            IntendedWorkHours = 100,
            Language = LanguageEnum.English,
            Keywords = new HashSet<Keyword>{new Keyword(){Str = "Algorithm"}},
            SkillRequirementDescription = "Intro to algorithms",
            SupervisorName = "Marie Dahl Esteban-Pedersen Sigurdsson",
            Location = LocationEnum.Onsite,
            IsThesis = true
        };
    
        public ProjectRepositoryTests(){
            
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<ProjectBankContext>();
            builder.UseSqlite(connection);
            var context = new ProjectBankContext(builder.Options);
            context.Database.EnsureCreated();
            
            context.projects.AddRange(
                project1,
                project2
            );
            

            context.SaveChanges();
            _context = context;
            _repo = new ProjectRepository(_context);
        }

        [Fact]
        public async void Read_given_existing_id_returns_Project()
        {
            //Arrange
            var expected = new ProjectDTO{
                Name = "AI-Project",
                Id = 1,
                Description = "If you like artificial intelligence this project is for you",
                DueDate = new System.DateTime(2021,12,30),
                IntendedWorkHours = 50,
                Language = LanguageEnum.English,
                Keywords = new List<string>{"Machine Learning", "Python"},
                SkillRequirementDescription = "Intro to AI",
                SupervisorName = "Kåre",
                Location = LocationEnum.Onsite,
                IsThesis = false 
            };

            //Act
            var actual = await _repo.Read(1);

            //Assert 
            Assert.Equal(Status.Found, actual.Item1);
            Assert.Equal(expected.Name, actual.Item2.Name);
            Assert.Equal(expected.DueDate, actual.Item2.DueDate);
            Assert.Equal(expected.Language, actual.Item2.Language);
            Assert.Equal(expected.Keywords.Count, actual.Item2.Keywords.Count);
        }

        [Fact]
        public async void Read_given_non_existing_id_returns_NotFound() {
            //Arrange
            var expected = default(ProjectDTO);

            //Act
            var actual = await _repo.Read(80);

            //Assert
            Assert.Equal(expected, actual.Item2);
            Assert.Equal(Status.NotFound, actual.Item1);
        }

        [Fact]
        public async void ReadAll_returns_all_projects() {
            //Arrange
            var expected = _context.projects.Count();

            //Act
            var actual = (await _repo.ReadAll()).Count();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void Update_updates_name_given_new_name(){
            //Arrange
            var update = new ProjectDTO{
                Name = "New AI-Project",
                Id = 1,
                Description = "If you like artificial intelligence this project is for you",
                DueDate = new System.DateTime(2021,12,30),
                IntendedWorkHours = 50,
                Language = LanguageEnum.English,
                Keywords = new List<string>{"Machine Learning", "Python"},
                SkillRequirementDescription = "Intro to machine learning",
                SupervisorName = "Kåre",
                Location = LocationEnum.Onsite,
                IsThesis = true 
            };

            //Act
            var status = await _repo.Update(1, update);

            //Assert
            var actual = _context.projects.Where(p => p.Id == 1).Select(p => p.Name).FirstOrDefault();
            Assert.Equal(Status.Updated, status);
            Assert.Equal("New AI-Project", actual);
        }

        [Fact]
        public async void Update_given_non_existing_id_returns_NotFound() {
            //Arrange
            var expected = Status.NotFound;

            //Act
            var actual = await _repo.Update(8999, default(ProjectDTO));

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void ReadAllKeyWords_returns_all_keywords_in_the_database()
        {
            // Given
            var keywords = await _repo.ReadAllKeywords();
            // When

            // Then
            Assert.Collection(keywords,
                keyword => Assert.Equal("Machine Learning", keyword),
                keyword => Assert.Equal("Python", keyword),
                keyword => Assert.Equal("Algorithm", keyword)
            );
        }

        [Fact]
        public async void Search_given_Keyword_returns_project_with_keyword() {
            //Arrange
            var expected = new ProjectDTO{
                Name = "AI-Project",
                Id = 1,
                Description = "If you like artificial intelligence this project is for you",
                DueDate = new System.DateTime(2021,12,30),
                IntendedWorkHours = 50,
                Language = LanguageEnum.English,
                Keywords = new List<string>{"Machine Learning", "Python"},
                SkillRequirementDescription = "Intro to AI",
                SupervisorName = "Kåre",
                Location = LocationEnum.Onsite,
                IsThesis = false 
            };
            
            //Act
            var actual = (await _repo.Search("Python")).First();

            //Assert
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.DueDate, actual.DueDate);
            Assert.True(expected.Keywords.SequenceEqual(actual.Keywords));
            Assert.Equal(expected.Language, actual.Language);
            Assert.Equal(expected.IntendedWorkHours, actual.IntendedWorkHours);
            Assert.Equal(expected.SkillRequirementDescription, actual.SkillRequirementDescription);
            Assert.Equal(expected.SupervisorName, actual.SupervisorName);
            Assert.Equal(expected.IsThesis, actual.IsThesis);
            Assert.Equal(expected.Location, actual.Location);
        }

        [Fact]

        public async void Match_returns_notFound_given_non_existing_studentID()
        {
            // Given
            var expected = Status.NotFound;
            
            // When
            var actual = await _repo.Match(33);
            // Then

            Assert.Equal(expected, actual.Item1);

            
        }

        [Fact]
        public async void Match_returns_statusFound_given_existing_studentID()
        {
            // Given
            var expected = Status.Found;

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
                Location = LocationEnum.Onsite,
                Workdays = new List<WorkdayEnum>(){WorkdayEnum.Monday, WorkdayEnum.Tuesday, WorkdayEnum.Friday},
                Language = LanguageEnum.English
            }
         };
            _context.students.Add(student1);
            _context.SaveChanges();
        
        
            // When
            var actual = await _repo.Match(1); 
        
            // Then
            Assert.Equal(expected, actual.Item1);
        }

        [Fact]
        public async void Search_given_Supervisorname_returns_project_with_supervisor() {
            //Arrange
            var expected = new ProjectDTO(){
                Name = "Algorithms-Thesis",
                Id = 2,
                Description = "If you like Algorithms this thesis is for you",
                DueDate = new System.DateTime(2021,12,25),
                IntendedWorkHours = 100,
                Language = LanguageEnum.English,
                Keywords = new List<string>{"Algorithm"},
                SkillRequirementDescription = "Intro to algorithms",
                SupervisorName = "Marie Dahl Esteban-Pedersen Sigurdsson",
                Location = LocationEnum.Onsite,
                IsThesis = true
            };
                
            //Act
            var actual = (await _repo.Search("Marie Dahl")).First();

            //Assert
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.DueDate, actual.DueDate);
            Assert.True(expected.Keywords.SequenceEqual(actual.Keywords));
            Assert.Equal(expected.Language, actual.Language);
            Assert.Equal(expected.IntendedWorkHours, actual.IntendedWorkHours);
            Assert.Equal(expected.SkillRequirementDescription, actual.SkillRequirementDescription);
            Assert.Equal(expected.SupervisorName, actual.SupervisorName);
            Assert.Equal(expected.IsThesis, actual.IsThesis);
            Assert.Equal(expected.Location, actual.Location);
        }
    }
}