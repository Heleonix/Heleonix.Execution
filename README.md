# Heleonix.Execution

Provides a command line arguments builder, methods to run executables and extract outputs, etc.

## Install

https://www.nuget.org/packages/Heleonix.Execution

## Documentation

### Heleonix.Execution.ArgsBuilder

Helps to build command line arguments to be passed in to executables.
Each method accepts a `condition` argument to determine whether to add an argument or not, and returns `this` instance
of the `ArgsBuilder` for chaining.

```csharp
using System.Diagnostics;
using Heleonix.Execution;

public static class Program
{
    public static void Main()
    {
        OpenChrome(true, true);

        OpenChrome(false, false);
    }

    public static void OpenChrome(bool isNewWindow, bool isSized)
    {
        var args = ArgsBuilder.By("--", "=")
            .AddPath("app", "http://www.google.com")
            .AddArgument("window-size", "300,300", isSized)
            .AddKey("new-window", isNewWindow);

        // Depending on conditions, command line arguments can be like:
        // "--app="http://www.google.com" --window-size=300,300 --new-window"
        // "--app="http://www.google.com" --window-size=300,300"
        // "--app="http://www.google.com" --new-window"
        // "--app="http://www.google.com""

        using (var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                Arguments = args // implicit type casting to string
            }
        })
        {
            process.Start();
        }
    }
}
```

#### Methods

* `public static ArgsBuilder By(string keyPrefix, string keyValueSeparator, string valueSeparator = ";", string pathWrapper = "\"", string argsSeparator = " ")`

  Creates a new instance of the `ArgsBuilder` with specified parameters.

  ##### Parameters

  * `keyPrefix`: Prepends each key.

  * `keyValueSeparator`: Separates keys and values.

    ```csharp
    var args = ArgsBuilder.By(keyPrefix: "--", keyValueSeparator: "=")
        .Addkey("key1")
        .AddArgument("arg1", "value1");

    // args: "--key1 --arg1=value1"
    ```

  * `valueSeparator`: Separates multiple values or paths under a single key.

    ```csharp
    var args = ArgsBuilder.By(keyPrefix: "--", keyValueSeparator: "=", valueSeparator: ",")
        .AddValues(new[] { "value1", "value2", "value3" });

    // args: "value1,value2,value3"
    ```

  * `pathWrapper`: Wraps paths using `AddPath` or `AddPaths` when paths have spaces etc.

    ```csharp
    var args = ArgsBuilder.By(keyPrefix: "--", keyValueSeparator: "=", valueSeparator: ",", pathWrapper: "'")
        .AddPath("myPath", "C:\My Folder With Spaces\my file.txt");

    // args: "--myPath="C:\My Folder With Spaces\my file.txt""
    ```

  * `argsSeparator`: Separates key/value pairs (arguments).

    ```csharp
    var args = ArgsBuilder.By(keyPrefix: "--", keyValueSeparator: "=", valueSeparator: ",", pathWrapper: "'", argsSeparator: ";")
        .AddArgument("arg1", "value1")
        .AddArgument("arg2", "value2");

    // args: "--arg1=value1;--arg2=value2"
    ```

  ##### Returns

  A newly created `ArgsBuilder` instance.

* `public ArgsBuilder AddKey(string key, bool condition = true)`

  Adds the specified key.

  ##### Example

  ```csharp
  var canAddKey = true;

  var args = ArgsBuilder.By("--", "=").AddKey("key1", canAddKey);

  // args: "--key1"
  ```

* `public ArgsBuilder AddKeys(IEnumerable<string> keys, bool condition = true)`

  Adds the specified list of keys.

  ##### Example

  ```csharp
  var canAddKeys = true;

  var args = ArgsBuilder.By("--", "=").AddKeys(new [] { "key1", "key2" }, canAddKeys);

  // args: "--key1 --key2"
  ```

- `public ArgsBuilder AddValue(object value, bool condition = true)`

  Adds the specified value.

  ##### Example

  ```csharp
  var canAddValue = true;

  var args = ArgsBuilder.By("--", "=").AddValue("12345", canAddValue);

  // args: "12345"
  ```

* `public ArgsBuilder AddValues(IEnumerable<object> values, bool condition = true)`

  Adds the specified list of values.

  ##### Example

  ```csharp
  var canAddValues = true;

  var args = ArgsBuilder.By("--", "=").AddValues(new [] { "111", "222", canAddValues);

  // args: "111 222"
  ```

* `public ArgsBuilder AddArgument(string key, object value, bool condition = true)`

  Adds the specified key and value.

  ##### Example

  ```csharp
  var canAddArgument = true;

  var args = ArgsBuilder.By("--", "=").AddArgument("arg1", "value1", canAddArgument);

  // args: "--arg1=value1"
  ```

