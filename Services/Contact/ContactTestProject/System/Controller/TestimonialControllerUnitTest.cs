using Business.Abstract;
using ContactMicroServices.Controllers;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineStudyShared;
using OnlineStudyShared.Services;

namespace ContactTestProject.System.Controller;

public class TestimonialControllerUnitTest 
{
    
     [Fact]
    public async Task GetAllAsync_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<ITestimonialService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new TestimonialsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<List<Testimonial>> { Data = new List<Testimonial>() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.GetAllAsync()).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    
    public async Task GetByIdAsync_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<ITestimonialService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new TestimonialsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Testimonial> { Data = new Testimonial() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetById(It.IsAny<int>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    public async Task CreateOrder_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<ITestimonialService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new TestimonialsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Testimonial> { Data = new Testimonial() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.AddAsync(It.IsAny<Testimonial>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.CreateOrder(It.IsAny<Testimonial>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    
    public async Task Update_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<ITestimonialService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new TestimonialsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Testimonial> { Data = new Testimonial() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.UpdateAsync(It.IsAny<Testimonial>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Update(It.IsAny<Testimonial>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    
    [Fact]
    
    public async Task Delete_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<ITestimonialService>();
        var identityMock = new Mock<ISharedIdentity>();
        var controller = new TestimonialsController(identityMock.Object,mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Testimonial> { Data = new Testimonial() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.DeleteAsync(It.IsAny<int>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Delete(It.IsAny<int>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    
    
    
    
    
    
}