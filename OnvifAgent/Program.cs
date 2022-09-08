using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapPost("/{IPAddress}", async (e) =>
{
    try
    {
        string path = "/onvif/device_service";
        if (e.Request.Query["path"] != null) path = e.Request.Query["path"];
        WebClient wc = new();
        wc.Credentials = new NetworkCredential(e.Request.Query["user"], e.Request.Query["pass"]);
        string result = await wc.UploadStringTaskAsync("http://" + e.Request.RouteValues["IPAddress"] + path, await new StreamReader(e.Request.Body).ReadToEndAsync());
        wc.Dispose();
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
