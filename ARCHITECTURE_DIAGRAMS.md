# Azure Architecture Diagrams

This document provides detailed architecture diagrams for the modernization approaches discussed in the Azure Modernization Assessment.

---

## Current (Legacy) Architecture

```
┌─────────────────────────────────────────────────────────┐
│           Windows Server / Local Machine                │
│                                                          │
│  ┌────────────────────┐        ┌─────────────────────┐ │
│  │ Windows Task       │        │  IIS / IIS Express  │ │
│  │ Scheduler          │        │                     │ │
│  │                    │        │  ┌───────────────┐  │ │
│  │ Triggers every     │        │  │ MessageService│  │ │
│  │ minute:            │        │  │               │  │ │
│  │                    │        │  │ ASP.NET       │  │ │
│  │ ┌───────────────┐  │  HTTP  │  │ Web API 2     │  │ │
│  │ │ Greetings     │  │───────▶│  │               │  │ │
│  │ │ Console.exe   │  │  GET   │  │ GET /api/     │  │ │
│  │ │               │  │◀───────│  │     message   │  │ │
│  │ │ .NET Fwk 4.8.1│  │        │  │               │  │ │
│  │ └───────────────┘  │        │  └───────────────┘  │ │
│  └────────────────────┘        └─────────────────────┘ │
│                                                          │
│  Technology:                                             │
│  - .NET Framework 4.8.1 (Windows-only)                  │
│  - Windows Task Scheduler                               │
│  - IIS/IIS Express                                      │
│                                                          │
│  Limitations:                                            │
│  - Platform locked to Windows                           │
│  - Manual scaling                                       │
│  - Infrastructure management required                   │
│  - No cloud-native features                             │
└─────────────────────────────────────────────────────────┘
```

---

## Recommended Architecture: Azure Functions + App Service

```
┌─────────────────────────────────────────────────────────────────────┐
│                         Azure Cloud Platform                        │
│                                                                     │
│  ┌────────────────────────────────────────────────────────────┐   │
│  │              Azure Function App (Consumption Plan)         │   │
│  │                                                            │   │
│  │  ┌──────────────────────────────────────────────────┐     │   │
│  │  │  Timer Trigger Function                          │     │   │
│  │  │                                                   │     │   │
│  │  │  Trigger: 0 * * * * * (Every Minute)            │     │   │
│  │  │  Runtime: .NET 8 Isolated                        │     │   │
│  │  │                                                   │     │   │
│  │  │  [TimerTrigger("0 * * * * *")]                  │     │   │
│  │  │  public async Task Run(TimerInfo timer)          │     │   │
│  │  │  {                                                │     │   │
│  │  │      // Call API and process response            │     │   │
│  │  │  }                                                │     │   │
│  │  └──────────────────┬───────────────────────────────┘     │   │
│  └─────────────────────┼─────────────────────────────────────┘   │
│                        │                                          │
│                        │ HTTPS GET                                │
│                        │ /api/message                             │
│                        │                                          │
│  ┌─────────────────────▼─────────────────────────────────────┐   │
│  │           Azure App Service (B1/S1 Tier)                  │   │
│  │                                                            │   │
│  │  ┌──────────────────────────────────────────────────┐     │   │
│  │  │  REST API                                        │     │   │
│  │  │                                                   │     │   │
│  │  │  ASP.NET Core Web API (.NET 8)                  │     │   │
│  │  │                                                   │     │   │
│  │  │  Endpoints:                                       │     │   │
│  │  │  GET /api/message                                │     │   │
│  │  │    Returns: {                                     │     │   │
│  │  │      "message": "timestamp - Hello World",       │     │   │
│  │  │      "timestamp": "2025-11-05T10:30:00Z"        │     │   │
│  │  │    }                                              │     │   │
│  │  │                                                   │     │   │
│  │  │  Features:                                        │     │   │
│  │  │  - Auto-scaling                                   │     │   │
│  │  │  - Built-in SSL                                   │     │   │
│  │  │  - Custom domains                                 │     │   │
│  │  │  - Deployment slots                               │     │   │
│  │  └──────────────────────────────────────────────────┘     │   │
│  └────────────────────────────────────────────────────────────┘   │
│                        │                                          │
│                        │ Telemetry                                │
│                        │                                          │
│  ┌─────────────────────▼─────────────────────────────────────┐   │
│  │            Application Insights                           │   │
│  │                                                            │   │
│  │  - Request tracking                                        │   │
│  │  - Performance monitoring                                  │   │
│  │  - Error tracking                                          │   │
│  │  - Custom metrics                                          │   │
│  │  - Distributed tracing                                     │   │
│  │  - Alerts and dashboards                                   │   │
│  │                                                            │   │
│  │  Monitors:                                                 │   │
│  │  - Function execution count (~1,440/day)                  │   │
│  │  - API response times                                      │   │
│  │  - Error rates                                             │   │
│  │  - Resource utilization                                    │   │
│  └────────────────────────────────────────────────────────────┘   │
│                                                                     │
│  ┌────────────────────────────────────────────────────────────┐   │
│  │            Azure Key Vault (Optional)                      │   │
│  │                                                            │   │
│  │  - API keys                                                │   │
│  │  - Connection strings                                      │   │
│  │  - Certificates                                            │   │
│  │  - Secrets management                                      │   │
│  └────────────────────────────────────────────────────────────┘   │
│                                                                     │
└─────────────────────────────────────────────────────────────────────┘

Key Benefits:
✅ Serverless scheduling (no infrastructure to manage)
✅ Automatic scaling for API
✅ Cost-effective ($75-90/month production)
✅ Built-in monitoring and logging
✅ Easy deployment and updates
✅ Cross-platform (.NET 8)
```

