using CatalogMicroServices.Business.Abstract;
using CatalogMicroServices.Controllers;
using CatalogMicroServices.Dtos.Course;
using CatalogMicroServices.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineStudyShared;
using OnlineStudyShared.Services;

namespace CatalogTestProject.System.Controller;

public class CourseControllerUnitTest
{
    [Fact]
    public async Task GetAll_ReturnsOkObjectResult_WhenCoursesExist()
    {
        // Arrange
        var mockCourseService = new Mock<ICourseService>();
        var controller = new CourseController(mockCourseService.Object);

        // Mock the service to return a list of courses
        var responseDto = new ResponseDto<List<CourseDto>> { Data = new List<CourseDto>() };
        mockCourseService.Setup(service => service.GetAllAsync()).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
    }
    
    [Fact]
    
    public async Task GetById_ReturnsOkObjectResult_WhenCourseExists()
    {
        // Arrange
        var mockCourseService = new Mock<ICourseService>();
        var controller = new CourseController(mockCourseService.Object);

        // Mock the service to return a course
        var responseDto = new ResponseDto<CourseDto> { Data = new CourseDto() };
        mockCourseService.Setup(service => service.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetById(It.IsAny<string>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
    }
    
    [Fact]
    public async Task Create_ReturnsOkObjectResult_WhenCourseCreated()
    {
        // Arrange
        var mockCourseService = new Mock<ICourseService>();
        var controller = new CourseController(mockCourseService.Object);

        // Mock the service to return a response containing the created course
        var createdCourse = new CourseDto { /* Your test data here */ };
        var responseDto = new ResponseDto<CourseDto> { Data = createdCourse };
        mockCourseService.Setup(service => service.CreateAsync(It.IsAny<CreateCourseDto>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Create(new CreateCourseDto { /* Your test data here */ });

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
    }

    
    
    [Fact]
    public async Task Update_ReturnsOkObjectResult_WhenCourseUpdated()
    {
        // Arrange
        var mockCourseService = new Mock<ICourseService>();
        var controller = new CourseController(mockCourseService.Object);

        // Mock the service to return a response indicating success with no content
        var responseDto = new ResponseDto<NoContent> { Data = new NoContent() };
        mockCourseService.Setup(service => service.UpdateAsync(It.IsAny<CourseUpdateDto>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Update(new CourseUpdateDto { /* Your test data here */ });

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
    }
    
    
    [Fact]
    
    public async Task Delete_ReturnsOkObjectResult_WhenCourseDeleted()
    {
        // Arrange
        var mockCourseService = new Mock<ICourseService>();
        var controller = new CourseController(mockCourseService.Object);

        // Mock the service to return a response indicating success with no content
        var responseDto = new ResponseDto<NoContent> { Data = new NoContent() };
        mockCourseService.Setup(service => service.DeleteAsync(It.IsAny<string>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Delete(It.IsAny<string>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
    }

    
}