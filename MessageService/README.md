# MessageService

## Overview
MessageService is a REST API built with ASP.NET Core that provides a simple message endpoint. The service returns a "Hello World" message with the current date and time prepended to it.

## Features
- RESTful API endpoint at `/api/message`
- Returns timestamped greeting messages
- Swagger/OpenAPI documentation included
- CORS enabled for cross-origin requests

## Technology Stack
- .NET 8.0
- ASP.NET Core Minimal APIs
- Swagger/OpenAPI for API documentation

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

### Using Visual Studio Code

1. Open the terminal in VS Code
2. Navigate to the MessageService directory:
   ```bash
   cd MessageService
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. The service will start on `http://localhost:5000`
5. Access the Swagger UI at `http://localhost:5000/swagger`

### Using Visual Studio

1. Open the `MigrationWorkshop.sln` solution file in Visual Studio
2. Set `MessageService` as the startup project (right-click on the project → Set as Startup Project)
3. Press `F5` or click the "Start" button to run the application
4. The service will start and open the Swagger UI in your default browser

### Using Command Line

```bash
cd MessageService
dotnet run
```

## Testing the API

### Using a Web Browser
Navigate to:
```
http://localhost:5000/api/message
```

### Using curl
```bash
curl http://localhost:5000/api/message
```

### Using PowerShell
```powershell
Invoke-WebRequest -Uri http://localhost:5000/api/message | Select-Object -ExpandProperty Content
```

### Using Swagger UI
Navigate to `http://localhost:5000/swagger` to access the interactive API documentation.

## Configuration

The service configuration can be found in `appsettings.json`:

- **Urls**: The service listens on `http://localhost:5000` by default
- **Logging**: Configured for Information level logging

## Project Structure

```
MessageService/
├── Program.cs              # Main application entry point and API endpoints
├── appsettings.json        # Application configuration
├── appsettings.Development.json  # Development-specific configuration
├── MessageService.csproj   # Project file
└── README.md              # This file
```

## Development

### Building the Project
```bash
dotnet build
```

### Publishing the Application
```bash
dotnet publish -c Release -o ./publish
```

## Dependencies

All dependencies are managed through NuGet and are automatically restored when building the project:
- Microsoft.AspNetCore.OpenApi
- Swashbuckle.AspNetCore

## Notes

- The service runs on HTTP (not HTTPS) by default for easier local development and testing
- CORS is enabled to allow the GreetingsService console application to call the API
- The timestamp format is `yyyy-MM-dd HH:mm:ss`
