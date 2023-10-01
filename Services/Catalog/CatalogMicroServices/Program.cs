using System.Configuration;
using CatalogMicroServices.Business.Abstract;
using CatalogMicroServices.Business.Concrete;
using CatalogMicroServices.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt=> {
    opt.Filters.Add(new AuthorizeFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
var databaseSettingsSection = builder.Configuration.GetSection("DatabaseSettings");
if (databaseSettingsSection != null)
{
    builder.Services.Configure<DatabaseSettings>(databaseSettingsSection);
    builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    {
        return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    });
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICourseService, CourseManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();