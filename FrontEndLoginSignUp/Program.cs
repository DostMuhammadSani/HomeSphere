using FrontEndLoginSignUp;
using FrontEndLoginSignUp.Components;
using System.Net.Http.Headers;
using Blazored.SessionStorage;
using Blazorise;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient("AuthApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001");
});
builder.Services.AddSingleton<UserService>();
builder.Services.AddBlazoredSessionStorage();

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