---

## Alternative 1: Azure Container Apps

```
┌──────────────────────────────────────────────────────────────────────┐
│                      Azure Container Apps                            │
│                                                                      │
│  ┌───────────────────────────────────────────────────────────────┐  │
│  │              Container App: message-api                       │  │
│  │                                                               │  │
│  │  ┌─────────────────────────────────────────────────────┐     │  │
│  │  │  Docker Container                                   │     │  │
│  │  │                                                      │     │  │
│  │  │  FROM mcr.microsoft.com/dotnet/aspnet:8.0          │     │  │
│  │  │                                                      │     │  │
│  │  │  REST API (.NET 8)                                  │     │  │
│  │  │  - HTTP Ingress enabled                             │     │  │
│  │  │  - HTTPS traffic                                    │     │  │
│  │  │  - GET /api/message                                 │     │  │
│  │  │                                                      │     │  │
│  │  │  Scaling:                                           │     │  │
│  │  │  - Min: 1 replica                                   │     │  │
│  │  │  - Max: 10 replicas                                 │     │  │
│  │  │  - Scale rules: HTTP request count                  │     │  │
│  │  └─────────────────────────────────────────────────────┘     │  │
│  └──────────────────────────────┬────────────────────────────────┘  │
│                                 │                                   │
│                                 │ Internal                          │
│                                 │ HTTP Call                         │
│                                 │                                   │
│  ┌──────────────────────────────▼───────────────────────────────┐  │
│  │         Container App: greetings-scheduler                   │  │
│  │                                                               │  │
│  │  ┌─────────────────────────────────────────────────────┐     │  │
│  │  │  Docker Container                                   │     │  │
│  │  │                                                      │     │  │
│  │  │  FROM mcr.microsoft.com/dotnet/aspnet:8.0          │     │  │
│  │  │                                                      │     │  │
│  │  │  Console App (.NET 8)                               │     │  │
│  │  │                                                      │     │  │
│  │  │  KEDA Scaling:                                      │     │  │
│  │  │  - Cron schedule: */1 * * * * (every minute)       │     │  │
│  │  │  - Min replicas: 0                                  │     │  │
│  │  │  - Max replicas: 1                                  │     │  │
│  │  │                                                      │     │  │
│  │  │  Environment Variables:                             │     │  │
│  │  │  - API_URL=https://message-api.azurecontainerapps  │     │  │
│  │  └─────────────────────────────────────────────────────┘     │  │
│  └──────────────────────────────────────────────────────────────┘  │
│                                 │                                   │
│                                 │ Logs & Metrics                    │
│                                 ▼                                   │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │              Log Analytics Workspace                         │  │
│  │              + Application Insights                          │  │
│  └──────────────────────────────────────────────────────────────┘  │
│                                                                      │
└──────────────────────────────────────────────────────────────────────┘

Use Case:
- Microservices architecture
- Need container control
- Future expansion planned
- Familiar with Docker/Kubernetes

Cost: ~$50-150/month
Complexity: Medium
```

