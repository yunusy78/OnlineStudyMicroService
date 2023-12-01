using Entity.Concrete;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace IntegrationTestProject;

public class UnitTest1
{
    public class DiscountsControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public DiscountsControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkStatus()
        {
            // Arrange - Burada gerekirse test için veri hazırlayabilirsiniz.

            // Act
            var response = await _client.GetAsync("/api/discounts");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var content = await response.Content.ReadAsStringAsync();
            var discounts = JsonConvert.DeserializeObject<Discount[]>(content); // DiscountDto'yu uygun şekilde değiştirin
            Assert.NotNull(discounts);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnOkStatus()
        {
            // Arrange - Gerekirse test için veri hazırlayabilir ve bir ID alabilirsiniz.

            // Act
            var response = await _client.GetAsync("/api/discounts/1"); // Değiştirilecek: 1 yerine mevcut bir ID

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var content = await response.Content.ReadAsStringAsync();
            var discount = JsonConvert.DeserializeObject<Discount>(content); // DiscountDto'yu uygun şekilde değiştirin
            Assert.NotNull(discount);
        }

        // Diğer test metodları için aynı şablonu kullanabilirsiniz.
    }
}