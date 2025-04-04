using FrontEndLoginSignUp;
using FrontEndLoginSignUp.Components;
using System.Net.Http.Headers;
using Blazored.SessionStorage;
using Blazorise;
using CloudinaryDotNet;
// Import the required packages
//==============================


using dotenv.net;
using ClassLibraryModel;

// Set your Cloudinary credentials
//=================================

// Load environment variables from the .env file
DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));

var cloudinarySettings = builder.Configuration.GetSection("Cloudinary").Get<CloudinarySettings>();
Account account = new Account(
    cloudinarySettings.CloudName,
    cloudinarySettings.ApiKey,
    cloudinarySettings.ApiSecret);

builder.Services.AddSingleton(new Cloudinary(account));


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient("AuthApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001");
});
builder.Services.AddSingleton<UserService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddBlazorBootstrap();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
