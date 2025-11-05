# .NET 8.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 8.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 8.0 upgrade.
3. Upgrade MessageService\MessageService.csproj
4. Upgrade GreetingsConsole\GreetingsConsole.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

No projects are excluded from this upgrade.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                | Current Version | New Version | Description                                   |
|:--------------------------------------------|:---------------:|:-----------:|:----------------------------------------------|
| Microsoft.AspNet.Cors                      | 5.2.9           |             | Package functionality included with framework |
| Microsoft.AspNet.WebApi                    | 5.2.9           |             | Package functionality included with framework |
| Microsoft.AspNet.WebApi.Core               | 5.2.9           |             | No supported version found                    |
| Microsoft.AspNet.WebApi.Cors               | 5.2.9           |             | No supported version found                    |
| Microsoft.AspNet.WebApi.WebHost            | 5.2.9           |             | No supported version found                    |
| Microsoft.CodeDom.Providers.DotNetCompilerPlatform | 4.1.0   |             | Package functionality included with framework |
| Microsoft.Web.Infrastructure               | 2.0.0           |             | Package functionality included with framework |
| Newtonsoft.Json                            | 13.0.3          | 13.0.4      | Recommended for .NET 8.0                     |
| Swashbuckle.Core                           | 5.6.0           |             | No supported version found                    |
| WebActivatorEx                             | 2.2.0           |             | No supported version found                    |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### MessageService\MessageService.csproj modifications

Project properties changes:
  - Target framework should be changed from `net48` to `net8.0`
  - Project file needs to be converted to SDK-style

NuGet packages changes:
  - Microsoft.AspNet.Cors should be removed (*package functionality included with framework*)
  - Microsoft.AspNet.WebApi should be removed (*package functionality included with framework*)
  - Microsoft.AspNet.WebApi.Core should be removed (*no supported version found*)
  - Microsoft.AspNet.WebApi.Cors should be removed (*no supported version found*)
  - Microsoft.AspNet.WebApi.WebHost should be removed (*no supported version found*)
  - Microsoft.CodeDom.Providers.DotNetCompilerPlatform should be removed (*package functionality included with framework*)
  - Microsoft.Web.Infrastructure should be removed (*package functionality included with framework*)
  - Newtonsoft.Json should be updated from `13.0.3` to `13.0.4` (*recommended for .NET 8.0*)
  - Swashbuckle.Core should be removed (*no supported version found*)
  - WebActivatorEx should be removed (*no supported version found*)

Feature upgrades:
  - Convert application initialization code from Global.asax.cs to .NET Core and clean up Global.asax.cs

#### GreetingsConsole\GreetingsConsole.csproj modifications

Project properties changes:
  - Target framework should be changed from `net48` to `net8.0`
  - Project file needs to be converted to SDK-style

NuGet packages changes:
  - Newtonsoft.Json should be updated from `13.0.3` to `13.0.4` (*recommended for .NET 8.0*)