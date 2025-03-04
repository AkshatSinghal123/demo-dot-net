using System; // Add this line
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages services
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure error handling and security policies
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Ensure the app listens on the dynamically assigned Azure port
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; // Default to 8080 for Azure
app.Urls.Add($"http://*:{port}");

// Middleware setup
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
