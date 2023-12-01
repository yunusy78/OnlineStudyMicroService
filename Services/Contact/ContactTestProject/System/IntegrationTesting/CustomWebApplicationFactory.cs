using Business.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Moq;

namespace ContactTestProject.System.IntegrationTesting;

public class CustomWebApplicationFactory:WebApplicationFactory<Program>
{
    
      public Mock<IContactService> MockContactService { get; }
      
      public CustomWebApplicationFactory()
      {
          MockContactService = new Mock<IContactService>();
      }
      
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(services =>
        {
            services.AddScoped(x => MockContactService.Object);
        });
    }
}