---

## Alternative 2: Full Azure Functions (Serverless)

```
┌──────────────────────────────────────────────────────────────────────┐
│                  Azure Functions (Consumption Plan)                  │
│                                                                      │
│  ┌───────────────────────────────────────────────────────────────┐  │
│  │              Function: MessageApi                             │  │
│  │                                                               │  │
│  │  Trigger: HTTP                                                │  │
│  │  Route: /api/message                                          │  │
│  │  Runtime: .NET 8 Isolated                                     │  │
│  │                                                               │  │
│  │  [Function("GetMessage")]                                     │  │
│  │  public HttpResponseData Run(                                 │  │
│  │      [HttpTrigger(AuthorizationLevel.Anonymous,              │  │
│  │                   "get", Route = "message")]                  │  │
│  │      HttpRequestData req)                                     │  │
│  │  {                                                            │  │
│  │      var response = req.CreateResponse();                     │  │
│  │      response.WriteAsJsonAsync(new {                          │  │
│  │          Message = $"{DateTime.Now} - Hello World",          │  │
│  │          Timestamp = DateTime.Now                             │  │
│  │      });                                                      │  │
│  │      return response;                                         │  │
│  │  }                                                            │  │
│  │                                                               │  │
│  │  Scaling: Automatic (0-200 instances)                        │  │
│  │  Cold Start: ~1-2 seconds                                     │  │
│  └──────────────────────────────┬────────────────────────────────┘  │
│                                 │                                   │
│                                 │ Internal                          │
│                                 │ HTTP Call                         │
│                                 │                                   │
│  ┌──────────────────────────────▼───────────────────────────────┐  │
│  │          Function: GreetingsScheduler                        │  │
│  │                                                               │  │
│  │  Trigger: Timer                                               │  │
│  │  Schedule: 0 * * * * * (Every minute)                        │  │
│  │  Runtime: .NET 8 Isolated                                     │  │
│  │                                                               │  │
│  │  [Function("GreetingsScheduler")]                            │  │
│  │  public async Task Run(                                       │  │
│  │      [TimerTrigger("0 * * * * *")] TimerInfo timer,          │  │
│  │      FunctionContext context)                                 │  │
│  │  {                                                            │  │
│  │      var client = new HttpClient();                           │  │
│  │      var response = await client.GetAsync(apiUrl);           │  │
│  │      // Process response                                      │  │
│  │  }                                                            │  │
│  │                                                               │  │
│  │  Executions: ~43,200/month (1/minute)                        │  │
│  └──────────────────────────────────────────────────────────────┘  │
│                                                                      │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │              Application Insights                            │  │
│  │              - Unified telemetry for both functions          │  │
│  └──────────────────────────────────────────────────────────────┘  │
│                                                                      │
└──────────────────────────────────────────────────────────────────────┘

Use Case:
- Fully serverless
- Low to moderate API traffic
- Maximum cost optimization
- Simple deployment

Cost: ~$20-60/month (mostly API traffic)
Complexity: Low-Medium

Note: HTTP Function has cold starts (~1-2 seconds for first request)
Consider Premium plan if cold starts are problematic
```

---

## Alternative 3: Azure Kubernetes Service (AKS)