* `public ArgsBuilder AddArguments(string key, IEnumerable<object> values, bool multipleTimes = false, bool condition = true)`

  Adds the specified list of values with the specified key, repeating key/value pair if `multipleTimes` is `true`.

  ##### Example

  ```csharp
  var canAddArguments = true;

  var args = ArgsBuilder.By("--", "=", valueSeparator: ",").AddArguments("key", new[] { "111", "222", false, canAddArguments);

  var multipleArgs = ArgsBuilder.By("--", "=").AddArguments("key", new[] { "111", "222", true, canAddArguments);

  // args: "--key=111,222"

  // multipleArgs: "--key=111 --key=222"
  ```

* `public ArgsBuilder AddPath(string path, bool condition = true)`

  Adds the specified path.

  ##### Example

  ```csharp
  var canAddPath = true;

  var args = ArgsBuilder.By("--", "=").AddPath("C:\My Folder\my file.txt", canAddPath);

  // args: ""C:\My Folder\my file.txt""
  ```

* `public ArgsBuilder AddPath(string key, string path, bool condition = true)`

  Adds the specified path with the specified key.

  ##### Example

  ```csharp
  var canAddPath = true;

  var args = ArgsBuilder.By("--", "=").AddPath("key", "C:\My Folder\my file.txt", canAddPath);

  // args: "--key="C:\My Folder\my file.txt""
  ```

* `public ArgsBuilder AddPaths(IEnumerable<string> paths, bool condition = true)`

  Adds the specified list of paths.

  ##### Example

  ```csharp
  var canAddPaths = true;

  var args = ArgsBuilder.By("--", "=").AddPaths(new [] { "C:\my file 1.txt", "C:\my file 2.txt", canAddPaths);

  // args: ""C:\my file 1.txt" "C:\my file 2.txt""
  ```

* `public ArgsBuilder AddPaths(string key, IEnumerable<string> paths, bool multipleTimes = false, bool condition = true)`

  Adds the specified list of values with the specified key, repeating key/value pair if `multipleTimes` is `true`.

  ##### Example

  ```csharp
  var canAddPaths = true;

  var args = ArgsBuilder.By("--", "=", valueSeparator: ",").AddPaths("key", new[] { "C:\my file 1.txt", "C:\my file 2.txt", false, canAddPaths);

  var multipleArgs = ArgsBuilder.By("--", "=").AddPaths("key", new[] { "C:\my file 1.txt", "C:\my file 2.txt", true, canAddPaths);

  // args: "--key="C:\my file 1.txt","C:\my file 2.txt""

  // multipleArgs: "--key="C:\my file 1.txt" --key="C:\my file 2.txt""
  ```

* `public static implicit operator string(ArgsBuilder builder) => builder?.ToString();`

  Performs an implicit conversion from `ArgsBuilder` to `string`.

### Heleonix.Execution.ExeHelper

Provides functionality for working with executables.

#### Methods

* `public static ExeResult Execute(string exePath, string arguments, bool extractOutput, string workingDirectory = "", int waitForExit = int.MaxValue)`

  Executes an executable by the specified path.

  ##### Parameters

  * `exePath`: Sefineds the path to executable.

  * `arguments`: Represents the command line arguments.

  * `extractOutput`: Defines whether to redirect and extract standard output and errors or not.

  * `workingDirectory`: The current working directory. Relative paths inside the executable will be relative to this working directory.

  * `waitForExit`: A number of millisecoonds to wait for process ending. Use `int.MaxValue` to wait infinitely.

  ##### Returns

  An instance of the `ExeResult` class representing the exit result.

  ##### Example

  ```csharp
  var result = ExeHelper.Execute(
      @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
      "--app=http://www.google.com --window-size=300,300 --new-window",
      true,
      string.Empty,
      2000
  );

  Console.WriteLine(result.ExitCode); // An exit code: value returned by `Main` or by `Environment.Exit(exitCode)` etc.
  Console.WriteLine(result.Output); // Output like `Console.WriteLine` is available here
  Console.WriteLine(result.Error); // Output like `Console.Error.WriteLine` is available here
  ```

* `public static int Execute(string exePath, string arguments, string workingDirectory = "")`

  Executes an executable by the specified path. Does not extract output and error streams.

  ##### Returns

  An executable's exit code.

### Heleonix.Execution.ExeResult

Represents a result of execution of an executable.

#### Properties

* `public int ExitCode { get; set; }`

  Provides an exit code: value returned by `Main` or by `Environment.Exit(exitCode)` etc.

* `public string Output { get; set; }`

  Provides executable's output like `Console.WriteLine` etc.

* `public string Error { get; set; }`

  Provides executable's error output like `Console.Error.WriteLine` etc.