using System.Net;
using System.Net.Http.Json;
using System.Text;
using CatalogMicroServices.Dtos.Category;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Xunit;
using JsonConvert = MongoDB.Bson.IO.JsonConvert;

namespace CatalogTestProject.System.Controller;

public class CategoryControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CategoryControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateCategory_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var client = _factory.CreateClient();

        var createCategoryDto = new CreateCategoryDto
        {
            CategoryName = "Integration Test Category"
        };

        // Convert the test category to JSON
        

        // Act
        var response = await client.PostAsJsonAsync("http://localhost:5011/api/category", createCategoryDto);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        // Optionally, you can deserialize the response content for further assertions
        // var createdCategory = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
        // Assert.NotNull(createdCategory);
    }
}