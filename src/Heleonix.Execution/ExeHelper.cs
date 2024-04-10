// <copyright file="ExeHelper.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution;

using System;
using System.Diagnostics;

/// <summary>
/// Provides functionality for working with executables.
/// </summary>
public static class ExeHelper
{
    /// <summary>
    /// Executes an executable by the specified <paramref name="exePath"/>.
    /// </summary>
    /// <param name="exePath">The path to the executable file to run.</param>
    /// <param name="arguments">Command line arguments to pass into the executable.</param>
    /// <param name="extractOutput">Defines whether to redirect and extract standard output and errors or not.</param>
    /// <param name="workingDirectory">The working directory to launch the executable in.</param>
    /// <param name="waitForExit">A number of millisecoonds to wait for the process ending.
    /// Use <see cref="int.MaxValue"/> to wait infinitely.
    /// </param>
    /// <returns>An exit result.</returns>
    /// <exception cref="InvalidOperationException">See the inner exception for details.</exception>
    /// <example>
    /// <code>
    /// var result = ExeHelper.Execute(
    /// @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
    /// "--app=http://www.google.com --window-size=300,300 --new-window",
    /// true,
    /// string.Empty,
    /// 2000);
    ///
    /// Console.WriteLine(result.ExitCode); // An exit code: value returned by `Main` or by `Environment.Exit(exitCode)` etc.
    /// Console.WriteLine(result.Output); // Output like `Console.WriteLine` is available here
    /// Console.WriteLine(result.Error); // Output like `Console.Error.WriteLine` is available here.
    /// </code>
    /// </example>
    public static ExeResult Execute(
        string exePath,
        string arguments,
        bool extractOutput,
        string workingDirectory = "",
        int waitForExit = int.MaxValue)
    {
        try
        {
            using (var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = arguments,
                    WorkingDirectory = workingDirectory,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardOutput = extractOutput,
                    RedirectStandardError = extractOutput,
                },
            })
            {
                process.Start();

                var output = extractOutput ? process.StandardOutput.ReadToEnd() : null;

                var error = extractOutput ? process.StandardError.ReadToEnd() : null;

                process.WaitForExit(waitForExit);

                return new ExeResult
                {
                    ExitCode = process.ExitCode,
                    Output = output,
                    Error = error,
                };
            }
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message, e);
        }
    }

    /// <summary>
    /// Executes an executable by the specified <paramref name="exePath"/>. Does not extract output and error streams.
    /// </summary>
    /// <param name="exePath">The path to the executable file to run.</param>
    /// <param name="arguments">Command line arguments to pass into the executable.</param>
    /// <param name="workingDirectory">The working directory to launch the executable in.</param>
    /// <returns>The exit code of the executable.</returns>
    /// <exception cref="InvalidOperationException">See the inner exception for more details.</exception>
    public static int Execute(string exePath, string arguments, string workingDirectory = "")
        => Execute(exePath, arguments, false, workingDirectory, int.MaxValue).ExitCode;

    /// <summary>
    /// Executes an executable by the specified <paramref name="exePath"/>.
    /// Asynchronously forwards <see cref="Process.StandardOutput"/> and <see cref="Process.StandardError"/>
    /// of the executable to the specified <paramref name="outputWriter"/> and <paramref name="errorWriter"/> using
    /// the intermediate <c>char</c> buffer with the specified <paramref name="bufferSize"/>.
    /// </summary>
    /// <param name="exePath">The path to the executable file to run.</param>
    /// <param name="arguments">Command line arguments to pass into the executable.</param>
    /// <param name="outputWriter">The text writer to forward the standard output stream to.</param>
    /// <param name="errorWriter">The text writer to forward the standard error stream to.</param>
    /// <param name="workingDirectory">The working directory to launch the executable in.</param>
    /// <param name="waitForExit">A number of millisecoonds to wait for the process ending.
    /// Use <see cref="int.MaxValue"/> to wait infinitely.
    /// </param>
    /// <param name="bufferSize">The sizes of the intermediate buffers to use for forwarding in number of <c>char</c>.</param>
    /// <returns>The exit code of the executable.</returns>
    /// <exception cref="InvalidOperationException">See the inner exception for more details.</exception>
    /// <example>
    /// Launch an executable and forward its output and error streams while it is running.
    /// <code>
    /// var output = new StringWriter();
    /// var error = new StringWriter();
    ///
    /// var exitCode =  ExeHelper.Execute("dotnet.exe", "--UNKNOWN", output, error, string.Empty, 5000, 2048);
    ///
    /// var o = output.ToString();
    /// var e = error.ToString();
    /// </code>
    /// Launch an executable and forward its output and error streams to the <see cref="Console"/> of the main process.
    /// <code>
    /// var exitCode =  ExeHelper.Execute("dotnet.exe", "--UNKNOWN", Console.Out, Console.Error, string.Empty, 5000, 2048);
    /// </code>
    /// </example>
    public static int Execute(
        string exePath,
        string arguments,
        TextWriter outputWriter,
        TextWriter errorWriter,
        string workingDirectory = "",
        int waitForExit = int.MaxValue,
        int bufferSize = 4096)
    {
        try
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = exePath;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = workingDirectory;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();

                var outputRedirector = new StreamRedirector(process.StandardOutput, outputWriter, bufferSize);
                var errorRedirector = new StreamRedirector(process.StandardError, errorWriter, bufferSize);

                outputRedirector.Start();
                errorRedirector.Start();

                process.WaitForExit(waitForExit);

                return process.ExitCode;
            }
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message, e);
        }
    }
}
