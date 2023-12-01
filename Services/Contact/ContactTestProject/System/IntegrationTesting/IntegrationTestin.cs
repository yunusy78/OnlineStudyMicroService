using System.Net;
using System.Text;
using Business.Abstract;
using ContactMicroServices.Controllers;
using EntityLayer.Concrete;
using Moq;
using Newtonsoft.Json;
using OnlineStudyShared;

namespace ContactTestProject.System.IntegrationTesting;

public class IntegrationTesting:IClassFixture<CustomWebApplicationFactory>
{
    
    private readonly HttpClient _client;

    public IntegrationTesting(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    
    [Fact]
    public async Task GetContacts_ShouldReturnSuccessStatusCode()
    {
        // Arrange - No specific arrangement needed for this test

        // Act
        var response = await _client.GetAsync("/api/contacts");

        // Assert
        response.EnsureSuccessStatusCode();
        // Additional assertions if needed
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // Check content, content type, or deserialize response content if applicable
    }


    [Fact]
    public async Task CreateOrder_ValidContact_ReturnsOkResult()
    {
        // Arrange
        var validContact = new Contact { Id = 1, FirstName = "A", LastName = "B", Email = "C", Message = "D", Subject = "E"};
        var content = new StringContent(JsonConvert.SerializeObject(validContact), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/contacts/CreateContact", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // Add more assertions based on your response structure
    }
}