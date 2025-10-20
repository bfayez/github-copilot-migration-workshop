# Step 4: Review Migration Work

## Objective
In this step, you'll review the Pull Request created by GitHub Copilot containing the migrated code and implementation.

### Step Progress Checklist
- [ ] Locate the Pull Request created by Copilot
- [ ] Review the code changes
- [ ] Check the deployment instructions
- [ ] Verify local testing guide is included
- [ ] Understand the new architecture
- [ ] Review configuration requirements
- [ ] Ask questions if needed (optional)
- [ ] Prepare for local testing

---

## Finding the Pull Request

### 1. Navigate to Pull Requests

- [ ] Go to your repository on GitHub
- [ ] Click on the "Pull Requests" tab
- [ ] Look for a PR created by @copilot
- [ ] The PR should be linked to your migration issue

### 2. Identify the Migration PR

The PR title will typically be something like:
- "Migrate application to Azure"
- "Implement Azure migration"
- "Modernize application for Azure"

- [ ] Click on the Pull Request to open it
- [ ] Verify it's linked to your migration issue

---

## What to Review in the Pull Request

### 1. PR Description 📝

The PR description should contain:

- [ ] **Summary of Changes**: Overview of what was migrated
- [ ] **Architecture Overview**: How the new system is structured
- [ ] **Key Changes**: Major modifications from the original
- [ ] **Migration Approach**: Brief description of the strategy used

**Read and understand:**
- What approach was used for the migration?
- How are the components restructured?
- What Azure services are being used?

### 2. Files Changed 📁

Review the "Files changed" tab to see:

- [ ] New project files or updated existing ones
- [ ] Configuration files (appsettings.json, local.settings.json, etc.)
- [ ] Infrastructure as Code files (if included)
- [ ] Documentation files (README, deployment guides)
- [ ] Any test files

**Key Areas to Check:**

#### API Implementation
- [ ] How is the MessageService implemented in the new architecture?
- [ ] Is it using Azure Functions, App Service, or Containers?
- [ ] Are the endpoints the same or modified?
- [ ] Is the functionality preserved?

#### Scheduled Task Implementation
- [ ] How is the scheduled task (every minute) implemented?
- [ ] Is it using Timer Trigger, Logic Apps, or another mechanism?
- [ ] Is the schedule configured correctly (every minute)?
- [ ] Does it call the API endpoint correctly?

#### Configuration
- [ ] Environment variables and app settings
- [ ] Connection strings (if applicable)
- [ ] Azure service configuration
- [ ] Local development settings

### 3. Deployment Instructions 🚀

Look for deployment documentation, typically in:
- [ ] README.md
- [ ] DEPLOYMENT.md
- [ ] docs/deployment.md
- [ ] Comments in the PR description

**The deployment guide should include:**

- [ ] **Prerequisites**: What you need before deploying
  - Azure subscription
  - Azure CLI or Portal access
  - Required tools and SDKs

- [ ] **Azure Resource Creation**: Step-by-step instructions
  - How to create the resource group
  - How to create each Azure service
  - Configuration for each service

- [ ] **Application Deployment**: How to deploy the code
  - Deployment commands or scripts
  - Configuration steps
  - Environment variable setup

- [ ] **Verification Steps**: How to verify the deployment
  - How to test the API
  - How to check the scheduled task
  - How to view logs

### 4. Local Testing Guide 🧪

This is crucial for Step 5. Look for:

- [ ] **Local Setup Instructions**
  - How to run locally without Azure
  - Required local dependencies
  - Configuration for local development

- [ ] **GitHub Codespaces Instructions**
  - How to test in Codespaces
  - Any Codespace-specific setup
  - How to simulate Azure services locally

- [ ] **Testing Steps**
  - How to start the API locally
  - How to test the scheduled task
  - How to verify functionality

---

## Understanding Common Migration Patterns

Here's what you might see based on different approaches:

