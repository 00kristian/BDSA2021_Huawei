using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Controllers;
using Xunit;

namespace Server.Tests{

    public class StudentprefrencesControllerTests{

    static readonly StudentDTO s1 = new StudentDTO(
        DegreeEnum.Bachelor, 
        3,
        "Flemming", 
        1,
        "flem1999@gmail.com", 
        new DateTime(1999, 8, 1),
        UniversityEnum.ITU, 
        null!
        );
    


    [Fact]
    public async void Get_returns_preferences_given_existing_id()
    {
         //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        PreferencesDTO expected = new PreferencesDTO(){Language = LanguageEnum.Danish, Keywords= new String[]{"hej"}, Workdays= new WorkdayEnum[]{WorkdayEnum.Friday}, Location=LocationEnum.Onsite };
        var repository = new Mock<IStudentRepository>();    
        repository.Setup(m => m.ReadPreferences(1)).ReturnsAsync((Status.Found,expected));
        var controller = new PreferencesController(logger.Object, repository.Object);


        // Act
        var actual = await controller.Get(1);

        // Assert
        
        Assert.True(expected.Keywords.SequenceEqual(actual.Value.Keywords));
        Assert.True(expected.Workdays.SequenceEqual(actual.Value.Workdays));
        Assert.Equal(expected.Language, actual.Value.Language);
        Assert.Equal(expected.Location, actual.Value.Location);
    }

    [Fact]
    public async void Get_returns_notFoundResult_giving_nonExisting_idAsync()
    {
        var logger = new Mock<ILogger<StudentsController>>();
        PreferencesDTO expected = new PreferencesDTO(){Language = LanguageEnum.Danish, Keywords= new String[]{"hej"}, Workdays= new WorkdayEnum[]{WorkdayEnum.Friday}, Location=LocationEnum.Onsite };
        var repository = new Mock<IStudentRepository>();    
        repository.Setup(m => m.ReadPreferences(5)).ReturnsAsync((Status.NotFound, expected));
        var controller = new PreferencesController(logger.Object, repository.Object);

        
        // When
        var actual = await controller.Get(5);
    
        // Then
         Assert.IsType<NotFoundResult>(actual.Result);
        
    }

    [Fact]
    public async void Put_returns_notFoundResult_with_no_studentID()
    {
         var logger = new Mock<ILogger<StudentsController>>();
        PreferencesDTO prePreferences = new PreferencesDTO();
        var repository = new Mock<IStudentRepository>();    
        repository.Setup(m => m.UpdatePreferences(1,prePreferences)).ReturnsAsync(Status.NotFound);
        var controller = new PreferencesController(logger.Object, repository.Object);

        
        // When
        var actual = await controller.Put(1,prePreferences);
    
        // Then
         Assert.IsType<NotFoundResult>(actual);
    } 
    [Fact]
    public async void Put_updates_Preferences()
    {
         var logger = new Mock<ILogger<StudentsController>>();
        PreferencesDTO prePreferences = new PreferencesDTO();
        var repository = new Mock<IStudentRepository>();    
        repository.Setup(m => m.UpdatePreferences(1,prePreferences)).ReturnsAsync(Status.Updated);
        var controller = new PreferencesController(logger.Object, repository.Object);

        
        // When
        var actual = await controller.Put(1,prePreferences);
    
        // Then
         Assert.IsType<NoContentResult>(actual);
    } 


    }
}