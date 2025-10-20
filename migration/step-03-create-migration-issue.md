# Step 3: Create Migration Issue

## Objective
In this step, you'll create a new GitHub issue asking Copilot to implement the migration based on the recommended approach from the assessment.

### Step Progress Checklist
- [ ] Review the recommended approach from Step 2
- [ ] Create a new migration issue
- [ ] Write a detailed migration prompt
- [ ] Request step-by-step deployment instructions
- [ ] Assign the issue to GitHub Copilot
- [ ] Wait for Copilot to complete the migration
- [ ] Verify Copilot has created a Pull Request

---

## Prerequisites

Before creating the migration issue, ensure:

- [ ] You've completed Step 2 and reviewed the assessment
- [ ] You understand the recommended approach
- [ ] You know which Azure services will be used
- [ ] The assessment issue is still open (you can reference it)

---

## Step-by-Step Instructions

### 1. Create a New Issue

- [ ] Go to your repository on GitHub
- [ ] Click on the "Issues" tab
- [ ] Click "New issue" button

### 2. Write the Migration Issue

Use the following template for your migration issue:

#### Issue Title
```
Migrate application to Azure using [RECOMMENDED APPROACH]
```

**Replace `[RECOMMENDED APPROACH]` with what Copilot recommended, for example:**
- "Azure Functions and App Service"
- "Azure Container Apps"
- "Azure Functions with Timer Trigger"

#### Issue Description

```markdown
@copilot Based on your assessment in issue #[ISSUE_NUMBER], please migrate our application to Azure using the recommended approach.

## Migration Request

Please implement the migration following the recommended architecture you provided:

**Recommended Approach**: [STATE THE RECOMMENDED APPROACH HERE]

## Components to Migrate

1. **MessageService (REST API)**
   - Current: ASP.NET Web API 2 (.NET Framework 4.8.1)
   - Target: [Azure service recommended - e.g., Azure App Service, Azure Functions, etc.]

2. **GreetingsConsole (Scheduled Task)**
   - Current: Console application that runs every minute
   - Target: [Azure service recommended - e.g., Azure Function with Timer Trigger]

## Required Deliverables

Please provide:

### 1. Migrated Code
- [ ] Modernized API implementation
- [ ] Modernized scheduled task implementation
- [ ] Updated to modern .NET (if recommended)
- [ ] All necessary configuration files
- [ ] Infrastructure as Code (if applicable)

### 2. Deployment Instructions
- [ ] **Step-by-step deployment guide**
- [ ] Azure resource creation steps
- [ ] Configuration requirements
- [ ] Environment variables and secrets setup
- [ ] How to deploy the API
- [ ] How to deploy the scheduled task
- [ ] How to verify the deployment

### 3. Local Testing Guide
- [ ] How to run the migrated application locally
- [ ] How to test in GitHub Codespaces
- [ ] Any required local dependencies
- [ ] How to verify functionality without deploying to Azure

### 4. Documentation
- [ ] Architecture diagram or description
- [ ] Changes made from the original application
- [ ] Configuration guide
- [ ] Troubleshooting tips

## Important Requirements

1. **Preserve Functionality**: The migrated application should maintain the same functionality
2. **Scheduled Execution**: Ensure the task runs every minute as in the original
3. **Cloud-Native**: Use Azure-native services as recommended
4. **Documentation**: Provide clear deployment and testing instructions
5. **Testable**: Must be testable locally before Azure deployment

## Success Criteria

The migration will be successful when:
- [ ] API returns the same timestamped greeting messages
- [ ] Scheduled task runs every minute and calls the API
- [ ] Application can be tested locally (in Codespaces)
- [ ] Clear deployment instructions are provided
- [ ] All code follows modern best practices

## Additional Notes

- Please use Infrastructure as Code (ARM, Bicep, or Terraform) if appropriate
- Include any necessary configuration files (.env, appsettings.json, etc.)
- Provide clear comments in the code for learning purposes
- Consider cost optimization in the implementation

Thank you! 🚀
```

### 3. Customize the Prompt

**Important**: Before submitting, customize these parts:

