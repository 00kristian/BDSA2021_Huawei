using System.Collections.Generic;
using System.Linq;
using Core;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Controllers;
using Xunit;

namespace Server.Tests{

public class ProjectsSearchControllerTests {

    [Fact]
    public async void Search_returns_search_result()
    {
          //Arrange
        var expected = new List<ProjectDTO>(){new ProjectDTO{
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
            }};
        var logger = new Mock<ILogger<SearchController>>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Search("AI")).ReturnsAsync(expected);
        var controller = new SearchController(logger.Object, repository.Object);

        // Act
        var actual = (await controller.Get("AI")).First();
        var e = expected.First();

        // Assert
        Assert.Equal(e.Name, actual.Name);
            Assert.Equal(e.Id, actual.Id);
            Assert.Equal(e.Description, actual.Description);
            Assert.Equal(e.DueDate, actual.DueDate);
            Assert.True(e.Keywords.SequenceEqual(actual.Keywords));
            Assert.Equal(e.Language, actual.Language);
            Assert.Equal(e.IntendedWorkHours, actual.IntendedWorkHours);
            Assert.Equal(e.SkillRequirementDescription, actual.SkillRequirementDescription);
            Assert.Equal(e.SupervisorName, actual.SupervisorName);
            Assert.Equal(e.IsThesis, actual.IsThesis);
            Assert.Equal(e.Location, actual.Location);
    }
    }
}