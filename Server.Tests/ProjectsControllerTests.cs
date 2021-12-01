using System;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectBank.Server.Controllers;
using Xunit;

namespace Server.Tests {

public class ProjectsControllerTests
{
    static readonly ProjectDTO p1 = new ProjectDTO("Project1", 1, "The first project ever", DateTime.MinValue, 8, "Many skills", false);
    static readonly ProjectDTO p2 = new ProjectDTO("Project2", 2, "The first thesis", DateTime.MinValue, 20, "Many skills", true);

    [Fact]
    public async Task Get_returns_Projects_from_repo()
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
    public void Get_given_existing_id_returns_Project()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var expected = p1;
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Read(1)).Returns(p1);
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
        var project = new ProjectDTO("oldProject", 1, "The first project ever", DateTime.MinValue, 8, "Many skills", false);;
        var update = p1;
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