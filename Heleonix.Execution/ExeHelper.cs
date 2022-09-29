// <copyright file="ExeHelper.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution
{
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
        /// <param name="exePath">Defines the path to executable.</param>
        /// <param name="arguments">Represents the command line arguments.</param>
        /// <param name="extractOutput">Defines whether to redirect and extract standard output and errors or not.</param>
        /// <param name="workingDirectory">The current working directory.
        /// Relative paths inside the executable will be relative to this working directory.</param>
        /// <param name="waitForExit">A number of millisecoonds to wait for process ending.
        /// Use <see cref="int.MaxValue"/> to wait infinitely.
        /// </param>
        /// <returns>An exit result.</returns>
        /// <exception cref="InvalidOperationException">See the inner exception for details.</exception>
        /// <example>
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
#pragma warning disable SG0001 // Potential command injection with Process.Start
                        Arguments = arguments,
                        FileName = exePath,
#pragma warning restore SG0001 // Potential command injection with Process.Start
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = workingDirectory,
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
        /// Executes an executable by the specified path. Does not extract output and error streams.
        /// </summary>
        /// <param name="exePath">The execute path.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="workingDirectory">The working directory.</param>
        /// <returns>An executable's exit code.</returns>
        /// <exception cref="InvalidOperationException">See the inner exception for details.</exception>
        public static int Execute(string exePath, string arguments, string workingDirectory = "")
            => Execute(exePath, arguments, false, workingDirectory, int.MaxValue).ExitCode;
    }
}
