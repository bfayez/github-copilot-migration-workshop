/// <summary>
/// MessageService Web API
/// This service provides a simple message endpoint that returns a greeting with the current timestamp.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MessageService API",
        Version = "v1",
        Description = "A simple REST API that returns greeting messages with timestamps"
    });
});

// Configure CORS to allow the console application to call the API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Disable HTTPS redirection for easier local testing
// app.UseHttpsRedirection();

/// <summary>
/// GET endpoint that returns a "Hello World" message with the current date and time prepended.
/// </summary>
/// <returns>A formatted message string with timestamp</returns>
app.MapGet("/api/message", () =>
{
    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    var message = $"{timestamp} - Hello World";
    
    return Results.Ok(new MessageResponse
    {
        Message = message,
        Timestamp = DateTime.Now
    });
})
.WithName("GetMessage")
.WithOpenApi()
.Produces<MessageResponse>(200)
.WithTags("Message");

app.Run();

/// <summary>
/// Response model for the message endpoint
/// </summary>
/// <param name="Message">The formatted message with timestamp</param>
/// <param name="Timestamp">The timestamp when the message was generated</param>
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