```
┌──────────────────────────────────────────────────────────────────────┐
│              Azure Kubernetes Service (AKS Cluster)                  │
│                                                                      │
│  ┌────────────────────────────────────────────────────────────────┐ │
│  │                         Namespace: production                  │ │
│  │                                                                │ │
│  │  ┌─────────────────────────────────────────────────────────┐  │ │
│  │  │              Deployment: message-api                    │  │ │
│  │  │                                                          │  │ │
│  │  │  ┌────────────┐  ┌────────────┐  ┌────────────┐        │  │ │
│  │  │  │   Pod 1    │  │   Pod 2    │  │   Pod 3    │        │  │ │
│  │  │  │            │  │            │  │            │        │  │ │
│  │  │  │  API       │  │  API       │  │  API       │        │  │ │
│  │  │  │  Container │  │  Container │  │  Container │        │  │ │
│  │  │  │  .NET 8    │  │  .NET 8    │  │  .NET 8    │        │  │ │
│  │  │  └────────────┘  └────────────┘  └────────────┘        │  │ │
│  │  │                                                          │  │ │
│  │  │  Replicas: 3 (HA)                                       │  │ │
│  │  │  HPA: Scale 2-10 based on CPU/Memory                    │  │ │
│  │  └──────────────────────────┬───────────────────────────────┘  │ │
│  │                             │                                  │ │
│  │  ┌──────────────────────────▼───────────────────────────────┐  │ │
│  │  │              Service: message-api-service                │  │ │
│  │  │              Type: ClusterIP                             │  │ │
│  │  │              Port: 80                                    │  │ │
│  │  └──────────────────────────┬───────────────────────────────┘  │ │
│  │                             │                                  │ │
│  │  ┌──────────────────────────▼───────────────────────────────┐  │ │
│  │  │              Ingress Controller                          │  │ │
│  │  │              (nginx/Azure Application Gateway)           │  │ │
│  │  │              - HTTPS termination                         │  │ │
│  │  │              - Load balancing                            │  │ │
│  │  └──────────────────────────┬───────────────────────────────┘  │ │
│  │                             │                                  │ │
│  │  ┌──────────────────────────▼───────────────────────────────┐  │ │
│  │  │              CronJob: greetings-scheduler                │  │ │
│  │  │                                                          │  │ │
│  │  │  Schedule: */1 * * * * (every minute)                   │  │ │
│  │  │                                                          │  │ │
│  │  │  Creates Pod every minute:                              │  │ │
│  │  │  ┌────────────────────────────────┐                     │  │ │
│  │  │  │  Job Pod                       │                     │  │ │
│  │  │  │  - Runs console app            │                     │  │ │
│  │  │  │  - Calls API service           │                     │  │ │
│  │  │  │  - Exits after completion      │                     │  │ │
│  │  │  │  - Cleaned up after TTL        │                     │  │ │
│  │  │  └────────────────────────────────┘                     │  │ │
│  │  │                                                          │  │ │
│  │  └──────────────────────────────────────────────────────────┘  │ │
│  │                                                                │ │
│  └────────────────────────────────────────────────────────────────┘ │
│                                                                      │
│  ┌────────────────────────────────────────────────────────────────┐ │
│  │              Azure Monitor for Containers                      │ │
│  │              + Application Insights                            │ │
│  │              - Container logs                                  │ │
│  │              - Kubernetes metrics                              │ │
│  │              - Performance data                                │ │
│  └────────────────────────────────────────────────────────────────┘ │
│                                                                      │
└──────────────────────────────────────────────────────────────────────┘

Use Case:
- Enterprise-scale applications
- Multiple microservices
- Need Kubernetes features
- Have K8s expertise

Cost: $200-500+/month
Complexity: Very High

Note: Significant overkill for this simple application
```

---

## Deployment Workflow (Recommended Approach)

