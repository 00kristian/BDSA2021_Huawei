using System.Threading.Tasks;
using API.Controllers;
using EF_PB;
using Microsoft.Extensions.Logging;
using Moq;
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
    public async Task Update_changes_Project_in_repo()
    {
        //Arrange
        var logger = new Mock<ILogger<ProjectsController>>();
        var project = new ProjectDTO("newProject");
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.Update(1, project));
        var controller = new ProjectsController(logger.Object, repository.Object);

        // // Act
        // var response = await controller.Put(1, project);

        // // Assert
        // Assert.IsType<NoContentResult>(response);
    }
}
}