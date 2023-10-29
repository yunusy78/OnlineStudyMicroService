using Business.Abstract;
using Business.Concrete;
using Business.Extensions;
using Business.Handler;
using Business.Helpers;
using Business.Models;
using Business.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineStudyShared.Services;
using StripePaymentMicroService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<IIdentityService, IdentityManager>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenManager>();


builder.Services.AddHttpContextAccessor();
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection(nameof(ClientSettings)));
builder.Services.AddScoped<ISharedIdentity, SharedIdentity>();
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.Configure<StripeService>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddSingleton<PhotoStockHelper>();
builder.Services.AddAccessTokenManagement();
builder.Services.AddScoped<ClientCredentialTokenHandler>();

builder.Services.AddHttpClientServices(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Auth/SignIn";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie.Name = "OnlineStudy";
});

builder.Services.AddControllersWithViews().AddFluentValidation(options =>
    options.RegisterValidatorsFromAssemblyContaining<CourseCreateInputValidator>());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name : "areas",
        pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();