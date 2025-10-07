# Step 5: Local Testing in GitHub Codespaces

## Objective
In this step, you'll test the migrated application locally using GitHub Codespaces to verify functionality **before deploying to Azure**.

### Step Progress Checklist
- [ ] Set up GitHub Codespace
- [ ] Install required dependencies
- [ ] Configure local settings
- [ ] Run the migrated API locally
- [ ] Test the API endpoint
- [ ] Run the scheduled task locally
- [ ] Verify functionality
- [ ] Merge the Pull Request if everything works

---

## Why Test Locally First?

Testing locally before Azure deployment:
- ✅ Verifies the code works correctly
- ✅ Identifies issues early (no Azure costs)
- ✅ Allows quick iterations and fixes
- ✅ Validates configuration
- ✅ Ensures you understand the application

---

## Setting Up GitHub Codespaces

### Option A: Open PR in Codespace

- [ ] Go to the Pull Request from Step 4
- [ ] Click the "Code" dropdown button
- [ ] Select "Codespaces" tab
- [ ] Click "Create codespace on [branch-name]"
- [ ] Wait for Codespace to initialize (1-3 minutes)

### Option B: Create Codespace from Repository

- [ ] Go to your repository main page
- [ ] Click the "Code" dropdown
- [ ] Select "Codespaces" tab
- [ ] Click "Create codespace on main" or the PR branch
- [ ] Wait for initialization

### Verify Codespace is Ready

- [ ] VS Code opens in your browser (or desktop if configured)
- [ ] Terminal is available at the bottom
- [ ] Files are visible in the explorer

---

## Installing Dependencies

The required dependencies depend on the migration approach. Follow the instructions from the PR documentation.

### Common Dependency Installations

#### For Azure Functions (.NET)

```bash
# Install Azure Functions Core Tools
npm install -g azure-functions-core-tools@4 --unsafe-perm true
```

- [ ] Run the command in the terminal
- [ ] Verify installation: `func --version`

#### For .NET Applications

```bash
# Verify .NET SDK is installed
dotnet --version

# Restore NuGet packages
dotnet restore
```

- [ ] Check .NET version (should be .NET 6, 7, or 8)
- [ ] Restore packages successfully

#### For Container-based Solutions

```bash
# Verify Docker is available
docker --version

# Build containers (if using Docker Compose)
docker-compose build
```

- [ ] Docker is installed in Codespace
- [ ] Containers build successfully

---

## Configuration Setup

### 1. Locate Configuration Files

Find configuration files in the repository:
- [ ] `local.settings.json` (for Azure Functions)
- [ ] `appsettings.Development.json` (for .NET APIs)
- [ ] `.env` (for environment variables)
- [ ] `docker-compose.yml` (for Docker)

### 2. Configure Local Settings

#### For Azure Functions

Edit `local.settings.json`:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "API_URL": "http://localhost:7071/api/message"
  }
}
```

- [ ] Update API_URL if needed
- [ ] Set any required environment variables

#### For .NET API

Edit `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

- [ ] Verify configuration is correct
- [ ] Add any custom settings

### 3. Configuration Checklist

- [ ] API endpoint URLs are configured
- [ ] Connection strings (if any) are set for local dev
- [ ] Feature flags are appropriate for local testing
- [ ] Logging is configured for debugging

---

## Running the Application Locally

Follow the specific instructions from the PR documentation. Here are common scenarios:

### Scenario 1: Azure Functions (HTTP + Timer)

#### Start the Function App

```bash
# Navigate to the function project directory
cd [function-project-folder]

# Start the Functions runtime
func start
```

- [ ] Functions runtime starts successfully
- [ ] Note the local URLs displayed
- [ ] HTTP trigger URL shown (e.g., `http://localhost:7071/api/message`)
- [ ] Timer trigger registered

#### What You Should See

```
Azure Functions Core Tools
Core Tools Version:       4.x.x
Function Runtime Version: 4.x.x

Functions:
  HttpMessageFunction: [GET,POST] http://localhost:7071/api/message
  TimerGreetingFunction: timerTrigger

Host started
```

### Scenario 2: Separate API and Function

#### Terminal 1 - Start the API

```bash
# Navigate to API project
cd [api-project-folder]

# Run the API
dotnet run
```

- [ ] API starts successfully
- [ ] Note the URL (e.g., `http://localhost:5000`)

