using System.Net;
using Moq;
using Newtonsoft.Json;
using OnlineStudyShared;

namespace ContactTestProject.System.IntegrationTesting;

public class ContactControllerIntegrationTesting : IDisposable
{
    private readonly HttpClient _client;
    private CustomWebApplicationFactory _factory;
    
    public ContactControllerIntegrationTesting()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }
    
    [Fact]
    public async Task Get_Always_ReturnsAllContacts()
    {
        // Arrange
        var mockContacts = new List<EntityLayer.Concrete.Contact>
        {
            new EntityLayer.Concrete.Contact { Id = 1, FirstName = "A", LastName = "B", Email = "C", Message = "D", Subject = "E" },
            new EntityLayer.Concrete.Contact { Id = 2, FirstName = "F", LastName = "G", Email = "H", Message = "I", Subject = "J" }
        };

        var responseDto = new ResponseDto<List<EntityLayer.Concrete.Contact>> { Data = mockContacts };

        _factory.MockContactService.Setup(r => r.GetAllAsync())
            .ReturnsAsync(responseDto);

        // Act
        var response = await _client.GetAsync("/api/contacts");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var data = JsonConvert.DeserializeObject<IEnumerable<EntityLayer.Concrete.Contact>>(await response.Content.ReadAsStringAsync());

        Assert.Collection(data,
            r =>
            {
                Assert.Equal("A", r.FirstName);
                Assert.Equal("B", r.LastName);
            },
            r =>
            {
                Assert.Equal("F", r.FirstName);
                Assert.Equal("G", r.LastName);
            }
        );
    }



    
    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}