### Pattern 1: Azure Functions + App Service

**Files You'll See:**
```
├── api/                          # API project
│   ├── Program.cs               # Modern .NET minimal API or
│   ├── Startup.cs               # .NET Core Web API
│   └── appsettings.json         # Configuration
├── scheduler/                    # Scheduled task
│   ├── TimerFunction.cs         # Azure Function
│   ├── host.json                # Function host config
│   └── local.settings.json      # Local settings
├── infra/                       # Infrastructure (optional)
│   └── main.bicep              # Bicep template
└── README.md                    # Documentation
```

**Scheduled Task**: Azure Function with Timer Trigger
```csharp
[FunctionName("ScheduledGreeting")]
public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo timer)
{
    // Calls API every minute
}
```

### Pattern 2: Azure Container Apps

**Files You'll See:**
```
├── src/
│   ├── Api/                     # API project
│   └── Scheduler/               # Scheduled task
├── Dockerfile                   # For API
├── Dockerfile.scheduler         # For scheduler
├── docker-compose.yml           # Local testing
├── deployment/                  # K8s or ACA manifests
└── README.md
```

**Scheduled Task**: Cron job in Container Apps or Kubernetes

### Pattern 3: All Azure Functions

**Files You'll See:**
```
├── Functions/
│   ├── HttpMessageFunction.cs   # API endpoint
│   ├── TimerGreetingFunction.cs # Scheduled task
│   ├── host.json
│   └── local.settings.json
└── README.md
```

---

## Key Questions to Answer

As you review, answer these questions:

### Functionality
- [ ] Does the API endpoint work the same way?
- [ ] Is the scheduled task configured to run every minute?
- [ ] Is the message format preserved?
- [ ] Are error handling mechanisms in place?

### Architecture
- [ ] What Azure services are used?
- [ ] How do components communicate?
- [ ] Is it cloud-native and scalable?
- [ ] Does it follow Azure best practices?

### Configuration
- [ ] What needs to be configured?
- [ ] How are secrets managed?
- [ ] What environment variables are needed?
- [ ] Is local configuration separate from Azure?

### Deployment
- [ ] Are deployment steps clear?
- [ ] Is Infrastructure as Code provided?
- [ ] Can I deploy incrementally?
- [ ] Are rollback procedures mentioned?

### Testing
- [ ] Can I test locally without Azure?
- [ ] Will it work in GitHub Codespaces?
- [ ] Are there any mock services needed?
- [ ] How do I verify functionality?

---

## Reviewing Code Changes

### API Code Review Checklist

- [ ] **Endpoints**: Verify the API endpoint paths
  - Original: `GET /api/message`
  - Migrated: Should be the same or clearly documented

- [ ] **Response Format**: Check if response structure is preserved
  ```json
  {
    "message": "timestamp - Hello World",
    "timestamp": "ISO-8601 datetime"
  }
  ```

- [ ] **Dependencies**: Review NuGet packages or dependencies
  - Modern .NET packages
  - Azure SDK packages
  - No unnecessary dependencies

- [ ] **Error Handling**: Ensure proper error handling exists

### Scheduled Task Code Review Checklist

- [ ] **Schedule Configuration**: Verify timer/cron expression
  - Should trigger every minute
  - Format: `"0 */1 * * * *"` for Azure Functions
  - Or appropriate format for the chosen service

- [ ] **API Call**: Check how it calls the API
  - Uses HTTP client
  - Correct endpoint URL
  - Handles responses properly

- [ ] **Error Handling**: Retry logic and error handling

---

## Configuration Review

### Check Configuration Files

- [ ] **Application Settings**
  - API URL for the scheduled task
  - Any feature flags
  - Logging configuration

- [ ] **Local Development Settings**
  - Local emulator settings (if using Azure Functions)
  - Local API URL
  - Debug settings

- [ ] **Azure Settings**
  - Resource names
  - Connection strings
  - Feature settings