#### Terminal 2 - Start the Scheduled Task

```bash
# Navigate to function project
cd [function-project-folder]

# Update local.settings.json with API URL
# Then start functions
func start
```

- [ ] Timer function starts
- [ ] Function is configured to call the API

### Scenario 3: Docker Compose

```bash
# Start all services
docker-compose up

# Or run in detached mode
docker-compose up -d
```

- [ ] All containers start successfully
- [ ] Services are accessible

---

## Testing the API

### 1. Test Using Curl

```bash
# Test the API endpoint
curl http://localhost:7071/api/message

# Or if using different port
curl http://localhost:5000/api/message
```

**Expected Response:**
```json
{
  "message": "2024-01-15 14:30:45 - Hello World",
  "timestamp": "2024-01-15T14:30:45.123Z"
}
```

- [ ] API responds successfully
- [ ] Response format matches expected structure
- [ ] Timestamp is current
- [ ] Message includes greeting

### 2. Test Using Browser (if HTTP endpoint)

- [ ] Open the API URL in Codespace browser
- [ ] Codespace prompts to open port in browser
- [ ] Click "Open in Browser"
- [ ] API returns JSON response

### 3. Test Using VS Code REST Client (Optional)

Create a file `test.http`:

```http
### Test Message Endpoint
GET http://localhost:7071/api/message
```

- [ ] Install REST Client extension (if not installed)
- [ ] Click "Send Request"
- [ ] View response

---

## Testing the Scheduled Task

### Method 1: Observe Timer Trigger (Azure Functions)

If using Azure Functions with Timer Trigger:

- [ ] Watch the terminal output
- [ ] Timer should trigger every minute
- [ ] Look for log entries showing execution

**Expected Log Output:**
```
[2024-01-15T14:30:00.123] Executing 'TimerGreetingFunction'
[2024-01-15T14:30:00.456] Timer triggered at: 2024-01-15T14:30:00
[2024-01-15T14:30:00.789] Calling API: http://localhost:7071/api/message
[2024-01-15T14:30:01.123] Response: {"message":"...","timestamp":"..."}
[2024-01-15T14:30:01.456] Executed 'TimerGreetingFunction' (Succeeded)
```

### Method 2: Trigger Manually (Azure Functions)

For immediate testing without waiting:

```bash
# Trigger the timer function manually
curl -X POST http://localhost:7071/admin/functions/TimerGreetingFunction -H "Content-Type: application/json" -d "{}"
```

- [ ] Function executes immediately
- [ ] Check terminal for execution logs
- [ ] Verify API was called

### Method 3: Check Logs

```bash
# If using Docker Compose
docker-compose logs -f scheduler

# If using Functions
# Logs appear in the terminal where you ran 'func start'
```

- [ ] Logs show scheduled execution
- [ ] API calls are logged
- [ ] No errors in logs

---

## Verifying Functionality

### Complete Verification Checklist

#### API Functionality
- [ ] API starts without errors
- [ ] Endpoint responds to HTTP requests
- [ ] Response format is correct (JSON with message and timestamp)
- [ ] Timestamp is current and properly formatted
- [ ] Message contains the expected greeting

#### Scheduled Task Functionality
- [ ] Timer/scheduler starts without errors
- [ ] Task triggers every minute (or can be triggered manually)
- [ ] Task successfully calls the API
- [ ] Task logs the response
- [ ] No errors during execution

#### Configuration
- [ ] All environment variables are set correctly
- [ ] Local configuration works as expected
- [ ] No missing dependencies
- [ ] No connection errors

#### Code Quality
- [ ] No compilation errors
- [ ] No runtime errors
- [ ] Proper error handling
- [ ] Logging is working

---

## Troubleshooting Common Issues

### Issue: Functions Core Tools Not Found

**Solution:**
```bash
# Install globally
npm install -g azure-functions-core-tools@4 --unsafe-perm true

# Or use alternative installation
sudo apt-get update
sudo apt-get install azure-functions-core-tools-4
```

### Issue: Port Already in Use

**Solution:**
```bash
# Use a different port
func start --port 7072

# Or kill the process using the port
lsof -ti:7071 | xargs kill -9
```

### Issue: API Not Reachable from Timer Function

**Solution:**
- Check `local.settings.json` has correct API URL
- Use `http://localhost:[port]` not `https`
- Ensure API is running before starting timer
- Check firewall/network settings in Codespace

