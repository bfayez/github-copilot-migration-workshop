# Migration Workshop - Demo Application

This repository contains a demonstration application consisting of two .NET projects that work together to showcase REST API communication.

## Overview

The solution includes two applications:

1. **MessageService** - A REST API service that returns timestamped greeting messages
2. **GreetingsService** - A console application that calls the MessageService and displays the results

## Architecture

```
┌─────────────────────┐          HTTP GET          ┌──────────────────┐
│  GreetingsService   │ ───────────────────────>  │  MessageService  │
│  (Console App)      │                            │  (REST API)      │
│                     │ <─────────────────────────  │                  │
└─────────────────────┘    JSON Response          └──────────────────┘
                           (Timestamped Message)
```

## Technology Stack

- **.NET 8.0** - Modern cross-platform framework (equivalent to .NET Framework 4.8.1 functionality)
- **ASP.NET Core** - For the REST API (MessageService)
- **Minimal APIs** - Lightweight API endpoints
- **Swagger/OpenAPI** - API documentation
- **HttpClient** - For REST API consumption

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- **Visual Studio 2022** (Windows) or **Visual Studio Code** (cross-platform)
- For Visual Studio Code: C# Dev Kit extension

## Quick Start

### Option 1: Using Visual Studio Code

1. **Clone the repository:**
   ```bash
   git clone https://github.com/bfayez/github-copilot-migration-workshop.git
   cd github-copilot-migration-workshop
   ```

2. **Open in VS Code:**
   ```bash
   code .
   ```

3. **Start MessageService (Terminal 1):**
   ```bash
   cd MessageService
   dotnet run
   ```
   Wait for the message: `Now listening on: http://localhost:5000`

4. **Run GreetingsService (Terminal 2):**
   ```bash
   cd GreetingsService
   dotnet run
   ```

5. **View the output** - You should see a greeting message with timestamp in the console

### Option 2: Using Visual Studio

1. **Open the solution:**
   - Launch Visual Studio 2022
   - Open `MigrationWorkshop.sln`

2. **Start MessageService:**
   - Right-click on `MessageService` project in Solution Explorer
   - Select "Debug" → "Start New Instance"
   - A console window will open showing the service is running

3. **Run GreetingsService:**
   - Right-click on `GreetingsService` project in Solution Explorer
   - Select "Debug" → "Start New Instance"
   - A console window will open displaying the greeting message

### Option 3: Using Command Line

1. **Build the solution:**
   ```bash
   dotnet build MigrationWorkshop.sln
   ```

2. **Run MessageService in background:**
   ```bash
   cd MessageService
   dotnet run &
   cd ..
   ```

3. **Run GreetingsService:**
   ```bash
   cd GreetingsService
   dotnet run
   ```

## Project Structure

```
github-copilot-migration-workshop/
├── MigrationWorkshop.sln          # Solution file
├── README.md                       # This file
├── MessageService/                 # REST API Project
│   ├── Program.cs                  # API endpoints and configuration
│   ├── MessageService.csproj       # Project file
│   ├── appsettings.json           # Configuration
│   └── README.md                   # Service-specific documentation
└── GreetingsService/              # Console Application Project
    ├── Program.cs                  # Main application logic
    ├── GreetingsService.csproj    # Project file
    └── README.md                   # Application-specific documentation
```

## Features

### MessageService API
- **Endpoint:** `GET /api/message`
- **Returns:** JSON object with timestamped greeting
- **Swagger UI:** Available at `http://localhost:5000/swagger`
- **Port:** 5000 (HTTP)

### GreetingsService Console App
- Connects to MessageService
- Displays formatted greeting message
- Error handling for connection issues
- User-friendly console interface

## Building the Solution

### Build All Projects
```bash
dotnet build MigrationWorkshop.sln
```

### Build Individual Projects
```bash
# Build MessageService
dotnet build MessageService/MessageService.csproj

# Build GreetingsService
dotnet build GreetingsService/GreetingsService.csproj
```

### Clean Build
```bash
dotnet clean MigrationWorkshop.sln
dotnet build MigrationWorkshop.sln
```

## Testing the API

### Using Swagger UI
Navigate to `http://localhost:5000/swagger` when MessageService is running to interact with the API through the browser.

### Using curl
```bash
curl http://localhost:5000/api/message
```

### Using PowerShell
```powershell
Invoke-RestMethod -Uri http://localhost:5000/api/message
```

### Using Browser
Simply navigate to `http://localhost:5000/api/message`

## Naming Conventions

This project follows .NET naming conventions:

- **Solution:** PascalCase - `MigrationWorkshop.sln`
- **Projects:** PascalCase - `MessageService`, `GreetingsService`
- **Files:** PascalCase - `Program.cs`, `README.md`
- **Classes:** PascalCase - `MessageResponse`
- **Methods:** PascalCase - `Main`, `GetMessage`
- **Variables:** camelCase - `httpClient`, `timestamp`
- **Constants:** PascalCase - `MessageServiceUrl`

## Documentation

Each project includes:
- **XML documentation comments** in source code for IntelliSense support
- **Project-specific README** files with detailed instructions
- **Inline comments** where necessary for clarity

See individual project READMEs for detailed information:
- [MessageService Documentation](MessageService/README.md)
- [GreetingsService Documentation](GreetingsService/README.md)

## Troubleshooting

### MessageService won't start
- Check if port 5000 is already in use
- Verify .NET 8.0 SDK is installed: `dotnet --version`

### GreetingsService cannot connect
- Ensure MessageService is running first
- Check that MessageService is listening on http://localhost:5000
- Verify no firewall is blocking the connection

### Build errors
- Restore NuGet packages: `dotnet restore MigrationWorkshop.sln`
- Clean and rebuild: `dotnet clean && dotnet build`

## Development

### Adding New Endpoints (MessageService)
Edit `MessageService/Program.cs` and add new endpoints using the Minimal API pattern:
```csharp
app.MapGet("/api/newendpoint", () => { /* implementation */ });
```

### Modifying the Console App (GreetingsService)
Edit `GreetingsService/Program.cs` to change the behavior or add new features.

## Publishing

### Publish MessageService
```bash
cd MessageService
dotnet publish -c Release -o ./publish
```

### Publish GreetingsService
```bash
cd GreetingsService
dotnet publish -c Release -o ./publish
```

## Notes

- This solution uses **.NET 8.0** instead of .NET Framework 4.8.1 for cross-platform compatibility
- The functionality and architecture remain equivalent to what would be implemented in .NET Framework
- HTTP (not HTTPS) is used for simplified local development
- CORS is enabled in MessageService for cross-origin requests

## License

This is a demonstration project for the GitHub Copilot Migration Workshop.

## Contributing

This is a workshop demonstration project. For questions or issues, please open an issue in the GitHub repository.
