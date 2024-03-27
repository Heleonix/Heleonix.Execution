# Heleonix.Execution

[![Release: .NET / NuGet](https://github.com/Heleonix/Heleonix.Execution/actions/workflows/release-net-nuget.yml/badge.svg)](https://github.com/Heleonix/Heleonix.Execution/actions/workflows/release-net-nuget.yml)

Provides a command line arguments builder, methods to run executables and extract outputs, etc.

## Install

https://www.nuget.org/packages/Heleonix.Execution

## Documentation

See [Heleonix.Execution](https://heleonix.github.io/docs/Heleonix.Execution)

## Examples

### ExeHelper

```csharp
var result = ExeHelper.Execute(
    @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
    "--app=http://www.google.com --window-size=300,300 --new-window",
    true,
    string.Empty,
    2000);

Console.WriteLine(result.ExitCode); // An exit code: value returned by `Main` or by `Environment.Exit(exitCode)` etc.
Console.WriteLine(result.Output); // Output like `Console.WriteLine` is available here
Console.WriteLine(result.Error); // Output like `Console.Error.WriteLine` is available here.
```

### ArgsBuilder

```csharp
var canAddArguments = true;

var args = ArgsBuilder.By("--", "=", valueSeparator: ",").AddArguments("key", new[] { "111", "222", false, canAddArguments);

var multipleArgs = ArgsBuilder.By("--", "=").AddArguments("key", new[] { "111", "222", true, canAddArguments);

// args: "--key=111,222"

// multipleArgs: "--key=111 --key=222".
```

## Contribution Guideline

1. [Create a fork](https://github.com/Heleonix/Heleonix.Execution/fork) from the main repository
2. Implement whatever is needed
3. [Create a Pull Request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request-from-a-fork).
   Make sure the assigned [Checks](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/collaborating-on-repositories-with-code-quality-features/about-status-checks#checks) pass successfully.
   You can watch the progress in the [PR: .NET](https://github.com/Heleonix/Heleonix.Execution/actions/workflows/pr-net.yml) GitHub workflows
4. [Request review](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/requesting-a-pull-request-review) from the code owner
5. Once approved, merge your Pull Request via [Squash and merge](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/incorporating-changes-from-a-pull-request/about-pull-request-merges#squash-and-merge-your-commits)

   > **IMPORTANT**  
   > While merging, enter a [Conventional Commits](https://www.conventionalcommits.org/) commit message.
   > This commit message will be used in automatically generated [Github Release Notes](https://github.com/Heleonix/Heleonix.Execution/releases)
   > and [NuGet Release Notes](https://www.nuget.org/packages/Heleonix.Execution/#releasenotes-body-tab)

5. Monitor the [Release: .NET / NuGet](https://github.com/Heleonix/Heleonix.Execution/actions/workflows/release-net-nuget.yml)
   GitHub workflow to make sure your changes are delivered successfully
5. In case of any issues, please contact [heleonix.sln@gmail.com](mailto:heleonix.sln@gmail.com)