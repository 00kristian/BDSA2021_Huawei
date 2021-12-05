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

public class StudentsControllerTests
{
    static readonly StudentDTO s1 = new StudentDTO(2, "Jesus Kristus", 1, "jesus@Church.com", new DateTime(1, 12, 24), "The church");
    
    static readonly StudentDTO s2 = new StudentDTO(3, "Flemming", 1, "flem1999@gmail.com", new DateTime(1999, 8, 1), "The IT University of Copenhagen");

    [Fact]
    public void Get_given_existing_id_returns_Student()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var expected = s1;
        var repository = new Mock<IStudentRepository>();
        repository.Setup(m => m.Read(1)).Returns((Status.Found, s1));
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        var actual = controller.Get(1);

        // Assert
        Assert.Equal(expected, actual.Value);
    }

    [Fact]
    public void Get_given_non_existing_id_returns_NotFound()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var repository = new Mock<IStudentRepository>();
        repository.Setup(m => m.Read(999)).Returns((Status.NotFound, default(StudentDTO)));
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        var actual = controller.Get(999);

        // Assert
        Assert.IsType<NotFoundResult>(actual.Result);
    } 

    [Fact]
    public void Put_changes_Student_in_repo()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var student = new StudentDTO(2, "Jehova", 1, "jesus@Church.com", new DateTime(1, 12, 24), "The church");
        var update = s1;
        var repository = new Mock<IStudentRepository>();
        repository.Setup(m => m.Update(1, update)).Callback(() => student.Name = update.Name).Returns(Status.Updated);
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        controller.Put(1, update);

        // // Assert
        Assert.Equal(update, student);
    }

    [Fact]
    public void Put_given_non_existing_returns_NotFound()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var repository = new Mock<IStudentRepository>();
        repository.Setup(m => m.Update(999, s2)).Returns((Status.NotFound));
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        var actual = controller.Put(999, s2);

        // Assert
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public void Post_adds_student_to_repository() {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var repository = new Mock<IStudentRepository>();
        var students = new List<StudentDTO>{s1};
        repository.Setup(m => m.Create(s2)).Callback(() => students.Add(s2));
        var controller = new StudentsController(logger.Object, repository.Object);

        // Act
        var actual = controller.Post(s2);

        // Assert
        Assert.IsType<CreatedAtActionResult>(actual);
        Assert.Equal(2, students.Count);
    }
}
}