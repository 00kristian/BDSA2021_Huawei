using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Core;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Controllers;
using Xunit;

namespace Server.Tests {

public class ProjectsControllerTests
{
    static readonly ProjectDTO p1 = new ProjectDTO{
        Name = "AI-Project",
        Id = 1,
        Description = "If you like artificial intelligence this project is for you",
        DueDate = new System.DateTime(2021,12,30),
        IntendedWorkHours = 50,
        Language = LanguageEnum.English,
        Keywords = new List<string>{"Machine Learning", "Python"},
        SkillRequirementDescription = "Intro to machine learning",
        SupervisorName = "Kåre",
        Location = "On site",
        IsThesis = true 
    };
    static readonly ProjectDTO p2 = new ProjectDTO{
        Name = "Projecgegih",
        Id = 2,
        Description = "I am veregy drunk arintb now htis is ja aa a prohec abotuhb maken the fijsr evert mavnhie leanrin algonroljn fojr dsf sibt goealfgelne",
        DueDate = new System.DateTime(2022,8,1),
        IntendedWorkHours = 50,
        Language = LanguageEnum.English,
        Keywords = new List<string>{"splat"},
        SupervisorName = "Flemming",
        IsThesis = false
    };
    [Fact]
    public async void Get_returns_Projects_from_repo()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var expected = new ProjectDTO[]{p1, p2};
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.ReadAll()).ReturnsAsync(expected);
        var controller = new ProjectsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Get_given_existing_id_returns_Project()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var expected = p1;
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Read(1)).ReturnsAsync((Status.Found, p1));
        var controller = new ProjectsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.GetProject(1);

        // Assert
        Assert.Equal(expected, actual.Value);
    }

    [Fact]
    public async void Get_given_non_existing_id_returns_NotFound()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Read(999)).ReturnsAsync((Status.NotFound, default(ProjectDTO)));
        var controller = new ProjectsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.GetProject(999);

        // Assert
        Assert.IsType<NotFoundResult>(actual.Result);
    } 

    [Fact]
    public async void Put_changes_Project_in_repo()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var update = new ProjectDTO{
            Name = "Sorry i was drunk",
            Id = 2,
            Description = "I am very sorry about my behavior last night i got way to drunk",
            DueDate = new System.DateTime(2022,8,1),
            IntendedWorkHours = 50,
            Language = LanguageEnum.English,
            Keywords = new List<string>{"splat"},
            SupervisorName = "Flemming",
            IsThesis = false
        };
        var project = p2;
        var repository = new Mock<IProjectRepository>();

        repository.Setup(m => m.Update(1, update)).Callback(() => {project.Name = update.Name;
        project.Description = update.Description;
    }).ReturnsAsync(Status.Updated);

        var controller = new ProjectsController(logger.Object, repository.Object);

        // Act
        await controller.Put(1, update);

        // // Assert

        Assert.Equal(update.Name, project.Name);
        Assert.Equal(update.Description, project.Description);

    }

    [Fact]
    public async void Put_given_non_existing_returns_NotFound()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Update(999, p2)).ReturnsAsync((Status.NotFound));
        var controller = new ProjectsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Put(999, p2);

        // Assert
        Assert.IsType<NotFoundResult>(actual);
    }
}
}