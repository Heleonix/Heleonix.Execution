// <copyright file="ExeSimulatorPath.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2017-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution.Tests.Common
{
    using System;
    using System.IO;

    /// <summary>
    /// The exe simulator paths.
    /// </summary>
    public static class ExeSimulatorPath
    {
        /// <summary>
        /// The current directory.
        /// </summary>
        private static readonly string CurrentDir =
            AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);

        /// <summary>
        /// Gets the project directory path.
        /// </summary>
        public static string ExePath => Path.Combine(
            CurrentDir,
            "..",
            "..",
            "..",
            "..",
            "Heleonix.Execution.Tests.ExeSimulator",
            "bin",
            Path.GetFileName(Path.GetDirectoryName(CurrentDir)),
            "net461",
            "Heleonix.Execution.Tests.ExeSimulator.exe");
    }
}