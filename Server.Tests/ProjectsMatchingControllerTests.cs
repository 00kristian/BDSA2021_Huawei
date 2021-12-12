using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Controllers;
using Xunit;


namespace Server.Tests{

public class ProjectsMatchingControllerTests{

    [Fact]
    public async void Match_returns_notFoundResult_when_giving_non_existing_studentID()
    {
          //Arrange
        var expected = new List<ProjectDTO>();
        var logger = new Mock<ILogger<ProjectsController>>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Match(999)).ReturnsAsync((Status.NotFound, expected));
        var controller = new MatchingController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get(999);

        // Assert
        Assert.IsType<NotFoundResult>(actual.Result);
    }

    [Fact]
    public async void Match_returns_foundResult_when_given_existing_StudentID()
    {
          //Arrange

        

        var expected = new List<ProjectDTO>(){new ProjectDTO(){Name= "OScar WIlde", Id=1, Description="Lucas hej", SupervisorName="Fisk", Keywords= new List<string>{}, DueDate = new DateTime(1,5,1), Language=LanguageEnum.Danish, SkillRequirementDescription="Hej", Location=LocationEnum.Onsite, IsThesis=false, Meetingday=WorkdayEnum.Friday, Rating=1 }};
        var logger = new Mock<ILogger<ProjectsController>>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Match(1)).ReturnsAsync((Status.Found, expected));
        var controller = new MatchingController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get(1); 

        // Assert
        
    }







    }
}