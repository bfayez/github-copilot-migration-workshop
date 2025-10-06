# MessageService

## Overview
MessageService is a REST API built with ASP.NET Web API 2 (.NET Framework 4.8.1) that provides a simple message endpoint. The service returns a "Hello World" message with the current date and time prepended to it.

## Features
- RESTful API endpoint at `/api/message`
- Returns timestamped greeting messages
- Swagger/OpenAPI documentation included
- CORS enabled for cross-origin requests

## Technology Stack
- .NET Framework 4.8.1
- ASP.NET Web API 2
- Swashbuckle for API documentation
- Newtonsoft.Json for JSON serialization

## API Endpoints

### GET /api/message
Returns a greeting message with the current timestamp.

**Response:**
```json
{
  "message": "2024-01-15 14:30:45 - Hello World",
  "timestamp": "2024-01-15T14:30:45.123Z"
}
```

**Status Codes:**
- `200 OK` - Success

## Running the Application

### Using Visual Studio

1. Open the `MigrationWorkshop.sln` solution file in Visual Studio 2022
2. Set `MessageService` as the startup project (right-click on the project → Set as Startup Project)
3. Press `F5` or click the "Start" button to run the application
4. The service will start in IIS Express and open in your default browser
5. Access the Swagger UI at `http://localhost:5000/swagger`

### Configuration

The service configuration can be found in `Web.config`:

- **Port**: The service listens on `http://localhost:5000` by default (configured in project properties)
- **Logging**: Standard ASP.NET logging configuration

## Testing the API

### Using Swagger UI
1. Run the MessageService project
2. Navigate to `http://localhost:5000/swagger`
3. Find the `/api/message` endpoint
4. Click "Try it out" then "Execute"

### Using a Web Browser
Navigate to:
```
http://localhost:5000/api/message
```

### Using PowerShell
```powershell
Invoke-RestMethod -Uri http://localhost:5000/api/message
```

### Using curl (if installed on Windows)
```bash
curl http://localhost:5000/api/message
```

## Project Structure

```
MessageService/
├── App_Start/
│   ├── WebApiConfig.cs         # Web API routing and configuration
│   └── SwaggerConfig.cs        # Swagger/OpenAPI configuration
├── Controllers/
│   └── MessageController.cs    # API controller with message endpoint
├── Models/
│   └── MessageResponse.cs      # Response data model
├── Properties/
│   └── AssemblyInfo.cs         # Assembly metadata
├── Global.asax                 # Application entry point
├── Global.asax.cs              # Application startup logic
├── Web.config                  # Application configuration
├── Web.Debug.config            # Debug configuration transform
├── Web.Release.config          # Release configuration transform
├── MessageService.csproj       # Project file
├── packages.config             # NuGet package references
└── README.md                   # This file
```

## Development

### Building the Project
1. Open solution in Visual Studio
2. Build → Build MessageService (or press `Ctrl+Shift+B`)

### Publishing the Application
1. Right-click on MessageService project
2. Select "Publish"
3. Choose your publish target:
   - IIS
   - Azure App Service
   - File System
   - FTP, etc.
4. Configure settings and click "Publish"

## Dependencies

All dependencies are managed through NuGet (packages.config):
- Microsoft.AspNet.WebApi (5.2.9)
- Microsoft.AspNet.WebApi.Cors (5.2.9)
- Newtonsoft.Json (13.0.3)
- Swashbuckle (5.6.0)

## Adding New Endpoints

To add a new endpoint, edit `Controllers/MessageController.cs`:

```csharp
[HttpGet]
[Route("api/newEndpoint")]
public IHttpActionResult GetNewEndpoint()
{
    return Ok(new { data = "your data here" });
}
```

## Notes

- The service runs on HTTP (not HTTPS) by default for easier local development
- CORS is enabled to allow the GreetingsConsole console application to call the API
- The timestamp format is `yyyy-MM-dd HH:mm:ss`
- Requires .NET Framework 4.8.1 and Windows OS
- Best developed using Visual Studio 2022 or 2019
