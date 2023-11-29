using CatalogMicroServices.Business.Abstract;
using CatalogMicroServices.Controllers;
using CatalogMicroServices.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineStudyShared;

namespace CatalogTestProject.System.Controller;

public class CategoryControllerUnitTest
{
    [Fact]
    public async Task GetAll_ReturnsOkObjectResult_WhenCategorysExist()
    {
        // Arrange
        var mockCategoryService = new Mock<ICategoryService>();
        var controller = new CategoryController(mockCategoryService.Object);

        // Mock the service to return a list of Categorys
        var responseDto = new ResponseDto<List<CategoryDto>> { Data = new List<CategoryDto>() };
        mockCategoryService.Setup(service => service.GetAllAsync()).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
    }
    
    [Fact]
    
    public async Task GetById_ReturnsOkObjectResult_WhenCategoryExists()
    {
        // Arrange
        var mockCategoryService = new Mock<ICategoryService>();
        var controller = new CategoryController(mockCategoryService.Object);

        // Mock the service to return a Category
        var responseDto = new ResponseDto<CategoryDto> { Data = new CategoryDto() };
        mockCategoryService.Setup(service => service.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetById(It.IsAny<string>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
    }
    
    [Fact]
    public async Task Create_ReturnsOkObjectResult_WhenCategoryCreated()
    {
        // Arrange
        var mockCategoryService = new Mock<ICategoryService>();
        var controller = new CategoryController(mockCategoryService.Object);

        // Mock the service to return a response containing the created Category
        var createdCategory = new CategoryDto { /* Your test data here */ };
        var responseDto = new ResponseDto<CategoryDto> { Data = createdCategory };
        mockCategoryService.Setup(service => service.CreateAsync(It.IsAny<CreateCategoryDto>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Create(new CreateCategoryDto { /* Your test data here */ });

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
    }
    
    

    
    
    
    
    
    
    
}