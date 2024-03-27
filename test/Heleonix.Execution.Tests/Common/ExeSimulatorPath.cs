// <copyright file="ExeSimulatorPath.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution.Tests.Common;

using System.IO;
using System.Reflection;

/// <summary>
/// The exe simulator paths.
/// </summary>
public static class ExeSimulatorPath
{
    /// <summary>
    /// The current directory.
    /// </summary>
    private static readonly string CurrentDir =
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace("file:///", string.Empty))
            .TrimEnd(Path.DirectorySeparatorChar);

    /// <summary>
    /// Gets the project directory path.
    /// </summary>
    public static string ExePath => Path.Combine(CurrentDir, "Heleonix.Execution.Tests.ExeSimulator.exe");
}