```
┌─────────────────────────────────────────────────────────────────┐
│                    Development Workflow                         │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                        GitHub Repository                        │
│                                                                 │
│  ┌───────────────┐     ┌────────────────┐                      │
│  │  Source Code  │     │  GitHub Actions│                      │
│  │  - API        │────▶│  Workflows     │                      │
│  │  - Function   │     │                │                      │
│  └───────────────┘     └────────┬───────┘                      │
│                                 │                               │
└─────────────────────────────────┼───────────────────────────────┘
                                  │
                    Push to main branch
                                  │
                                  ▼
┌─────────────────────────────────────────────────────────────────┐
│                      GitHub Actions Pipeline                    │
│                                                                 │
│  1. Build & Test                                                │
│     ├─ Restore NuGet packages                                  │
│     ├─ Build solution (.NET 8)                                 │
│     ├─ Run unit tests                                          │
│     └─ Generate artifacts                                      │
│                                                                 │
│  2. Deploy to Staging                                           │
│     ├─ Deploy API to App Service (staging slot)               │
│     ├─ Deploy Function to Function App (staging)              │
│     └─ Run smoke tests                                         │
│                                                                 │
│  3. Approval Gate (Optional)                                    │
│     └─ Manual approval for production                          │
│                                                                 │
│  4. Deploy to Production                                        │
│     ├─ Swap App Service slots (API)                           │
│     ├─ Deploy Function to production                           │
│     └─ Monitor deployment                                      │
│                                                                 │
└─────────────────────────────────┬───────────────────────────────┘
                                  │
                                  ▼
┌─────────────────────────────────────────────────────────────────┐
│                         Azure Production                        │
│                                                                 │
│  ┌─────────────────┐          ┌──────────────────┐             │
│  │  Function App   │────────▶ │  App Service     │             │
│  │  (Production)   │   HTTPS  │  (Production)    │             │
│  └─────────────────┘          └──────────────────┘             │
│           │                             │                       │
│           └─────────────────────────────┘                       │
│                         │                                       │
│                         ▼                                       │
│           ┌──────────────────────────┐                          │
│           │  Application Insights   │                          │
│           │  - Monitor performance   │                          │
│           │  - Track errors          │                          │
│           │  - Alert on issues       │                          │
│           └──────────────────────────┘                          │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

---

## Data Flow Diagram (Recommended Approach)

```
┌────────────────────────────────────────────────────────────────────┐
│                    Execution Flow (Every Minute)                   │
└────────────────────────────────────────────────────────────────────┘

Time: XX:XX:00 (Start of minute)
         │
         ▼
┌─────────────────────────────────────────┐
│    Azure Functions Timer Trigger        │
│                                          │
│    Cron: 0 * * * * *                    │
│    - Activates function                 │
│    - Creates execution context          │
│    - Initializes HttpClient             │
└──────────────────┬──────────────────────┘
                   │
                   │ Step 1: Function starts
                   │
                   ▼
┌─────────────────────────────────────────┐
│         Azure Function Code             │
│                                          │
│    public async Task Run(               │
│        [TimerTrigger] TimerInfo timer)  │
│    {                                     │
│        var client = new HttpClient();   │
│        var url = Environment            │
│                  .GetEnvironmentVar...  │
│                                          │
└──────────────────┬──────────────────────┘
                   │
                   │ Step 2: Make HTTP request
                   │
                   ▼
┌─────────────────────────────────────────┐
│     HTTPS Request                       │
│                                          │
│     GET https://api.example.azure       │
│         .com/api/message                │
│                                          │
│     Headers:                             │
│     - User-Agent: Azure-Functions/1.0   │
│     - Accept: application/json          │
└──────────────────┬──────────────────────┘
                   │
                   │ Step 3: Request reaches API
                   │
                   ▼
┌─────────────────────────────────────────┐
│      Azure App Service (API)            │
│                                          │
│      API Controller receives request    │
│      - Generates timestamp              │
│      - Creates response object          │
│      - Serializes to JSON               │
└──────────────────┬──────────────────────┘
                   │
                   │ Step 4: API responds
                   │
                   ▼
┌─────────────────────────────────────────┐
│      HTTP Response                      │
│                                          │
│      Status: 200 OK                     │
│      Content-Type: application/json     │
│                                          │
│      Body:                               │
│      {                                   │
│        "message": "2025-11-05 10:30:00  │
│                    - Hello World",      │
│        "timestamp": "2025-11-05T10:30   │
│                      :00.000Z"          │
│      }                                   │
└──────────────────┬──────────────────────┘
                   │
                   │ Step 5: Function processes response
                   │
                   ▼
┌─────────────────────────────────────────┐
│      Azure Function Processing          │
│                                          │
│        var response = await client      │
│            .GetAsync(url);               │
│        var content = await response     │
│            .Content.ReadAsStringAsync(); │
│                                          │
│        _logger.LogInformation(          │
│            "Received: {content}",        │
│            content);                     │
│    }                                     │
│    // Function completes                │
└──────────────────┬──────────────────────┘
                   │
                   │ Step 6: Log to Application Insights
                   │
                   ▼
