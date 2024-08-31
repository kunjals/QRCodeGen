using QRCoder;
using Serilog;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSerilogRequestLogging();

    app.MapGet("/qrcode/{referenceNumber}", (string referenceNumber) =>
    {
        if (string.IsNullOrEmpty(referenceNumber))
        {
            return Results.BadRequest("Reference number is required.");
        }

        if (!Regex.IsMatch(referenceNumber, "^[a-zA-Z0-9]+$"))
        {
            return Results.BadRequest("Reference number must be alphanumeric.");
        }

        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(referenceNumber, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeImageBase64 = qrCode.GetGraphic(20);

        return Results.Ok(qrCodeImageBase64);
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}