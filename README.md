# QR Code Generator - .NET 8 Minimal API

This project is a simple QR code generator implemented using .NET 8 Minimal API. It provides a single GET endpoint that returns a base64-encoded string representation of a QR code image, ensuring that the input is a valid alphanumeric string.

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or Visual Studio Code

## Setup

1. Create a new .NET 8 Web API project or use an existing one.

2. Copy the contents of the `Program.cs` file into your project's `Program.cs`.

3. Install the required NuGet packages by running the following commands in the project directory:

   ```
   dotnet add package QRCoder
   dotnet add package System.Drawing.Common
   ```

4. Ensure your `.csproj` file includes the following property to enable the use of System.Drawing.Common:

   ```xml
   <PropertyGroup>
     <TargetFramework>net8.0</TargetFramework>
     <ImplicitUsings>enable</ImplicitUsings>
     <Nullable>enable</Nullable>
     <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
   </PropertyGroup>
   ```

## Running the Application

1. Open a terminal in the project directory.

2. Run the following command:

   ```
   dotnet run
   ```

3. The application will start, typically on `https://localhost:5001` or `http://localhost:5000`.

## Usage

To generate a QR code, make a GET request to the `/qrcode/{referenceNumber}` endpoint, where `{referenceNumber}` is replaced with your actual reference number:

```
GET https://localhost:5001/qrcode/YourReferenceHere
```

Replace `YourReferenceHere` with the actual reference number you want to encode in the QR code.

Important: The reference number must be a non-empty alphanumeric string (containing only letters and numbers). Any other input will result in a bad request response.

The API will return a base64-encoded string representing the QR code image. You can use this string to display the QR code in an HTML `<img>` tag by prefixing it with `data:image/png;base64,`.

Example usage in HTML:
```html
<img src="data:image/png;base64,YOUR_BASE64_STRING_HERE" alt="QR Code" />
```

## Error Handling

The API will return a 400 Bad Request status code with an error message in the following cases:
- If the referenceNumber is missing or empty
- If the referenceNumber contains any characters other than letters and numbers

## Note

While this implementation includes basic input validation, in a production environment, you should consider adding more robust error handling, logging, and security measures as appropriate for your use case.