┌─────────────────────────────────────────┐
│      Application Insights               │
│                                          │
│      Logs captured:                     │
│      - Function execution start         │
│      - HTTP request made                │
│      - HTTP response received           │
│      - Function execution complete      │
│      - Duration: ~500ms                 │
│      - Status: Success                  │
│                                          │
│      Metrics updated:                   │
│      - Execution count: +1              │
│      - Success rate: 100%               │
│      - Average duration: 487ms          │
└─────────────────────────────────────────┘

Time: XX:XX:01 (Execution complete - ~1 second elapsed)

┌────────────────────────────────────────────────────────────────────┐
│                              Summary                               │
│                                                                    │
│  Total execution time: ~500-1000ms                                 │
│  - Timer trigger activation: ~50ms                                 │
│  - HTTP request: ~200-400ms                                        │
│  - Response processing: ~50ms                                      │
│  - Logging: ~10-50ms                                               │
│                                                                    │
│  Resources consumed:                                               │
│  - Function execution: ~512MB memory, ~200ms compute              │
│  - API request: ~100ms, minimal CPU                               │
│                                                                    │
│  Cost per execution: ~$0.000001 (essentially free)                │
│  Monthly executions: 43,200                                        │
│  Monthly cost: ~$0.04 (within free tier)                          │
└────────────────────────────────────────────────────────────────────┘
```

---

## Cost Breakdown Comparison

```
┌─────────────────────────────────────────────────────────────────────┐
│              Monthly Cost Comparison (Production)                   │
├──────────────────┬──────────────────────────────────────────────────┤
│                  │                                                  │
│  Functions +     │  ████████████████░░░░  $75-90                    │
│  App Service ⭐  │  Recommended                                     │
│                  │                                                  │
├──────────────────┼──────────────────────────────────────────────────┤
│                  │                                                  │
│  Full Functions  │  ██████░░░░░░░░░░░░░░  $20-60                    │
│                  │  Best for low traffic                           │
│                  │                                                  │
├──────────────────┼──────────────────────────────────────────────────┤
│                  │                                                  │
│  Container Apps  │  ████████████░░░░░░░░  $50-150                   │
│                  │  Good for microservices                         │
│                  │                                                  │
├──────────────────┼──────────────────────────────────────────────────┤
│                  │                                                  │
│  Logic Apps      │  ████████████░░░░░░░░  $50-80                    │
│                  │  Low-code option                                │
│                  │                                                  │
├──────────────────┼──────────────────────────────────────────────────┤
│                  │                                                  │
│  VMs             │  ████████████████████  $100-300                  │
│  (Lift & Shift)  │  No modernization                               │
│                  │                                                  │
├──────────────────┼──────────────────────────────────────────────────┤
│                  │                                                  │
│  AKS             │  ████████████████████████████  $200-500+         │
│                  │  Enterprise/complex apps                        │
│                  │                                                  │
└──────────────────┴──────────────────────────────────────────────────┘

                $0        $100       $200       $300       $400      $500+
