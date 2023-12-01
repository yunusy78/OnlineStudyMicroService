using System.Net;
using Business.Abstract;
using ContactMicroServices.Controllers;
using ContactTestProject.MockData;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineStudyShared;

namespace ContactTestProject.System.Controller;

public class ContactControllerUnitTest
{
    [Fact]
    public async Task GetAllAsync_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IContactService>();
        var controller = new ContactsController(mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<List<Contact>> { Data = new List<Contact>() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.GetAllAsync()).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    
    public async Task GetByIdAsync_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IContactService>();
        var controller = new ContactsController(mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Contact> { Data = new Contact() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.GetById(It.IsAny<int>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    public async Task CreateOrder_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IContactService>();
        var controller = new ContactsController(mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Contact> { Data = new Contact() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.AddAsync(It.IsAny<Contact>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.CreateContact(It.IsAny<Contact>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    [Fact]
    
    public async Task Update_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IContactService>();
        var controller = new ContactsController(mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Contact> { Data = new Contact() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.UpdateAsync(It.IsAny<Contact>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Update(It.IsAny<Contact>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    
    
    [Fact]
    
    public async Task Delete_ShouldReturn200Status()
    {
        var mockDiscountService = new Mock<IContactService>();
        var controller = new ContactsController(mockDiscountService.Object);

        // Assuming your service returns a ResponseDto<List<Discount>> for GetAllAsync
        var responseDto = new ResponseDto<Contact> { Data = new Contact() }; // You may adjust this based on your actual implementation
        mockDiscountService.Setup(service => service.DeleteAsync(It.IsAny<int>())).ReturnsAsync(responseDto);

        // Act
        var result = await controller.Delete(It.IsAny<int>());

        // Assert
        Assert.IsAssignableFrom<ObjectResult>(result);
        
    }
    


    
}