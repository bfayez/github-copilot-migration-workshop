# GitHub Copilot Migration Workshop

Welcome to the GitHub Copilot Migration Workshop! This workshop demonstrates how to use GitHub Copilot not just as a coding assistant, but as an autonomous team member that can handle complete application modernization tasks.

## 🎯 Workshop Overview

Learn to leverage GitHub Copilot to modernize a legacy .NET Framework application and migrate it to Azure cloud services. You'll experience firsthand how Copilot can analyze, plan, implement, and document complex migration projects.

### What You'll Learn

- 📝 How to use GitHub Copilot as a pair programmer in VS Code
- 🤖 How to assign tasks to GitHub Copilot as a team member
- 🔄 Application modernization strategies and best practices
- ☁️ Migrating legacy applications to Azure
- 🧪 Testing cloud applications locally with GitHub Codespaces
- 🚀 Deploying to Azure with manual or automated CI/CD approaches

### Workshop Outcomes

By completing this workshop, you will:

1. ✅ Understand the two modes of using GitHub Copilot
2. ✅ Have a modernization assessment with multiple options and trade-offs
3. ✅ Successfully migrate a .NET Framework app to modern cloud-native architecture
4. ✅ Deploy a working application to Azure
5. ✅ (Optional) Set up automated CI/CD pipelines

## 📋 Prerequisites

Before starting the workshop:

- [ ] GitHub account with Copilot access enabled
- [ ] Access to this repository (fork or clone)
- [ ] Basic understanding of .NET applications
- [ ] Azure subscription (free tier works fine)
- [ ] GitHub Codespaces available OR local development environment

## 🏗️ Current Application Architecture

The workshop starts with a legacy application consisting of:

### Components

1. **MessageService** - REST API (ASP.NET Web API 2, .NET Framework 4.8.1)
   - Endpoint: `GET /api/message`
   - Returns timestamped greeting messages
   - Runs on IIS/IIS Express

2. **GreetingsConsole** - Console Application (.NET Framework 4.8.1)
   - Calls the MessageService API
   - Displays greeting messages
   - **Scheduled to run every minute** (important for migration)

### Current Limitations

- Windows-only (.NET Framework 4.8.1)
- Requires IIS for hosting
- Not cloud-native
- Manual scaling
- Legacy deployment model

## 📚 Workshop Steps

Follow these steps in order to complete the workshop:

### [Step 0: Introduction](step-00-introduction.md)
**Time: 10 minutes**

- [ ] Understand GitHub Copilot's capabilities
- [ ] Learn about Copilot as a pair programmer
- [ ] Learn about Copilot as a team member
- [ ] Review the current application
- [ ] Understand workshop objectives

### [Step 1: Create Assessment Issue](step-01-create-assessment-issue.md)
**Time: 15 minutes**

- [ ] Create a GitHub issue for modernization assessment
- [ ] Write a detailed prompt for Copilot
- [ ] Assign the issue to @copilot
- [ ] Wait for Copilot to analyze and recommend approaches
- [ ] Review multiple options with pros/cons

### [Step 2: Review Assessment](step-02-review-assessment.md)
**Time: 15 minutes**

- [ ] Review Copilot's modernization recommendations
- [ ] Understand the suggested architecture
- [ ] Compare alternative approaches
- [ ] Evaluate advantages and disadvantages
- [ ] Choose the best approach for your needs
- [ ] Ask follow-up questions if needed

### [Step 3: Create Migration Issue](step-03-create-migration-issue.md)
**Time: 20 minutes**

- [ ] Create a new issue for implementing the migration
- [ ] Request step-by-step deployment instructions
- [ ] Assign the issue to @copilot
- [ ] Wait for Copilot to implement the migration
- [ ] Copilot creates a Pull Request with the migrated code

### [Step 4: Review Migration Work](step-04-review-migration.md)
**Time: 20 minutes**

