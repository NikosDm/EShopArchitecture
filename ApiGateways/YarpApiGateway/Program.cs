using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Defines how many times can we send a request to cluster. 
// `PermitLimit` defines the maximum times we can send requests. 
// If this is exceeded, then we have to wait for `opt.Window` time. 
builder.Services.AddRateLimiter(options => 
{
    options.AddFixedWindowLimiter("fixed", opt => 
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 5;
    });
});

var app = builder.Build();

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
