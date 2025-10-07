# Step 6: Azure Deployment

## Objective
In this final step, you'll deploy the migrated application to Azure using either manual deployment or automated CI/CD.

### Step Progress Checklist
- [ ] Choose deployment approach (Manual or CI/CD)
- [ ] Prepare Azure environment
- [ ] Deploy the application
- [ ] Verify deployment
- [ ] Monitor the application
- [ ] Complete the workshop

---

## Choose Your Deployment Path

You have two options for deploying to Azure:

### Option A: Manual Deployment (Recommended for Learning)
- Follow step-by-step instructions from the migration PR
- Understand each deployment step
- Good for learning Azure services
- ➡️ [Jump to Option A](#option-a-manual-deployment)

### Option B: Automated CI/CD (Recommended for Production)
- Create an issue for Copilot to set up CI/CD
- Automated deployment on every commit
- Production-ready approach
- ➡️ [Jump to Option B](#option-b-cicd-deployment)

---

## Option A: Manual Deployment

Use this option to learn the deployment process and understand Azure services.

### Prerequisites Checklist

- [ ] Azure subscription (create free account at [azure.com](https://azure.microsoft.com/free/))
- [ ] Azure CLI installed (or use Azure Cloud Shell)
- [ ] Logged into Azure CLI: `az login`
- [ ] Deployment instructions from the PR (Step 4)
- [ ] Migrated code is merged to main branch

### Step-by-Step Manual Deployment

The exact steps depend on the migration approach. Follow the deployment guide from the Pull Request documentation.

#### General Deployment Flow

##### 1. Prepare Azure Environment

- [ ] **Login to Azure**
  ```bash
  az login
  ```

- [ ] **Set the subscription** (if you have multiple)
  ```bash
  az account list --output table
  az account set --subscription "Your-Subscription-Name"
  ```

- [ ] **Create Resource Group**
  ```bash
  az group create \
    --name rg-migration-workshop \
    --location eastus
  ```

##### 2. Deploy Infrastructure

**If using Bicep/ARM Templates:**

- [ ] **Deploy infrastructure**
  ```bash
  az deployment group create \
    --resource-group rg-migration-workshop \
    --template-file infra/main.bicep \
    --parameters location=eastus
  ```

**If using Manual Resource Creation:**

Follow the specific commands from the deployment guide for:
- [ ] Creating Function App (if applicable)
- [ ] Creating App Service (if applicable)
- [ ] Creating Container Apps (if applicable)
- [ ] Creating Storage Account (if needed)
- [ ] Creating other required resources

##### 3. Deploy Application Code

**For Azure Functions:**

- [ ] **Navigate to function project**
  ```bash
  cd [function-project-folder]
  ```

- [ ] **Publish to Azure**
  ```bash
  func azure functionapp publish [your-function-app-name]
  ```

**For Azure App Service:**

- [ ] **Navigate to API project**
  ```bash
  cd [api-project-folder]
  ```

- [ ] **Publish to Azure**
  ```bash
  az webapp deploy \
    --resource-group rg-migration-workshop \
    --name [your-app-service-name] \
    --src-path [path-to-zip-or-folder]
  ```

  Or using .NET:
  ```bash
  dotnet publish -c Release
  az webapp deployment source config-zip \
    --resource-group rg-migration-workshop \
    --name [your-app-service-name] \
    --src [path-to-zip]
  ```

**For Container Apps:**

- [ ] **Build and push container**
  ```bash
  docker build -t [your-registry].azurecr.io/api:latest .
  docker push [your-registry].azurecr.io/api:latest
  ```

- [ ] **Deploy container app**
  ```bash
  az containerapp create \
    --name api-app \
    --resource-group rg-migration-workshop \
    --image [your-registry].azurecr.io/api:latest
  ```

##### 4. Configure Application Settings

- [ ] **Set environment variables**
  ```bash
  # For Function App
  az functionapp config appsettings set \
    --name [function-app-name] \
    --resource-group rg-migration-workshop \
    --settings "SETTING_NAME=value"

  # For App Service
  az webapp config appsettings set \
    --name [app-service-name] \
    --resource-group rg-migration-workshop \
    --settings "SETTING_NAME=value"
  ```

- [ ] **Configure connection strings** (if needed)
- [ ] **Set API URLs for the scheduled task**
- [ ] **Configure any required secrets**

##### 5. Verify Deployment

- [ ] **Get the API URL**
  ```bash
  # For Function App
  az functionapp show \
    --name [function-app-name] \
    --resource-group rg-migration-workshop \
    --query "defaultHostName" --output tsv

  # For App Service
  az webapp show \
    --name [app-service-name] \
    --resource-group rg-migration-workshop \
    --query "defaultHostName" --output tsv
  ```

- [ ] **Test the API endpoint**
  ```bash
  curl https://[your-app-url]/api/message
  ```

- [ ] **Check timer function logs**
  ```bash
  # Stream logs
  az webapp log tail \
    --name [app-name] \
    --resource-group rg-migration-workshop
  ```

- [ ] **Verify scheduled task is running**
  - Check Azure Portal > Function App > Monitor
  - Look for executions every minute
  - Check Application Insights (if configured)

### Verification Checklist

- [ ] API is accessible via HTTPS
- [ ] API endpoint returns correct response
- [ ] Scheduled task is running every minute
- [ ] Task successfully calls the API
- [ ] No errors in logs
- [ ] Application Insights showing data (if configured)

### Manual Deployment Complete! 🎉

- [ ] Document your deployment
- [ ] Save resource names and URLs
- [ ] Note any configuration settings
- [ ] Capture screenshots for reference

---

## Option B: CI/CD Deployment

Use this option for automated, production-ready deployment.

### Step-by-Step CI/CD Setup

#### 1. Create CI/CD Issue for Copilot

- [ ] Go to GitHub Issues
- [ ] Create a new issue
- [ ] Use the template below

#### Issue Title
```
Create CI/CD workflow for Azure deployment
```

#### Issue Description

```markdown
@copilot Please create a complete CI/CD pipeline for deploying the migrated application to Azure.

## Requirements

### CI/CD Pipeline Should:

1. **Trigger on Push to Main Branch**
   - Automatically deploy when code is merged to main
   - Include manual trigger option

2. **Build the Application**
   - Build API project
   - Build scheduled task/function project
   - Run tests (if any)
   - Create deployment artifacts

3. **Deploy to Azure**
   - Deploy API to [specify service: Azure App Service / Azure Functions / Container Apps]
   - Deploy scheduled task to [specify service: Azure Functions / Container Apps]
   - Use Infrastructure as Code (Bicep/ARM/Terraform)

4. **Manage Azure Resources**
   - Create resource group if not exists
   - Create/update all required Azure resources
   - Configure application settings
   - Set environment variables and secrets

5. **Security Best Practices**
   - Use GitHub Secrets for sensitive data
   - Use Azure Service Principal or Managed Identity
   - No hardcoded credentials
   - Secure secret management

### Deliverables

Please provide:

- [ ] GitHub Actions workflow file (`.github/workflows/deploy.yml`)
- [ ] Infrastructure as Code files (Bicep/ARM templates)
- [ ] Documentation on:
  - Required GitHub Secrets to configure
  - Azure setup prerequisites
  - How to trigger deployment
  - How to verify deployment
  - Rollback procedures

### Azure Resources to Deploy

Based on the migration (customize this based on your actual migration):

- Resource Group
- [Azure Function App / App Service / Container Apps] for API
- [Azure Function App / Container Apps] for scheduled task
- Storage Account (if needed)
- Application Insights (for monitoring)

### Configuration Needed

- [ ] Application settings for API
- [ ] Timer schedule configuration (every minute: "0 */1 * * * *")
- [ ] API URL for scheduled task
- [ ] Any connection strings or secrets

## Additional Requirements

- Include environment-specific deployments (optional: dev, staging, prod)
- Add deployment status badge to README
- Provide troubleshooting guide for common CI/CD issues
- Include automated testing in pipeline (if applicable)

Thank you! 🚀
```

#### 2. Assign to Copilot

- [ ] Assign the issue to @copilot
- [ ] Add labels: `cicd`, `azure`, `automation`
- [ ] Submit the issue

#### 3. Wait for Copilot to Complete

Copilot will:
- [ ] Create GitHub Actions workflow
- [ ] Create Infrastructure as Code files
- [ ] Create deployment documentation
- [ ] Create a Pull Request with the CI/CD setup

This typically takes 5-15 minutes.

#### 4. Review the CI/CD Pull Request

- [ ] Review the workflow file (`.github/workflows/deploy.yml`)
- [ ] Review Infrastructure as Code files
- [ ] Review documentation
- [ ] Check for any required secrets

#### 5. Configure GitHub Secrets

Before merging, configure required secrets:

- [ ] Go to repository Settings > Secrets and variables > Actions
- [ ] Add required secrets (documented in the PR):

**Common Secrets:**
```
AZURE_CREDENTIALS          # Azure Service Principal
AZURE_SUBSCRIPTION_ID      # Your Azure subscription ID
AZURE_RESOURCE_GROUP       # Resource group name
AZURE_FUNCTIONAPP_NAME     # Function app name (if using)
AZURE_WEBAPP_NAME          # App service name (if using)
```

**How to Create Azure Service Principal:**
```bash
az ad sp create-for-rbac \
  --name "github-actions-sp" \
  --role contributor \
  --scopes /subscriptions/{subscription-id} \
  --sdk-auth
```

- [ ] Copy the JSON output
- [ ] Add as `AZURE_CREDENTIALS` secret in GitHub

#### 6. Merge the CI/CD Pull Request

- [ ] Review all changes
- [ ] Ensure secrets are configured
- [ ] Merge the Pull Request
- [ ] Watch the deployment workflow run

#### 7. Monitor First Deployment

- [ ] Go to Actions tab
- [ ] Watch the workflow execution
- [ ] Check for any errors
- [ ] Verify deployment succeeds

#### 8. Verify Deployment

- [ ] Check Azure Portal for created resources
- [ ] Test the deployed API
- [ ] Verify scheduled task is running
- [ ] Check Application Insights

### CI/CD Workflow Example

Your workflow might look like this:

```yaml
name: Deploy to Azure

on:
  push:
    branches: [ main ]
  workflow_dispatch:

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '8.0.x'
      
      - name: Build
        run: dotnet build --configuration Release
      
      - name: Deploy Infrastructure
        uses: azure/arm-deploy@v1
        with:
          subscriptionId: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
          resourceGroupName: ${{ secrets.AZURE_RESOURCE_GROUP }}
          template: ./infra/main.bicep
      
      - name: Deploy Functions
        uses: Azure/functions-action@v1
        with:
          app-name: ${{ secrets.AZURE_FUNCTIONAPP_NAME }}
          package: './output'
```

### Triggering Deployments

After CI/CD is set up:

#### Automatic Deployment
- [ ] Merge any PR to main branch
- [ ] Workflow automatically triggers
- [ ] Application is deployed to Azure

#### Manual Deployment
- [ ] Go to Actions tab
- [ ] Select the deployment workflow
- [ ] Click "Run workflow"
- [ ] Choose branch and run

### CI/CD Verification Checklist

- [ ] GitHub Actions workflow file exists
- [ ] Infrastructure as Code files are present
- [ ] GitHub Secrets are configured
- [ ] First deployment succeeded
- [ ] Resources created in Azure
- [ ] API is accessible
- [ ] Scheduled task is running
- [ ] Future commits trigger auto-deployment

---

## Post-Deployment Tasks

Regardless of deployment method:

### 1. Monitor the Application

- [ ] **Set up Application Insights** (if not already done)
  ```bash
  az monitor app-insights component create \
    --app [app-insights-name] \
    --location eastus \
    --resource-group rg-migration-workshop
  ```

- [ ] **Configure alerts**
  - Set up alerts for failures
  - Monitor execution count
  - Track API response times

- [ ] **Check logs regularly**
  ```bash
  # Stream logs
  az webapp log tail --name [app-name] --resource-group rg-migration-workshop
  
  # Or use Azure Portal
  # Navigate to resource > Monitoring > Logs
  ```

### 2. Cost Management

- [ ] **Set up cost alerts**
  - Go to Azure Portal > Cost Management + Billing
  - Create budget alerts
  - Set spending limits

- [ ] **Monitor resource usage**
  - Check consumption metrics
  - Optimize if needed
  - Consider reserved instances for production

- [ ] **Review pricing tier**
  - Ensure appropriate tier selected
  - Scale down if over-provisioned

### 3. Security Hardening

- [ ] **Enable HTTPS only**
- [ ] **Configure authentication** (if needed)
- [ ] **Set up Key Vault for secrets** (if using sensitive data)
- [ ] **Review network security**
- [ ] **Enable logging and monitoring**

### 4. Documentation

- [ ] **Update README** with deployment info
- [ ] **Document environment variables**
- [ ] **Create runbook for common tasks**
- [ ] **Document troubleshooting steps**

---

## Verification & Testing

### Final Verification Checklist

#### Application Functionality
- [ ] API endpoint is accessible: `https://[your-app].azurewebsites.net/api/message`
- [ ] API returns correct response format
- [ ] Response includes timestamp and message
- [ ] Scheduled task executes every minute
- [ ] Task successfully calls the API
- [ ] Logs show successful executions

#### Infrastructure
- [ ] All Azure resources are created
- [ ] Resource group exists
- [ ] App Service/Function App is running
- [ ] Application Insights is collecting data
- [ ] No deployment errors

#### Security & Configuration
- [ ] HTTPS is enforced
- [ ] Environment variables are set
- [ ] Secrets are properly configured
- [ ] No sensitive data in code or logs

#### Monitoring & Observability
- [ ] Application Insights shows telemetry
- [ ] Logs are available and readable
- [ ] Alerts are configured
- [ ] Metrics are being collected

### Testing the Deployed Application

#### 1. Test API Endpoint

```bash
# Test the production API
curl https://[your-app].azurewebsites.net/api/message

# Expected response
{
  "message": "2024-01-15 14:30:45 - Hello World",
  "timestamp": "2024-01-15T14:30:45.123Z"
}
```

#### 2. Verify Scheduled Task

**Via Azure Portal:**
- [ ] Navigate to Function App (or Container App)
- [ ] Go to Functions > [TimerFunction] > Monitor
- [ ] Check invocation history
- [ ] Verify executions every minute
- [ ] Check success rate

**Via CLI:**
```bash
# Query Application Insights
az monitor app-insights query \
  --app [app-insights-name] \
  --analytics-query "traces | where message contains 'Timer' | top 10 by timestamp desc"
```

#### 3. Load Testing (Optional)

```bash
# Simple load test
for i in {1..100}; do
  curl https://[your-app].azurewebsites.net/api/message
done
```

---

## Troubleshooting Common Issues

### Issue: Deployment Failed

**Solutions:**
1. Check deployment logs in GitHub Actions or Azure Portal
2. Verify all secrets are configured
3. Ensure Azure Service Principal has correct permissions
4. Check resource naming conflicts

### Issue: API Returns 500 Error

**Solutions:**
1. Check Application Insights for exceptions
2. Review application logs: `az webapp log tail`
3. Verify environment variables are set
4. Check for missing dependencies

### Issue: Timer Function Not Executing

**Solutions:**
1. Check function app is running: `az functionapp show`
2. Verify timer trigger configuration
3. Check Application Insights for errors
4. Ensure function app has correct runtime
5. Verify storage account connection

### Issue: High Costs

**Solutions:**
1. Review consumption metrics
2. Adjust pricing tiers
3. Implement auto-scaling rules
4. Use consumption plans instead of dedicated

---

## Workshop Complete! 🎉

### What You've Accomplished

✅ **Learned GitHub Copilot capabilities**
   - Used Copilot as a pair programmer
   - Used Copilot as an autonomous team member

✅ **Completed application modernization**
   - Assessed modernization options
   - Migrated to cloud-native architecture
   - Deployed to Azure

✅ **Mastered the workflow**
   - Created issues for Copilot
   - Reviewed Copilot's work
   - Tested locally before deployment
   - Deployed to production

### Next Steps & Best Practices

#### Continue Learning
- [ ] Explore more Copilot features
- [ ] Try migrating other applications
- [ ] Learn advanced Azure services
- [ ] Experiment with different architectures

#### Improve the Application
- [ ] Add authentication and authorization
- [ ] Implement caching for better performance
- [ ] Add database integration
- [ ] Create a frontend UI
- [ ] Implement comprehensive monitoring

#### Share Your Success
- [ ] Document your experience
- [ ] Share learnings with your team
- [ ] Create internal workshops
- [ ] Contribute to community knowledge

---

## Cleanup (Optional)

If this was just for learning and you want to avoid costs:

### Delete Azure Resources

```bash
# Delete entire resource group (removes all resources)
az group delete \
  --name rg-migration-workshop \
  --yes --no-wait
```

### Clean Up GitHub

- [ ] Close all issues
- [ ] Archive the repository (if not needed)
- [ ] Delete branches (if not needed)

---

## Additional Resources

### Azure Resources
- [Azure Documentation](https://docs.microsoft.com/azure/)
- [Azure Functions Documentation](https://docs.microsoft.com/azure/azure-functions/)
- [Azure App Service Documentation](https://docs.microsoft.com/azure/app-service/)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)
- [Azure Architecture Center](https://docs.microsoft.com/azure/architecture/)

### GitHub Copilot Resources
- [GitHub Copilot Documentation](https://docs.github.com/copilot)
- [Copilot Best Practices](https://docs.github.com/copilot/using-github-copilot/best-practices-for-using-github-copilot)
- [Copilot for Business](https://docs.github.com/copilot/overview-of-github-copilot/about-github-copilot-business)

### CI/CD Resources
- [GitHub Actions Documentation](https://docs.github.com/actions)
- [Azure Deployment with GitHub Actions](https://docs.microsoft.com/azure/developer/github/github-actions)
- [Infrastructure as Code Best Practices](https://docs.microsoft.com/azure/architecture/framework/devops/iac)

---

## Workshop Feedback

We'd love to hear about your experience:

- What worked well?
- What could be improved?
- Did you encounter any unexpected issues?
- How has this changed your view of GitHub Copilot?

---

## Thank You! 🙏

Congratulations on completing the GitHub Copilot Migration Workshop! You've successfully:

1. ✅ Used GitHub Copilot as an intelligent team member
2. ✅ Modernized a legacy .NET application
3. ✅ Deployed a cloud-native solution to Azure
4. ✅ Set up automated deployment pipelines (if you chose CI/CD)

**You're now ready to use GitHub Copilot for your own modernization projects!**

---

⬅️ [Back to Step 5](step-05-local-testing.md) | [Return to Introduction](step-00-introduction.md) 🏠