### Issue: Timer Not Triggering

**Solution:**
- Verify timer expression in code (e.g., `"0 */1 * * * *"`)
- Check `host.json` configuration
- Look for errors in function startup logs
- Try manual trigger to test function logic

### Issue: .NET SDK Version Mismatch

**Solution:**
```bash
# Check required version in .csproj
cat [project].csproj | grep TargetFramework

# Install required .NET SDK if needed
# Consult PR documentation for specific version
```

### Issue: Missing Dependencies

**Solution:**
```bash
# Restore .NET packages
dotnet restore

# Install npm packages if needed
npm install

# Pull Docker images
docker-compose pull
```

---

## Advanced Testing (Optional)

### Load Testing the API

```bash
# Simple load test with curl
for i in {1..10}; do
  curl http://localhost:7071/api/message
  echo ""
done
```

### Testing Error Scenarios

```bash
# Test with invalid endpoints
curl http://localhost:7071/api/invalid

# Test API when offline (stop API first)
# Then trigger timer function to see error handling
```

---

## Accepting the Work

### When Everything Works ✅

If all tests pass and functionality is verified:

#### 1. Document Test Results

- [ ] Create a comment on the PR with test results:

```markdown
## Local Testing Results ✅

### API Testing
- ✅ API starts successfully
- ✅ Endpoint responds correctly: `GET /api/message`
- ✅ Response format is correct
- ✅ Timestamp is accurate

### Scheduled Task Testing
- ✅ Timer triggers every minute
- ✅ Successfully calls API
- ✅ Logs responses correctly

### Configuration
- ✅ All settings configured correctly
- ✅ No missing dependencies

**Verdict**: All functionality verified. Ready to merge! 🚀
```

#### 2. Merge the Pull Request

- [ ] Go to the Pull Request page
- [ ] Review one final time
- [ ] Click "Merge pull request"
- [ ] Choose merge strategy (typically "Squash and merge" or "Create a merge commit")
- [ ] Confirm the merge
- [ ] Optionally delete the branch

#### 3. Verify Merge to Main

- [ ] PR is merged
- [ ] Branch is deleted (if you chose to)
- [ ] Changes are now in the main branch
- [ ] Issue is automatically closed (if properly linked)

---

## When Something Doesn't Work ❌

If you find issues:

### 1. Document the Issues

- [ ] Comment on the PR with specific issues:

```markdown
@copilot I found some issues during local testing:

1. **Issue**: [Describe the problem]
   - Expected: [What should happen]
   - Actual: [What actually happens]
   - Logs: [Relevant log output]

2. **Issue**: [Another problem]
   - Details...

Could you please fix these issues?
```

### 2. Wait for Fixes

- [ ] Copilot will update the PR with fixes
- [ ] Re-test once fixes are committed
- [ ] Repeat until all issues are resolved

### 3. Then Merge

- [ ] Once all issues are fixed and retested
- [ ] Follow the merge process above

---

## Post-Merge Checklist

After merging:

- [ ] Pull Request is merged
- [ ] Changes are in main branch
- [ ] Migration issue is closed
- [ ] Code is ready for deployment
- [ ] Documentation is updated

---

## Next Steps

Now that the code is tested and merged:

### Option A: Manual Deployment to Azure
➡️ **[Proceed to Step 6A: Manual Azure Deployment](step-06-deployment.md)**

### Option B: Automated CI/CD Deployment
➡️ **[Proceed to Step 6B: CI/CD Setup](step-06-deployment.md#option-b-cicd-deployment)**

---

## Key Takeaways

After this step, you have:

✅ Tested the migrated application locally
✅ Verified all functionality works correctly
✅ Identified and resolved any issues
✅ Merged the working code to main branch
✅ Validated the migration before Azure deployment

---

## Additional Resources

- [GitHub Codespaces Documentation](https://docs.github.com/codespaces)
- [Azure Functions Local Development](https://docs.microsoft.com/azure/azure-functions/functions-develop-local)
- [Docker Compose Documentation](https://docs.docker.com/compose/)
- [.NET Local Development](https://docs.microsoft.com/dotnet/core/tools/)

---

⬅️ [Back to Step 4](step-04-review-migration.md) | [Next: Azure Deployment](step-06-deployment.md) ➡️
