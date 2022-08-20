using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

WebClient wc = new();

app.MapPost("/{IPAddress}", async (e) =>
{
    try
    {
        wc.Credentials = new NetworkCredential(e.Request.Query["user"], e.Request.Query["pass"]);
        string result = await wc.UploadStringTaskAsync("http://" + e.Request.RouteValues["IPAddress"] + "/onvif/device_service", await new StreamReader(e.Request.Body).ReadToEndAsync());
        await e.Response.WriteAsync(result);
    }
    catch (Exception ex)
    {
        await e.Response.WriteAsync(ex.Message);
    }
});

app.MapGet("/", () =>
{
    return "Send POST with ONVIF SOAP to /IPAddress?user=username&pass=password";
});

app.Run();