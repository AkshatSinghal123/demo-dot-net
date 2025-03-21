using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Ensure necessary services are added
builder.Services.AddRazorPages();

// Improved Logging
builder.Logging.ClearProviders(); // Remove default logging providers
builder.Logging.AddConsole(); // Enable Console logging
builder.Logging.AddDebug(); // Enable Debug logging

var app = builder.Build();

try
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting(); // Ensure routing is set before authentication/authorization

    // Optional: Use authentication if your app has authentication configured
    // app.UseAuthentication();
    
    app.UseAuthorization();

    app.MapRazorPages();

    // Additional logging to confirm startup
    app.Logger.LogInformation("Application is starting...");

    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Application failed to start due to an exception.");
    throw; // Rethrow to let the framework handle it
}
