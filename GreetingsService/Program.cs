/// <summary>
/// GreetingsService Console Application
/// This application calls the MessageService REST API and displays the greeting message to the console.
/// </summary>
using System.Net.Http.Json;
using System.Text.Json;

namespace GreetingsService;

/// <summary>
/// Main program class for the GreetingsService console application
/// </summary>
class Program
{
    /// <summary>
    /// The base URL for the MessageService API
    /// </summary>
    private const string MessageServiceUrl = "http://localhost:5000";
    
    /// <summary>
    /// Main entry point for the console application
    /// </summary>
    /// <param name="args">Command line arguments</param>
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== GreetingsService Console Application ===");
        Console.WriteLine($"Connecting to MessageService at: {MessageServiceUrl}");
        Console.WriteLine();
        
        try
        {
            // Create HttpClient to call the REST API
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(MessageServiceUrl)
            };
            
            // Call the message endpoint
            Console.WriteLine("Calling /api/message endpoint...");
            var response = await httpClient.GetFromJsonAsync<MessageResponse>("/api/message");
            
            if (response != null)
            {
                Console.WriteLine();
                Console.WriteLine("=== Response Received ===");
                Console.WriteLine($"Message: {response.Message}");
                Console.WriteLine($"Timestamp: {response.Timestamp:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Error: Received empty response from the service.");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine();
            Console.WriteLine($"Error: Unable to connect to MessageService.");
            Console.WriteLine($"Details: {ex.Message}");
            Console.WriteLine();
            Console.WriteLine("Please ensure the MessageService is running at " + MessageServiceUrl);
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"Unexpected error: {ex.Message}");
            Environment.Exit(1);
        }
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

/// <summary>
/// Response model matching the MessageService API response
/// </summary>
record MessageResponse
{
    /// <summary>
    /// The formatted message string
    /// </summary>
    public string Message { get; init; } = string.Empty;
    
    /// <summary>
    /// The timestamp when the message was generated
    /// </summary>
    public DateTime Timestamp { get; init; }
}