- [ ] Review the Pull Request created by Copilot
- [ ] Understand the code changes
- [ ] Review the new architecture
- [ ] Check deployment instructions
- [ ] Verify local testing guide is included
- [ ] Ask questions if anything is unclear

### [Step 5: Local Testing in Codespaces](step-05-local-testing.md)
**Time: 30 minutes**

- [ ] Open GitHub Codespace from the PR
- [ ] Install required dependencies
- [ ] Configure local settings
- [ ] Run the migrated API locally
- [ ] Test the scheduled task locally
- [ ] Verify all functionality works
- [ ] Merge the PR if tests pass

### [Step 6: Azure Deployment](step-06-deployment.md)
**Time: 30-60 minutes**

Choose one of two paths:

#### Option A: Manual Deployment
- [ ] Follow step-by-step deployment instructions
- [ ] Create Azure resources manually
- [ ] Deploy the application
- [ ] Verify deployment
- [ ] Monitor the application

#### Option B: CI/CD Deployment (Recommended)
- [ ] Create an issue for Copilot to set up CI/CD
- [ ] Review and merge the CI/CD Pull Request
- [ ] Configure GitHub Secrets
- [ ] Trigger automated deployment
- [ ] Monitor deployment pipeline

## ⏱️ Time Estimate

- **Core Workshop**: 2-3 hours
- **With CI/CD Setup**: 3-4 hours
- **Advanced Exploration**: Additional time as needed

## 🚀 Quick Start

Ready to begin? Here's your path:

1. **Start Here**: [Step 0 - Introduction](step-00-introduction.md)
2. Follow the steps sequentially
3. Check off items as you complete them
4. Don't skip steps - they build on each other

## 📊 Workshop Progress Tracker

Use this to track your overall progress:

- [ ] **Step 0**: Introduction completed
- [ ] **Step 1**: Assessment issue created and Copilot responded
- [ ] **Step 2**: Assessment reviewed and approach selected
- [ ] **Step 3**: Migration issue created and PR received
- [ ] **Step 4**: Migration work reviewed
- [ ] **Step 5**: Local testing completed and PR merged
- [ ] **Step 6**: Application deployed to Azure
- [ ] **Bonus**: CI/CD pipeline set up (optional)

## 🎓 Learning Objectives

### Technical Skills

After completing this workshop, you'll know how to:

- Create effective prompts for GitHub Copilot
- Use Copilot for complex, multi-step tasks
- Modernize .NET Framework applications
- Deploy applications to Azure
- Test cloud applications locally
- Set up CI/CD pipelines
- Use Infrastructure as Code

### GitHub Copilot Skills

You'll master:

- Assigning issues to Copilot
- Reviewing Copilot's work
- Asking follow-up questions
- Iterating with Copilot
- Accepting or requesting changes
- Integrating Copilot into team workflows

## 🛠️ Tools & Technologies Used

### Development Tools
- GitHub Copilot
- GitHub Codespaces
- Visual Studio Code
- Git & GitHub

### Technologies
- .NET (Framework → Modern .NET)
- Azure Functions
- Azure App Service (or alternatives)
- Azure Container Apps (possibly)
- Docker (possibly)

### Azure Services (depends on migration path)
- Azure Functions
- Azure App Service
- Azure Container Apps
- Application Insights
- Azure Key Vault
- Azure Resource Manager

## 📖 Additional Resources

