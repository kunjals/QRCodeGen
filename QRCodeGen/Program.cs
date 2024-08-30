using QRCoder;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/qrcode", (string referenceNumber) =>
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
    Base64QRCode qrCode = new Base64QRCode(qrCodeData);
    string qrCodeImageBase64 = qrCode.GetGraphic(20);

    return Results.Ok(qrCodeImageBase64);
});

app.Run();