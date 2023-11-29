using Business.Abstract;
using ContactMicroServices.Controllers;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineStudyShared;
using OnlineStudyShared.Services;

namespace ContactTestProject.System.Controller;

public class InstructorControllerUnitTest 
{
    
     [Fact]
    public async Task GetAllAsync_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IInstructorService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new InstructorsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<List<Instructor>> { Data = new List<Instructor>() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.GetAllAsync()).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    
    public async Task GetByIdAsync_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IInstructorService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new InstructorsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Instructor> { Data = new Instructor() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetById(It.IsAny<int>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    public async Task CreateOrder_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IInstructorService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new InstructorsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Instructor> { Data = new Instructor() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.AddAsync(It.IsAny<Instructor>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.CreateOrder(It.IsAny<Instructor>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    
    public async Task Update_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IInstructorService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new InstructorsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Instructor> { Data = new Instructor() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.UpdateAsync(It.IsAny<Instructor>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Update(It.IsAny<Instructor>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    
    [Fact]
    
    public async Task Delete_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IInstructorService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new InstructorsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Instructor> { Data = new Instructor() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.DeleteAsync(It.IsAny<int>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Delete(It.IsAny<int>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    
    
    
    
    
    
}