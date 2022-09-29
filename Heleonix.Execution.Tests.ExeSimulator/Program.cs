﻿// <copyright file="Program.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution.Tests.ExeSimulator
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Exit code.</returns>
        public static int Main(string[] args)
        {
            var arguments = args.Select(arg => arg.Split('=')).ToDictionary(kvp => kvp[0], kvp => kvp[1]);

            if (arguments.ContainsKey("WriteOutput"))
            {
                Console.WriteLine(Environment.CurrentDirectory);
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                Console.WriteLine("-output-");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                Console.Error.WriteLine("-error-");
            }

            return Convert.ToInt32(arguments["ExitCode"], CultureInfo.InvariantCulture);
        }
    }
}