```

---

## Security Architecture

```
┌─────────────────────────────────────────────────────────────────────┐
│                    Security Layers (Recommended)                    │
│                                                                     │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │              Azure Active Directory (Optional)                │  │
│  │              - Identity and Access Management                 │  │
│  │              - OAuth 2.0 / OpenID Connect                     │  │
│  └──────────────────────┬───────────────────────────────────────┘  │
│                         │ Authentication                            │
│                         ▼                                           │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │              Azure API Management (Optional)                  │  │
│  │              - API Gateway                                    │  │
│  │              - Rate limiting                                  │  │
│  │              - Throttling                                     │  │
│  │              - API keys                                       │  │
│  └──────────────────────┬───────────────────────────────────────┘  │
│                         │                                           │
│                         ▼                                           │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │              Azure Front Door / Application Gateway           │  │
│  │              - WAF (Web Application Firewall)                 │  │
│  │              - DDoS protection                                │  │
│  │              - SSL/TLS termination                            │  │
│  │              - Global load balancing                          │  │
│  └──────────────────────┬───────────────────────────────────────┘  │
│                         │ HTTPS Only                                │
│                         ▼                                           │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │              Application Layer                                │  │
│  │  ┌────────────────┐            ┌────────────────┐             │  │
│  │  │ Azure Function │            │ App Service    │             │  │
│  │  │                │            │                │             │  │
│  │  │ - Managed ID   │            │ - Managed ID   │             │  │
│  │  │ - Key Vault    │───────────▶│ - Key Vault    │             │  │
│  │  │   integration  │            │   integration  │             │  │
│  │  └────────────────┘            └────────────────┘             │  │
│  └──────────────────────────┬─────────────────────────────────────┘│
│                             │                                       │
│                             ▼                                       │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │              Azure Key Vault                                  │  │
│  │              - Secrets (API keys, connection strings)         │  │
│  │              - Certificates                                   │  │
│  │              - Encryption keys                                │  │
│  │              - Access policies                                │  │
│  └──────────────────────────────────────────────────────────────┘  │
│                                                                     │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │              Azure Monitor & Security Center                  │  │
│  │              - Security alerts                                │  │
│  │              - Threat detection                               │  │
│  │              - Compliance monitoring                          │  │
│  │              - Audit logs                                     │  │
│  └──────────────────────────────────────────────────────────────┘  │
│                                                                     │
└─────────────────────────────────────────────────────────────────────┘

Security Best Practices Applied:
✅ HTTPS only (TLS 1.2+)
✅ Managed identities (no credentials in code)
✅ Azure Key Vault for secrets
✅ Network isolation options (VNet integration)
✅ Application-level authentication (optional)
✅ Monitoring and alerting
✅ Regular security updates (PaaS auto-patching)
```

---

## Migration Timeline Visualization

```
Week 1: Preparation & Development
├─ Day 1: Setup & Planning
│  ├─ [✓] Azure subscription setup
│  ├─ [✓] Install development tools
│  ├─ [✓] Create Azure resources
│  └─ [✓] Code review
│
├─ Day 2-3: Code Migration
│  ├─ [✓] Migrate API to ASP.NET Core
│  ├─ [✓] Convert Console to Function
│  ├─ [✓] Local testing
│  └─ [✓] Unit tests
│
├─ Day 4: Azure Deployment (Staging)
│  ├─ [✓] Deploy API to staging
│  ├─ [✓] Deploy Function to staging
│  ├─ [✓] Integration testing
│  └─ [✓] Performance testing
│
└─ Day 5: Production & Monitoring
   ├─ [✓] Production deployment
   ├─ [✓] Cutover from old system
   ├─ [✓] 24h monitoring
   └─ [✓] Validation

Week 2: Optimization & Cleanup
├─ Day 6-7: Monitoring & Tuning
│  ├─ [ ] Review metrics
│  ├─ [ ] Optimize performance
│  ├─ [ ] Adjust scaling rules
│  └─ [ ] Cost optimization
│
└─ Day 8-10: Documentation & Decommission
   ├─ [ ] Create documentation
   ├─ [ ] Team training
   ├─ [ ] Decommission old infra
   └─ [ ] Post-mortem review

Success Metrics:
→ ~1,440 function executions per day
→ API response time < 500ms
→ Zero downtime during migration
→ Cost within $75-90/month budget
```

---

This comprehensive architecture documentation provides visual representations of all modernization approaches discussed in the assessment, helping stakeholders understand the technical architecture, data flow, security considerations, and implementation timeline for migrating to Azure.
