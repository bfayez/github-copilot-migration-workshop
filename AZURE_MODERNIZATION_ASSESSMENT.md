# Azure Modernization Assessment

## Executive Summary

This document provides a comprehensive analysis and recommendation for modernizing the current .NET Framework 4.8.1 application for Azure cloud deployment. After analyzing the application architecture, scheduled task requirements, and Azure service offerings, **the recommended approach is to use Azure Functions with Timer Trigger for the scheduled console application and Azure App Service for the REST API**.

---

## Current Application Analysis

### Application Components

1. **MessageService** (REST API)
   - Technology: ASP.NET Web API 2 (.NET Framework 4.8.1)
   - Endpoint: `GET /api/message`
   - Functionality: Returns timestamped greeting messages
   - Hosting: IIS/IIS Express
   - Dependencies: Newtonsoft.Json, Swashbuckle (Swagger)

2. **GreetingsConsole** (Console Application)
   - Technology: .NET Framework 4.8.1 Console App
   - Functionality: Calls MessageService API and displays results
   - **Critical Requirement**: Scheduled to run every minute
   - Dependencies: HttpClient, Newtonsoft.Json

### Current Architecture Limitations

- **Platform Lock-in**: Windows-only due to .NET Framework 4.8.1
- **Infrastructure Dependencies**: Requires IIS for API hosting
- **Scheduling**: Requires Windows Task Scheduler or external scheduling mechanism
- **Scalability**: Manual scaling with IIS configuration
- **Cloud-Native**: Not designed for cloud-native deployment patterns
- **Deployment**: Traditional deployment model requiring Windows infrastructure

---

## Recommended Approach: Azure Functions + Azure App Service

### Architecture Overview