- [ ] Replace `[ISSUE_NUMBER]` with the assessment issue number (e.g., #1)
- [ ] Replace `[STATE THE RECOMMENDED APPROACH HERE]` with the actual recommendation
- [ ] Replace `[Azure service recommended...]` with the specific services mentioned

**Example of Customized Sections:**
```markdown
**Recommended Approach**: Azure Functions with Timer Trigger + Azure App Service

1. **MessageService (REST API)**
   - Current: ASP.NET Web API 2 (.NET Framework 4.8.1)
   - Target: Azure App Service (or Azure Functions with HTTP trigger)

2. **GreetingsConsole (Scheduled Task)**
   - Current: Console application that runs every minute
   - Target: Azure Function with Timer Trigger (cron: "0 */1 * * * *")
```

### 4. Assign to GitHub Copilot

- [ ] Find "Assignees" on the right sidebar
- [ ] Type and select `@copilot` or `copilot`
- [ ] Verify Copilot is assigned

### 5. Add Reference to Assessment Issue

- [ ] In your issue description, reference the assessment issue
- [ ] Use `#[number]` format (e.g., "Based on your assessment in issue #1")
- [ ] This helps Copilot understand the context

### 6. Add Labels (Optional)

Consider adding:
- [ ] `migration`
- [ ] `azure`
- [ ] `copilot-task`
- [ ] `enhancement`

### 7. Submit the Issue

- [ ] Review all content
- [ ] Ensure all placeholders are replaced
- [ ] Click "Submit new issue"
- [ ] Note the issue number

---

## What Happens Next?

After submitting the migration issue:

### Copilot's Workflow
1. **Analysis** (1-2 minutes)
   - Copilot analyzes the current codebase
   - Reviews the assessment recommendations
   - Plans the migration approach

2. **Implementation** (5-15 minutes)
   - Creates migrated code
   - Updates or creates new project files
   - Adds configuration files
   - Writes documentation

3. **Pull Request Creation**
   - Copilot creates a PR with all changes
   - Links the PR to the issue
   - Adds description of changes made

### You'll Know It's Done When:
- [ ] You receive a GitHub notification
- [ ] A Pull Request is created and linked to the issue
- [ ] The PR contains the migrated code
- [ ] Deployment instructions are included

---

## Expected Deliverables

When Copilot completes the migration, expect to see:

### In the Pull Request:
- [ ] **New/Updated Code Files**
  - Modernized API implementation
  - Scheduled task implementation
  - Configuration files

- [ ] **Infrastructure Code** (if applicable)
  - ARM templates, Bicep files, or Terraform
  - Deployment scripts

- [ ] **Documentation**
  - README with deployment steps
  - Local testing guide
  - Architecture overview

- [ ] **Configuration**
  - Environment variable templates
  - Settings files
  - Connection strings (templates)

---

## Tips for a Successful Migration Request

### ✅ Do's:
- Reference the assessment issue for context
- Be specific about requirements (runs every minute)
- Request both deployment and local testing instructions
- Ask for step-by-step guidance
- Request documentation

### ❌ Don'ts:
- Don't create multiple migration issues simultaneously
- Don't modify the issue while Copilot is working
- Don't specify implementation details unless necessary (let Copilot decide)
- Don't close the issue before the PR is created

---

## Troubleshooting

### Issue: Copilot Doesn't Respond
**Solutions:**
- Wait 5-10 minutes (complex migrations take time)
- Verify Copilot is assigned
- Check that issue description is clear
- Ensure repository has Copilot access

### Issue: Missing Context
**Solutions:**
- Add reference to assessment issue (#number)
- Include key requirements in the issue
- Mention specific Azure services if known

### Issue: Incomplete Deliverables
**Solutions:**
- Comment on the issue asking for missing items
- Specifically request what's missing (e.g., "@copilot Please provide deployment instructions")

---

## Example Complete Issue

Here's what your complete issue might look like:

```
Title: Migrate application to Azure using Azure Functions and App Service

Body:
@copilot Based on your assessment in issue #1, please migrate our application 
to Azure using the recommended approach.

## Migration Request
Please implement the migration following the recommended architecture you provided:

**Recommended Approach**: Azure Functions (Timer Trigger) + Azure App Service

## Components to Migrate
[... rest of the template with customizations ...]
```

---

## Verification Checklist

Before moving to the next step, ensure:

- [ ] Migration issue is created with clear requirements
- [ ] Assessment issue is referenced (#number)
- [ ] Recommended approach is specified
- [ ] Deployment instructions are requested
- [ ] Local testing guide is requested
- [ ] GitHub Copilot is assigned
- [ ] Issue is submitted

---

## While You Wait

As Copilot works on the migration:

- Review Azure service documentation for the recommended services
- Prepare your Azure subscription (if deploying)
- Set up Azure CLI or Azure Portal access
- Review .NET migration guides
- Prepare GitHub Codespaces for testing

---

## Time Expectations

**Typical Migration Timeline:**
- **Analysis**: 1-2 minutes
- **Implementation**: 5-15 minutes
- **PR Creation**: 1-2 minutes
- **Total**: 10-20 minutes (can vary based on complexity)

---

## Next Steps

Once Copilot creates the Pull Request with the migrated code:

➡️ **[Proceed to Step 4: Review Migration Work](step-04-review-migration.md)**

---

## Additional Resources

- [Azure Migration Center](https://azure.microsoft.com/migration/)
- [.NET Application Migration](https://dotnet.microsoft.com/learn/azure/architecture)
- [Azure Functions Best Practices](https://docs.microsoft.com/azure/azure-functions/functions-best-practices)
- [GitHub Copilot PR Review](https://docs.github.com/en/copilot)

---

⬅️ [Back to Step 2](step-02-review-assessment.md) | [Next: Review Migration](step-04-review-migration.md) ➡️