### Security Review

- [ ] Secrets are not hardcoded
- [ ] Sensitive data uses environment variables
- [ ] Configuration templates are provided (not actual secrets)
- [ ] Azure Key Vault mentioned (if applicable)

---

## Asking Questions (Optional)

If something is unclear, comment on the PR:

### How to Ask Questions

1. [ ] Go to the "Files changed" tab
2. [ ] Find the relevant line of code
3. [ ] Click the "+" icon to add a comment
4. [ ] Mention @copilot and ask your question

### Example Questions:

```markdown
@copilot Why was this specific Azure service chosen over alternatives?
```

```markdown
@copilot Can you explain how the timer trigger schedule is configured?
```

```markdown
@copilot How do I configure the API URL for local testing?
```

```markdown
@copilot Are there any specific Azure permissions needed for deployment?
```

---

## Documentation Review

### Check for Complete Documentation

- [ ] **README or Deployment Guide** exists
- [ ] **Architecture diagram or description** is clear
- [ ] **Prerequisites** are listed
- [ ] **Step-by-step deployment** instructions provided
- [ ] **Local testing** instructions included
- [ ] **Configuration** guide is complete
- [ ] **Troubleshooting** section exists

### Verify Step-by-Step Instructions

The deployment guide should have clear steps like:

```markdown
## Deployment Steps

1. Create Azure Resource Group
   ```bash
   az group create --name rg-migration --location eastus
   ```

2. Deploy Azure Function App
   ```bash
   az functionapp create --name func-scheduler --resource-group rg-migration
   ```

3. Deploy API
   [specific commands]

4. Configure Settings
   [configuration steps]

5. Verify Deployment
   [verification steps]
```

---

## Common Issues and Solutions

### Issue: Missing Deployment Instructions
**Solution:**
- Comment on the PR: "@copilot Please provide step-by-step deployment instructions"

### Issue: No Local Testing Guide
**Solution:**
- Ask: "@copilot How do I run and test this locally in GitHub Codespaces?"

### Issue: Unclear Configuration
**Solution:**
- Request: "@copilot Please explain the required configuration settings"

### Issue: Different Architecture Than Expected
**Solution:**
- Ask: "@copilot Why was this approach chosen over [other approach]?"

---

## Verification Checklist

Before proceeding to local testing:

- [ ] I understand the new architecture
- [ ] I've reviewed all code changes
- [ ] Deployment instructions are clear and complete
- [ ] Local testing guide is provided
- [ ] Configuration requirements are documented
- [ ] I know which Azure services are used
- [ ] I've asked any clarification questions needed
- [ ] I'm ready to test the application locally

---

## Making Notes for Testing

Document the following for Step 5:

- [ ] **Local testing approach**: How to run locally
- [ ] **Required tools**: What needs to be installed
- [ ] **Configuration**: Environment variables needed
- [ ] **Verification steps**: How to confirm it works

**Example Notes:**
```
Local Testing Setup:
- Run Azure Functions Core Tools locally
- API URL: http://localhost:7071/api/message
- Timer function runs every minute automatically
- Check local.settings.json for configuration
```

---

## Next Steps

Once you've thoroughly reviewed the migration work:

➡️ **[Proceed to Step 5: Local Testing in Codespaces](step-05-local-testing.md)**

**Note**: You do NOT need to merge the PR yet. You'll test first, then merge in Step 5.

---

## Additional Resources

- [Azure Functions Local Development](https://docs.microsoft.com/azure/azure-functions/functions-develop-local)
- [GitHub Codespaces Documentation](https://docs.github.com/codespaces)
- [Azure App Service Local Testing](https://docs.microsoft.com/azure/app-service/)
- [Code Review Best Practices](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/reviewing-changes-in-pull-requests)

---

⬅️ [Back to Step 3](step-03-create-migration-issue.md) | [Next: Local Testing](step-05-local-testing.md) ➡️