```
┌─────────────────────────────────────────────────────────┐
│                     Azure Cloud                        │
│                                                         │
│  ┌──────────────────────┐      ┌──────────────────┐   │
│  │  Azure Function      │      │  Azure App       │   │
│  │  (Timer Trigger)     │─────▶│  Service         │   │
│  │                      │ HTTP │                  │   │
│  │  - Runs every minute │      │  - REST API      │   │
│  │  - Calls API         │      │  - /api/message  │   │
│  │  - .NET 8            │      │  - .NET 8        │   │
│  └──────────────────────┘      └──────────────────┘   │
│           │                              │             │
│           │                              │             │
│  ┌────────▼──────────────────────────────▼────────┐   │
│  │      Application Insights                      │   │
│  │      (Monitoring & Logging)                    │   │
│  └────────────────────────────────────────────────┘   │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

### Solution Components

1. **Azure App Service (REST API)**
   - Host the modernized REST API
   - Migrate from ASP.NET Web API 2 to ASP.NET Core Web API
   - Target: .NET 8 (current LTS)
   - Built-in scaling, SSL, and custom domains
   - Supports Linux or Windows hosting (recommend Linux for cost savings)

2. **Azure Function with Timer Trigger (Scheduled Task)**
   - Replace console application with Timer-triggered Azure Function
   - Schedule: `0 * * * * *` (cron expression for every minute)
   - Target: .NET 8 Isolated Worker
   - Uses HttpClient to call the API
   - Serverless execution model

3. **Application Insights**
   - Unified monitoring for both components
   - Distributed tracing
   - Performance metrics
   - Error tracking and alerting

### Why This Approach is Recommended

1. **Cost-Effective**
   - App Service: Predictable monthly cost with free/shared tier available for dev
   - Azure Functions: Consumption plan charges only for execution time
   - Running every minute = 43,200 executions/month (well within free tier of 1M executions)

2. **Operational Simplicity**
   - Minimal infrastructure management
   - Azure handles scaling, patching, and availability
   - Simple deployment via Azure CLI, GitHub Actions, or Azure DevOps
   - Built-in CI/CD integration

3. **Modern Technology Stack**
   - Migrate to .NET 8 (cross-platform, better performance)
   - Access to modern libraries and frameworks
   - Better security and long-term support

4. **Reliable Scheduling**
   - Azure Functions Timer Trigger is purpose-built for scheduled tasks
   - No need to manage external schedulers
   - Built-in retry mechanisms and monitoring
   - Guaranteed execution with proper configuration

5. **Scalability**
   - App Service: Scale up/out based on demand
   - Azure Functions: Automatic scaling (though not needed for timer triggers)
   - Ready for future growth

6. **Perfect Fit for Use Case**
   - Simple REST API → App Service is ideal
   - Scheduled task (every minute) → Timer Trigger is purpose-built for this
   - No complex orchestration needed

---

## Alternative Approaches

### Alternative 1: Azure Container Apps

**Architecture:**
- Deploy both API and scheduled task as containers
- Use KEDA (Kubernetes Event-Driven Autoscaling) for scheduling
- Container-based deployment with full control

**When to Consider:**
- You want containerization benefits
- Planning to add more microservices
- Need more control over the runtime environment
- Already using containers in your organization

**Advantages:**
- ✅ Complete control over runtime environment
- ✅ Consistent deployment across environments
- ✅ Easy to add more services later
- ✅ Built-in scaling with KEDA
- ✅ Support for any language/framework

**Disadvantages:**
- ❌ More complex than Functions + App Service
- ❌ Requires Docker knowledge
- ❌ More operational overhead
- ❌ Slightly higher cost for simple scenarios
- ❌ Overkill for this simple application

**Cost Estimate:**
- **Development**: ~$15-30/month (minimal instances)
- **Production**: ~$50-150/month (with proper scaling)

**Complexity:** ⭐⭐⭐ (Medium)

### Alternative 2: Full Azure Functions (Serverless)

**Architecture:**
- REST API as HTTP-triggered Azure Functions
- Scheduled task as Timer-triggered Azure Function
- Both using Azure Functions runtime

**When to Consider:**
- Want fully serverless architecture
- API traffic is low to moderate
- Want maximum cost optimization
- Don't need advanced API features (like middleware)

**Advantages:**
- ✅ Fully serverless (both components)
- ✅ Pay only for execution time
- ✅ Automatic scaling
- ✅ Maximum cost savings for low traffic
- ✅ Simple deployment model

**Disadvantages:**
- ❌ Cold start latency for HTTP Functions
- ❌ Limited middleware options compared to ASP.NET Core
- ❌ Request timeout limits (230 seconds default)
- ❌ Less suitable for complex APIs
- ❌ May need Premium plan for consistent performance

**Cost Estimate:**
- **Development**: ~$0-5/month (within free tier)
- **Production**: ~$20-60/month (depends on API traffic)

**Complexity:** ⭐⭐ (Low-Medium)

### Alternative 3: Azure Kubernetes Service (AKS)

**Architecture:**
- Deploy both components as Kubernetes deployments
- Use CronJob for scheduled task
- Full Kubernetes orchestration

**When to Consider:**
- Large-scale enterprise application
- Multiple microservices planned
- Need advanced orchestration
- Have Kubernetes expertise in team
- Require multi-cloud portability

**Advantages:**
- ✅ Enterprise-grade orchestration
- ✅ Advanced deployment strategies
- ✅ Multi-cloud portability
- ✅ Comprehensive ecosystem
- ✅ Fine-grained control

**Disadvantages:**
- ❌ Significant complexity overhead
- ❌ Requires Kubernetes expertise
- ❌ Higher operational cost
- ❌ Massive overkill for this simple application
- ❌ Expensive for small workloads

**Cost Estimate:**
- **Development**: ~$70-100/month (single node cluster)
- **Production**: ~$200-500+/month (HA cluster)

**Complexity:** ⭐⭐⭐⭐⭐ (Very High)

### Alternative 4: Azure App Service + Azure Logic Apps

**Architecture:**
- REST API on Azure App Service
- Scheduled task using Azure Logic Apps
- Logic Apps calls the API every minute

**When to Consider:**
- Prefer low-code/no-code for scheduling
- Integration with other Azure services needed
- Want visual workflow designer
- Non-developer team members need to maintain schedules

**Advantages:**
- ✅ Visual designer for workflows
- ✅ Easy integration with other Azure services
- ✅ No code required for scheduling logic
- ✅ Built-in connectors
- ✅ Good monitoring

**Disadvantages:**
- ❌ Higher cost than Azure Functions
- ❌ Less flexible for complex logic
- ❌ Limited to Logic Apps capabilities
- ❌ May be limiting if requirements change

**Cost Estimate:**
- **Development**: ~$15-25/month
- **Production**: ~$50-80/month (based on executions)

**Complexity:** ⭐⭐ (Low-Medium)

### Alternative 5: Lift-and-Shift to Azure VMs

**Architecture:**
- Deploy Windows VMs in Azure
- Install IIS for MessageService
- Use Windows Task Scheduler for console app
- Minimal code changes

**When to Consider:**
- Need fastest migration path
- Cannot refactor code immediately
- Have Windows-specific dependencies
- Want to migrate first, modernize later

**Advantages:**
- ✅ Minimal code changes required
- ✅ Fastest migration path
- ✅ Familiar Windows environment
- ✅ Easy to implement

**Disadvantages:**
- ❌ Still managing infrastructure
- ❌ Higher cost (always-on VMs)
- ❌ Manual scaling required
- ❌ No cloud-native benefits
- ❌ Technical debt remains
- ❌ Misses modernization opportunity

**Cost Estimate:**
- **Development**: ~$30-50/month (B-series VMs)
- **Production**: ~$100-300/month (with HA)

**Complexity:** ⭐ (Low, but maintains technical debt)

---

## Detailed Comparison Table

| Criteria | **Functions + App Service** ⭐ | Container Apps | Full Functions | AKS | Logic Apps | Lift-and-Shift |
|----------|-------------------------------|----------------|----------------|-----|------------|----------------|
| **Cost (Dev)** | $10-20/month | $15-30/month | $0-5/month | $70-100/month | $15-25/month | $30-50/month |
| **Cost (Prod)** | $40-100/month | $50-150/month | $20-60/month | $200-500+/month | $50-80/month | $100-300/month |
| **Complexity** | ⭐⭐ Low-Medium | ⭐⭐⭐ Medium | ⭐⭐ Low-Medium | ⭐⭐⭐⭐⭐ Very High | ⭐⭐ Low-Medium | ⭐ Low |
| **Modernization** | ✅ Full | ✅ Full | ✅ Full | ✅ Full | ⚠️ Partial | ❌ None |
| **Scalability** | ✅ Excellent | ✅ Excellent | ✅ Excellent | ✅ Excellent | ⚠️ Good | ⚠️ Manual |
| **Operational Overhead** | ✅ Very Low | ⚠️ Medium | ✅ Very Low | ❌ High | ✅ Very Low | ❌ High |
| **Time to Implement** | 2-3 days | 3-5 days | 2-3 days | 1-2 weeks | 2-3 days | 1-2 days |
| **Cold Start** | ✅ None (App Service) | ✅ Minimal | ⚠️ Yes | ✅ None | N/A | ✅ None |
| **Monitoring** | ✅ Built-in | ✅ Built-in | ✅ Built-in | ⚠️ Requires Setup | ✅ Built-in | ⚠️ Manual |
| **Best For** | Simple APIs + Scheduled Tasks | Microservices | Low Traffic APIs | Enterprise Scale | Simple Workflows | Quick Migration |
| **Learning Curve** | ⚠️ Moderate | ❌ Steep | ⚠️ Moderate | ❌ Very Steep | ✅ Easy | ✅ Easy |
| **Future Flexibility** | ✅ Good | ✅ Excellent | ✅ Good | ✅ Excellent | ⚠️ Limited | ❌ Poor |

### Scoring Summary (Higher is Better)

1. **Azure Functions + App Service**: 88/100 ⭐ **RECOMMENDED**
   - Best balance of cost, simplicity, and modernization
   - Purpose-built for this use case

2. **Full Azure Functions**: 82/100
   - Best cost optimization
   - Good for low-traffic scenarios

3. **Azure Container Apps**: 75/100
   - Great for future growth
   - More complexity than needed now

4. **Azure Logic Apps**: 70/100
   - Easy to use but higher cost
   - Limited flexibility

5. **Lift-and-Shift VMs**: 45/100
   - Fast but doesn't modernize
   - Ongoing technical debt

6. **AKS**: 40/100
   - Excellent capabilities but massive overkill
   - Too complex and expensive for this scenario

---

## Migration Considerations

### Code Migration

#### MessageService (REST API)
- **Current**: ASP.NET Web API 2 (.NET Framework 4.8.1)
- **Target**: ASP.NET Core Web API (.NET 8)
- **Key Changes**:
  - Controller inheritance changes (`ApiController` → `ControllerBase`)
  - Routing attribute updates
  - Configuration system changes (Web.config → appsettings.json)
  - Dependency injection built-in
  - Cross-platform compatibility

**Migration Complexity**: ⭐⭐ (Low-Medium)
- Simple API with minimal dependencies
- Straightforward port to ASP.NET Core
- Can use .NET Upgrade Assistant for initial migration

#### GreetingsConsole (Console Application)
- **Current**: .NET Framework 4.8.1 Console App
- **Target**: Azure Function with Timer Trigger (.NET 8)
- **Key Changes**:
  - Convert from console app to Function app
  - Replace `Main()` with Function trigger method
  - Add timer trigger attribute with cron expression
  - Configure function app settings
  - Remove console output (use logging instead)

**Migration Complexity**: ⭐⭐ (Low-Medium)
- Simple transformation
- Core HTTP logic remains similar
- Need to adapt to serverless patterns

### Scheduling Considerations

**Current**: Scheduled to run every minute (critical requirement)

**Azure Functions Timer Trigger Configuration**:
```csharp
[Function("GreetingsScheduledTask")]
public async Task Run(
    [TimerTrigger("0 * * * * *")] TimerInfo myTimer,
    FunctionContext context)
{
    // Cron: 0 * * * * * = Every minute
    // Second Minute Hour Day Month DayOfWeek
}
```

**Important Timer Trigger Notes**:
1. **Accuracy**: May have up to 30-second variance on Consumption plan
2. **Missed Executions**: Configure `RunOnStartup` for missed schedules
3. **Monitoring**: Use Application Insights to track execution
4. **Reliability**: Consider idempotent operations
5. **Premium Plan**: For guaranteed execution timing (if needed)

**Recommendation**: Start with Consumption plan; upgrade to Premium only if exact timing is critical.

### Data and State

**Current State**: 
- Stateless application
- No database
- No persistent storage needed

**Migration Impact**: ✅ **Minimal**
- No data migration required
- No state management needed
- Simplifies migration significantly

### Configuration Management

**Current**:
- Web.config for MessageService
- App.config for GreetingsConsole

**Target**:
- Azure App Service: Application Settings
- Azure Functions: Application Settings or Azure Key Vault
- Both support environment variables

**Benefits**:
- Configuration separate from code
- Easy to change without redeployment
- Support for different environments (dev, staging, prod)
- Integration with Azure Key Vault for secrets

### Security Considerations

1. **API Security**:
   - Consider adding authentication (Azure AD, API keys)
   - Enable HTTPS only
   - Configure CORS if needed
   - Use managed identities for Azure resource access

2. **Function Security**:
   - Restrict function access with function keys (optional)
   - Use managed identity for API calls if needed
   - Store secrets in Azure Key Vault

3. **Network Security**:
   - Option to use VNet integration
   - Private endpoints for enhanced security
   - Azure Firewall for advanced scenarios

### Monitoring and Observability

**Application Insights Integration**:
- Automatic request tracking
- Performance monitoring
- Exception tracking
- Custom metrics and logging
- Distributed tracing (correlation between Function and API)

**Key Metrics to Monitor**:
- Function execution count (should be ~1440 per day)
- Function execution duration
- API response times
- Error rates
- Cost metrics

### Cost Optimization Strategies

1. **Right-Sizing**:
   - Start with App Service B1 or S1 tier (can scale down to Free tier for dev)
   - Use Consumption plan for Functions (very cost-effective)

2. **Scaling**:
   - Enable auto-scale rules based on metrics
   - Scale down during off-hours if applicable

3. **Development Environment**:
   - Use Free tier App Service for development
   - Functions Consumption plan is free for first 1M executions

4. **Monitoring**:
   - Use Azure Cost Management to track spending
   - Set up budget alerts

### Deployment Strategy

**Recommended Approach**: Blue-Green or Staged Deployment

1. **Phase 1: Setup Azure Resources**
   - Create Azure App Service
   - Create Azure Function App
   - Configure Application Insights
   - Set up deployment slots (staging)

2. **Phase 2: Migrate MessageService**
   - Deploy API to staging slot
   - Test thoroughly
   - Swap to production
   - Keep old environment running as backup

3. **Phase 3: Migrate GreetingsConsole**
   - Deploy Function to staging environment
   - Configure to call new API
   - Test timer trigger
   - Enable in production
   - Disable old scheduled task

4. **Phase 4: Validation**
   - Monitor for 24-48 hours
   - Verify scheduled executions
   - Check logs and metrics
   - Decommission old infrastructure

### Rollback Plan

- Keep old infrastructure for 1-2 weeks
- Use deployment slots for easy rollback
- Document rollback procedures
- Test rollback process before go-live

### Testing Strategy

1. **Local Testing**:
   - Test API locally with Kestrel
   - Test Function locally with Azure Functions Core Tools
   - Verify timer trigger configuration

2. **Integration Testing**:
   - Test Function calling API in Azure
   - Verify end-to-end flow
   - Check timing accuracy

3. **Load Testing**:
   - Not critical for this use case
   - API load from Function is minimal (1 request/minute)

---

## Cost Estimates

### Recommended Approach: Functions + App Service

#### Development Environment
- **Azure App Service** (Free or B1 tier): $0-13/month
- **Azure Function** (Consumption plan): ~$0/month (within free tier)
- **Application Insights**: ~$0-5/month (within free tier for low volume)
- **Total**: **$0-18/month**

#### Production Environment (Moderate Traffic)
- **Azure App Service** (S1 tier - 100 total ACU, 1.75 GB memory): ~$70/month
- **Azure Function** (Consumption plan):
  - Executions: 43,200/month (1 per minute)
  - Execution time: ~1-2 seconds each
  - Total: ~$0.50/month (well within free tier of 400,000 GB-s)
- **Application Insights**: ~$5-20/month (depends on telemetry volume)
- **Total**: **$75-90/month**

#### Cost Optimization Notes:
- Functions are essentially free for this workload (far below free tier limits)
- Can start with Free or B1 App Service tier for dev/test
- Scale App Service tier based on actual traffic
- Production costs can range from $40-100/month depending on App Service tier

### Alternative Costs Summary

| Approach | Development | Production | Notes |
|----------|-------------|------------|-------|
| **Functions + App Service** | $0-18/mo | $75-90/mo | ⭐ Most cost-effective |
| Full Functions | $0-5/mo | $20-60/mo | Cheapest but cold starts |
| Container Apps | $15-30/mo | $50-150/mo | More expensive |
| Logic Apps | $15-25/mo | $50-80/mo | Higher execution cost |
| AKS | $70-100/mo | $200-500+/mo | Most expensive |
| VMs (Lift-and-Shift) | $30-50/mo | $100-300/mo | Always-on costs |

---

## Migration Path Overview

### Phase 1: Preparation (Day 1)
- [ ] Set up Azure subscription
- [ ] Create resource group
- [ ] Install required tools (.NET 8 SDK, Azure CLI, Azure Functions Core Tools)
- [ ] Review and understand current codebase
- [ ] Create backups of current system

### Phase 2: Code Migration (Days 2-3)
- [ ] Migrate MessageService to ASP.NET Core Web API
  - Use .NET Upgrade Assistant or manual migration
  - Update packages and dependencies
  - Convert Web.config to appsettings.json
  - Update controller code
  - Test locally
- [ ] Convert GreetingsConsole to Azure Function
  - Create new Function project (.NET 8 Isolated)
  - Implement Timer Trigger
  - Migrate HTTP client logic
  - Configure cron expression (every minute)
  - Test locally with Azure Functions Core Tools

### Phase 3: Azure Setup (Day 3)
- [ ] Create Azure App Service
  - Choose appropriate tier (B1 for dev, S1 for prod)
  - Configure Linux or Windows hosting
  - Set up deployment slots (staging)
  - Configure custom domain and SSL (if needed)
- [ ] Create Azure Function App
  - Consumption plan
  - Configure storage account
  - Set up Application Settings
- [ ] Configure Application Insights
  - Link to both App Service and Function App
  - Set up alerts and dashboards

### Phase 4: Deployment (Day 4)
- [ ] Deploy API to Azure App Service (staging slot)
  - Use Azure CLI, GitHub Actions, or Azure DevOps
  - Configure application settings
  - Verify deployment
  - Test staging endpoint
- [ ] Deploy Function to Azure (staging environment)
  - Deploy function code
  - Configure application settings (API URL)
  - Verify timer trigger configuration
  - Test function execution

### Phase 5: Testing and Validation (Day 4-5)
- [ ] Integration testing
  - Verify Function can call API
  - Check timer trigger execution
  - Monitor logs in Application Insights
  - Verify execution frequency (every minute)
- [ ] Performance testing
  - API response times
  - Function execution duration
  - Monitor resource utilization
- [ ] Security validation
  - Verify HTTPS configuration
  - Check authentication (if implemented)
  - Review security best practices

### Phase 6: Production Cutover (Day 5)
- [ ] Swap staging slot to production (API)
- [ ] Enable Function in production
- [ ] Disable old scheduled task
- [ ] Monitor for first 24 hours
  - Check execution logs
  - Verify 1,440 executions per day
  - Monitor Application Insights
  - Check for errors or issues

### Phase 7: Optimization and Decommission (Week 2)
- [ ] Review and optimize based on metrics
- [ ] Configure auto-scaling if needed
- [ ] Set up cost alerts
- [ ] Document the new system
- [ ] Decommission old infrastructure
- [ ] Post-migration review

**Total Timeline**: 5-7 days for implementation, 1-2 weeks for complete migration including monitoring.

---

## Success Criteria

The migration will be considered successful when:

1. ✅ **Functionality**: 
   - API returns timestamped messages correctly
   - Function executes every minute reliably
   - End-to-end flow works as expected

2. ✅ **Performance**:
   - API response time < 500ms
   - Function execution time < 5 seconds
   - No missed executions

3. ✅ **Reliability**:
   - 99.9%+ uptime for API
   - ~1,440 function executions per day
   - No errors in Application Insights

4. ✅ **Cost**:
   - Production costs within $75-100/month budget
   - No unexpected charges
   - Optimized resource usage

5. ✅ **Operational**:
   - Easy to deploy updates
   - Good monitoring and alerting
   - Documented deployment process
   - Team trained on new system

---

## Next Steps

### Immediate Actions:

1. **Review and Approve**: Review this assessment and approve the recommended approach
2. **Create Migration Issue**: Create a new issue for implementing the migration
3. **Set Up Azure**: Create Azure subscription and resource group
4. **Install Tools**: Install .NET 8 SDK, Azure CLI, and Azure Functions Core Tools

### Questions to Consider:

1. Do you have an Azure subscription ready?
2. What is your target timeline for migration?
3. Do you have any specific security or compliance requirements?
4. Do you need CI/CD pipeline setup (GitHub Actions or Azure DevOps)?
5. Do you need custom domain and SSL certificate setup?

### Want to Proceed?

If you're ready to move forward with the recommended approach, I can:
- Create detailed implementation code
- Provide step-by-step migration scripts
- Set up CI/CD pipelines
- Create deployment documentation
- Help with Azure resource provisioning

Let me know how you'd like to proceed! 🚀

---

## Appendix: Additional Resources

### Azure Documentation
- [Azure App Service Documentation](https://docs.microsoft.com/azure/app-service/)
- [Azure Functions Documentation](https://docs.microsoft.com/azure/azure-functions/)
- [Application Insights Documentation](https://docs.microsoft.com/azure/azure-monitor/app/app-insights-overview)
- [Azure Functions Timer Trigger](https://docs.microsoft.com/azure/azure-functions/functions-bindings-timer)

### Migration Resources
- [.NET Upgrade Assistant](https://dotnet.microsoft.com/platform/upgrade-assistant)
- [Migrate ASP.NET Web API to ASP.NET Core](https://docs.microsoft.com/aspnet/core/migration/webapi)
- [Azure Migration Center](https://azure.microsoft.com/migration/)

### Cost Management
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)
- [Azure Cost Management](https://docs.microsoft.com/azure/cost-management-billing/)
- [Azure Functions Pricing](https://azure.microsoft.com/pricing/details/functions/)
- [App Service Pricing](https://azure.microsoft.com/pricing/details/app-service/)

### Best Practices
- [Azure Architecture Center](https://docs.microsoft.com/azure/architecture/)
- [Cloud Design Patterns](https://docs.microsoft.com/azure/architecture/patterns/)
- [Azure Well-Architected Framework](https://docs.microsoft.com/azure/architecture/framework/)

---

**Document Version**: 1.0  
**Last Updated**: 2025-11-05  
**Author**: GitHub Copilot  
**Status**: Ready for Review
