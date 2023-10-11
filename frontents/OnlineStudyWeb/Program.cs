using Business.Abstract;
using Business.Concrete;
using Business.Handler;
using Business.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineStudyShared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddHttpClient<IIdentityService, IdentityManager>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenManager>();
builder.Services.AddHttpClient<IImageStockService, ImageStockManager>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<ICatalogService, CatalogManager>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();



builder.Services.AddHttpContextAccessor();
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection(nameof(ClientSettings)));
builder.Services.AddScoped<ISharedIdentity, SharedIdentity>();
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddAccessTokenManagement();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<IUserService, UserManager>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration.GetSection(nameof(ServiceApiSettings)).Get<ServiceApiSettings>()!
        .IdentityBaseUri);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Auth/SignIn";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie.Name = "OnlineStudy";
});
builder.Services.AddControllersWithViews();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();