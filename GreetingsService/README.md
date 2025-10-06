# GreetingsService

## Overview
GreetingsService is a console application that communicates with the MessageService REST API to retrieve and display greeting messages. This application demonstrates how to consume a REST API using HttpClient in .NET.

## Features
- Connects to MessageService REST API
- Displays timestamped greeting messages in the console
- Error handling for network connectivity issues
- Clean, user-friendly console output

## Technology Stack
- .NET 8.0
- System.Net.Http.Json for JSON serialization

## Prerequisites

The MessageService must be running before starting this application. See the [MessageService README](../MessageService/README.md) for instructions on how to start the service.

## Running the Application

### Using Visual Studio Code

1. **Start MessageService first:**
   - Open a terminal
   - Navigate to MessageService directory: `cd MessageService`
   - Run: `dotnet run`
   - Keep this terminal open

2. **Run GreetingsService:**
   - Open a new terminal
   - Navigate to GreetingsService directory: `cd GreetingsService`
   - Run: `dotnet run`
   - View the output in the console

### Using Visual Studio

1. **Start MessageService first:**
   - Open the `MigrationWorkshop.sln` solution
   - Right-click on `MessageService` project → Debug → Start New Instance
   - Wait for the service to start

2. **Run GreetingsService:**
   - Right-click on `GreetingsService` project → Debug → Start New Instance
   - View the output in the console window

### Using Command Line

1. **Start MessageService (in first terminal):**
   ```bash
   cd MessageService
   dotnet run
   ```

2. **Run GreetingsService (in second terminal):**
   ```bash
   cd GreetingsService
   dotnet run
   ```

## Expected Output

When the application runs successfully, you should see output similar to:

```
=== GreetingsService Console Application ===
Connecting to MessageService at: http://localhost:5000

Calling /api/message endpoint...

=== Response Received ===
Message: 2024-01-15 14:30:45 - Hello World
Timestamp: 2024-01-15 14:30:45

Press any key to exit...
```

## Error Handling

If MessageService is not running, you will see:

```
Error: Unable to connect to MessageService.
Details: [Connection error details]

Please ensure the MessageService is running at http://localhost:5000
```

## Configuration

The MessageService URL is configured as a constant in `Program.cs`:
```csharp
private const string MessageServiceUrl = "http://localhost:5000";
```

To change the service URL, modify this constant in the source code.

## Project Structure

```
GreetingsService/
├── Program.cs              # Main application entry point
├── GreetingsService.csproj # Project file
└── README.md              # This file
```

## Development

### Building the Project
```bash
dotnet build
```

### Running in Debug Mode
```bash
dotnet run --configuration Debug
```

### Publishing the Application
```bash
dotnet publish -c Release -o ./publish
```

## How It Works

1. The application creates an HttpClient instance configured to connect to the MessageService
2. It sends a GET request to the `/api/message` endpoint
3. The response is deserialized into a `MessageResponse` object
4. The message and timestamp are displayed in the console
5. The application waits for user input before exiting

## Code Documentation

The source code includes XML documentation comments for all public types and members, providing IntelliSense support in Visual Studio and VS Code.

## Troubleshooting

**Problem:** Connection refused error  
**Solution:** Ensure MessageService is running on http://localhost:5000

**Problem:** JSON deserialization error  
**Solution:** Ensure you're running the latest version of MessageService

**Problem:** Application exits immediately  
**Solution:** Check that you're running in a console window, not as a background process

## Dependencies

All dependencies are managed through NuGet and are automatically restored when building the project. The application uses built-in .NET libraries:
- System.Net.Http
- System.Net.Http.Json
