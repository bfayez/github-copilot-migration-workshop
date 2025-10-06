# GreetingsService

## Overview
GreetingsService is a console application built with .NET Framework 4.8.1 that communicates with the MessageService REST API to retrieve and display greeting messages. This application demonstrates how to consume a REST API using HttpClient in .NET Framework.

## Features
- Connects to MessageService REST API
- Displays timestamped greeting messages in the console
- Error handling for network connectivity issues
- Clean, user-friendly console output
- Async/await pattern for HTTP requests

## Technology Stack
- .NET Framework 4.8.1
- HttpClient for HTTP communication
- Newtonsoft.Json for JSON deserialization

## Prerequisites

The MessageService must be running before starting this application. See the [MessageService README](../MessageService/README.md) for instructions on how to start the service.

## Running the Application

### Using Visual Studio

**Method 1: Start New Instance**
1. Ensure MessageService is already running
2. Open the `MigrationWorkshop.sln` solution
3. Right-click on `GreetingsService` project in Solution Explorer
4. Select "Debug" → "Start New Instance"
5. View the output in the console window

**Method 2: Multiple Startup Projects**
1. Right-click on the Solution in Solution Explorer
2. Select "Properties"
3. Choose "Multiple startup projects"
4. Set both MessageService and GreetingsService to "Start"
5. Click OK
6. Press `F5` to start both projects

### Using Command Line

1. **Build the project:**
   ```cmd
   msbuild GreetingsService.csproj /p:Configuration=Release
   ```

2. **Run the executable:**
   ```cmd
   bin\Release\GreetingsService.exe
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

### MessageService Not Running

If MessageService is not running, you will see:

```
Error: Unable to connect to MessageService.
Details: [Connection error details]

Please ensure the MessageService is running at http://localhost:5000
```

### Other Errors

The application handles various error scenarios:
- Network connectivity issues
- Invalid responses from the service
- JSON deserialization errors

## Configuration

The MessageService URL is configured as a constant in `Program.cs`:
```csharp
private const string MessageServiceUrl = "http://localhost:5000";
```

To change the service URL, modify this constant and rebuild the application.

## Project Structure

```
GreetingsService/
├── Models/
│   └── MessageResponse.cs      # Data model for API response
├── Properties/
│   └── AssemblyInfo.cs         # Assembly metadata
├── Program.cs                  # Main application entry point
├── App.config                  # Application configuration
├── GreetingsService.csproj     # Project file
├── packages.config             # NuGet package references
└── README.md                   # This file
```

## Development

### Building the Project
In Visual Studio:
1. Build → Build GreetingsService
2. Or press `Ctrl+Shift+B`

### Publishing the Application
1. Right-click on GreetingsService project
2. Select "Publish"
3. Choose "Folder" as the target
4. Configure output folder
5. Click "Publish"
6. The executable and all dependencies will be in the publish folder

## How It Works

1. The application creates an HttpClient instance
2. It sends a GET request to `http://localhost:5000/api/message`
3. The response is read as a string
4. JSON is deserialized into a `MessageResponse` object using Newtonsoft.Json
5. The message and timestamp are displayed in the console
6. The application waits for user input before exiting

## Code Documentation

The source code includes XML documentation comments for all public types and members, providing IntelliSense support in Visual Studio.

## Dependencies

All dependencies are managed through NuGet (packages.config):
- Newtonsoft.Json (13.0.3)

## Troubleshooting

**Problem:** Connection refused error  
**Solution:** Ensure MessageService is running on http://localhost:5000

**Problem:** JSON deserialization error  
**Solution:** Ensure you're running the latest version of MessageService with matching API contract

**Problem:** Application exits immediately  
**Solution:** Run from Visual Studio or check console output for error messages

**Problem:** "Press any key" doesn't work  
**Solution:** This is normal behavior when running from some environments. The application will exit after displaying results.

## Notes

- Requires .NET Framework 4.8.1 and Windows OS
- Uses async/await pattern for non-blocking HTTP requests
- Requires MessageService to be running before execution
- Best developed using Visual Studio 2022 or 2019
