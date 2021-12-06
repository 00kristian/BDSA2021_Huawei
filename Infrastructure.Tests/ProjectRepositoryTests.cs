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
            Location = new Location(){Str = "On site"},
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
            Location = new Location(){Str = "Online"},
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
                Location = "On site",
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
                Location = "OnSite",
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
    }
}