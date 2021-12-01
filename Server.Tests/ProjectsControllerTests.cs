using System;
using System.Threading.Tasks;
using EF_PB;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectBank.Server.Controllers;
using Xunit;

namespace Server.Tests {

public class ProjectsControllerTests
{
    [Fact]
    public async Task Get_returns_Projects_from_repo()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var expected = new ProjectDTO[]{new ProjectDTO("Project1"), new ProjectDTO("Project2")};
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.ReadAll()).ReturnsAsync(expected);
        var controller = new ProjectsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Get_given_existing_id_returns_Project()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var expected = new ProjectDTO("Project1");
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Read(1)).Returns(expected);
        var controller = new ProjectsController(logger.Object, repository.Object);

        // Act
        var actual = controller.GetProject(1);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Update_changes_Project_in_repo()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var project = new ProjectDTO("Project");
        var update = new ProjectDTO("NewName");
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Update(1, update)).Callback(() => project.Name = update.Name);
        var controller = new ProjectsController(logger.Object, repository.Object);

        // Act
        controller.Put(1, update);

        // // Assert
        Assert.Equal(project, update);
    }
}
}