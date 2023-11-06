using System.IdentityModel.Tokens.Jwt;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineStudyShared.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<ICommentDal, CommentEfRepository>();
builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactDal, ContactEfRepository>();
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<ITestimonialDal, TestimonialEfRepository>();
builder.Services.AddScoped<IInstructorService, InstructorManager>();
builder.Services.AddScoped<IInstructorDal, InstructorEfRepository>();
builder.Services.AddScoped<ISharedIdentity, SharedIdentity>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme(\"bearer {token}\"))",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseMySQL(connectionString!));

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_contact";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddControllers(opt=> {
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();