using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Controllers;
using Xunit;

namespace Server.Tests {

public class StudentsControllerTests
{
    static readonly StudentDTO s1 = new StudentDTO(DegreeEnum.PHD, 2, "Jesus Kristus", 1, "jesus@Church.com", new DateTime(1, 12, 24), UniversityEnum.DTU, null!);
    
    static readonly StudentDTO s2 = new StudentDTO(DegreeEnum.Bachelor, 3, "Flemming", 1, "flem1999@gmail.com", new DateTime(1999, 8, 1), UniversityEnum.ITU, null!);


    [Fact]
    public async void Get_given_existing_id_returns_Student()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var expected = s1;
        var repository = new Mock<IStudentRepository>();
        repository.Setup(m => m.Read(1)).ReturnsAsync((Status.Found, s1));
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get(1);

        // Assert
        Assert.Equal(expected, actual.Value);
    }

    [Fact]
    public async void Get_given_non_existing_id_returns_NotFound()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var repository = new Mock<IStudentRepository>();
        repository.Setup(m => m.Read(999)).ReturnsAsync((Status.NotFound, default(StudentDTO)));
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get(999);

        // Assert
        Assert.IsType<NotFoundResult>(actual.Result);
    } 

    [Fact]
    public async void Put_changes_Student_in_repo()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();

        var update = new StudentDTO(DegreeEnum.PHD, 2, "Jehova", 1, "jesus@Church.com", new DateTime(1, 12, 24), UniversityEnum.DTU, null!);
        var student = s1;

        var repository = new Mock<IStudentRepository>();
        repository.Setup(m => m.Update(1, update)).Callback(() => student.Name = update.Name).ReturnsAsync(Status.Updated);
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        await controller.Put(1, update);

        // // Assert
        Assert.Equal(update.Name, student.Name);

    }

    [Fact]
    public async void Put_given_non_existing_returns_NotFound()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var repository = new Mock<IStudentRepository>();
        repository.Setup(m => m.Update(999, s2)).ReturnsAsync((Status.NotFound));
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Put(999, s2);

        // Assert
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async void Post_adds_student_to_repository() {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var repository = new Mock<IStudentRepository>();
        var students = new List<StudentDTO>{s1};
        repository.Setup(m => m.Create(s2)).Callback(() => students.Add(s2));
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Post(s2);

        // Assert
        Assert.IsType<CreatedAtActionResult>(actual);
        Assert.Equal(2, students.Count);
    }

    [Fact]
    public async void Post_returns_StatusConclict_because_name_already_is_in_database()
    {
        // Given
        var logger = new Mock<ILogger<StudentsController>>();
        var repository = new Mock<IStudentRepository>();
        var students = new List<StudentDTO>{s1};
         StudentDTO s3 = new StudentDTO(DegreeEnum.Bachelor, 2, "Jesus Kristus", 1, "Hvilket@hav.com", new DateTime(1999, 8, 1), UniversityEnum.ITU, null!);
        repository.Setup(m => m.Create(s3)).ReturnsAsync(() => (Status.Conflict, 1) );
        var controller = new StudentsController(logger.Object, repository.Object);


        // When
        var actual = await controller.Post(s3);
    
        // Then
        Assert.IsType<ConflictObjectResult>(actual);
        Assert.Equal(1,students.Count);
    
    }
}
}