### GitHub Copilot
- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)
- [Copilot Best Practices](https://docs.github.com/en/copilot/using-github-copilot/best-practices-for-using-github-copilot)
- [Copilot Workspace](https://githubnext.com/projects/copilot-workspace)

### Azure Migration
- [Azure Migration Center](https://azure.microsoft.com/migration/)
- [.NET Application Migration](https://dotnet.microsoft.com/learn/azure/architecture)
- [Azure Architecture Center](https://docs.microsoft.com/azure/architecture/)

### .NET Modernization
- [.NET Upgrade Assistant](https://dotnet.microsoft.com/platform/upgrade-assistant)
- [Migrating to Modern .NET](https://docs.microsoft.com/dotnet/core/porting/)
- [.NET Migration Guide](https://docs.microsoft.com/dotnet/architecture/modernize-with-azure-containers/)

## 💡 Tips for Success

### Working with Copilot

1. **Be Specific**: The more details you provide, the better Copilot's recommendations
2. **One Task at a Time**: Don't assign multiple issues to Copilot simultaneously
3. **Review Carefully**: Always review Copilot's work before merging
4. **Ask Questions**: Don't hesitate to ask Copilot for clarification
5. **Iterate**: It's okay to request changes or improvements

### Workshop Best Practices

1. **Follow the Steps**: Each step builds on the previous one
2. **Use Checkboxes**: Track your progress with the provided checklists
3. **Test Locally First**: Always test in Codespaces before Azure deployment
4. **Document**: Take notes on what you learn
5. **Experiment**: Try different approaches and see what works best

## 🐛 Troubleshooting

### Common Issues

**Copilot doesn't respond to issue:**
- Ensure Copilot is properly assigned (@copilot)
- Verify Copilot is enabled for your repository
- Wait a few minutes - complex tasks take time

**Local testing fails:**
- Check all dependencies are installed
- Verify configuration files are correct
- Review logs for specific errors
- Consult the troubleshooting section in Step 5

**Deployment fails:**
- Verify Azure credentials are correct
- Check GitHub Secrets are configured
- Review deployment logs
- Ensure resource names are unique
- Check Azure subscription limits

**Need Help?**
- Review the troubleshooting sections in each step
- Check the [GitHub Copilot documentation](https://docs.github.com/en/copilot)
- Review Azure service documentation
- Ask Copilot for help in the issues

## 🎯 Success Criteria

You'll know you've successfully completed the workshop when:

1. ✅ You understand how to use Copilot as a team member
2. ✅ Copilot provided a thorough modernization assessment
3. ✅ The application is successfully migrated to modern architecture
4. ✅ Local testing confirms all functionality works
5. ✅ The application is deployed and running in Azure
6. ✅ (Optional) CI/CD pipeline is set up and working

## 🏆 Next Steps After the Workshop

### Expand Your Knowledge
- Migrate another application using Copilot
- Try different Azure services
- Explore advanced Copilot features
- Learn more about cloud architecture patterns

### Apply to Real Projects
- Use Copilot for your actual modernization projects
- Introduce Copilot to your team
- Create custom prompts for your organization
- Share learnings with your colleagues

### Contribute
- Improve this workshop with your feedback
- Share your success stories
- Help others learn
- Create your own workshops

## 📝 Workshop Feedback

After completing the workshop, please consider:

- What worked well?
- What could be improved?
- What challenges did you face?
- How will you use Copilot in your work?
- Would you recommend this workshop to others?

## 📜 License

This workshop is part of the GitHub Copilot Migration Workshop repository.

## 🙏 Acknowledgments

This workshop demonstrates the power of GitHub Copilot as an AI pair programmer and autonomous team member for application modernization and cloud migration.

---

## Ready to Start?

Begin your journey here:

➡️ **[Start with Step 0: Introduction](step-00-introduction.md)**

Good luck, and enjoy learning how to work with GitHub Copilot! 🚀

---

## Quick Navigation

- [Step 0: Introduction](step-00-introduction.md)
- [Step 1: Create Assessment Issue](step-01-create-assessment-issue.md)
- [Step 2: Review Assessment](step-02-review-assessment.md)
- [Step 3: Create Migration Issue](step-03-create-migration-issue.md)
- [Step 4: Review Migration Work](step-04-review-migration.md)
- [Step 5: Local Testing](step-05-local-testing.md)
- [Step 6: Azure Deployment](step-06-deployment.md)
