using Microsoft.Extensions.Configuration;
using Netopia.Logic;
using Netopia.Logic.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(c =>
{
    c.TimestampFormat = "[HH:mm:ss] ";
});

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddFile("app_{0:yyyy}-{0:MM}-{0:dd}.log", fileLoggerOpts =>
    {
        fileLoggerOpts.FormatLogFileName = fName =>
        {
            return String.Format(fName, DateTime.UtcNow);
        };
        fileLoggerOpts.UseUtcTimestamp = true;
    });
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<PaymentConfiguration>(builder.Configuration.GetSection("PaymentConfiguration"));

builder.Services.AddScoped<IPaymentProcessor, PaymentProcessor>();

var app = builder.Build();

app.Logger.LogInformation("Starting the App");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
