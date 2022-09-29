// <copyright file="ExeHelperTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution.Tests
{
    using System;
    using Heleonix.Execution;
    using Heleonix.Execution.Tests.Common;
    using Heleonix.Testing.NUnit.Aaa;
    using NUnit.Framework;
    using static Heleonix.Testing.NUnit.Aaa.AaaSpec;

    /// <summary>
    /// Tests the <see cref="ExeHelper"/>.
    /// </summary>
    [ComponentTest(Type = typeof(ExeHelper))]
    public static class ExeHelperTests
    {
        /// <summary>
        /// Tests the <see cref="ExeHelper.Execute(string,string,bool,string,int)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ExeHelper.Execute) + "(string,string,bool,string,int)")]
        public static void Execute1()
        {
            When("the method is executed", () =>
            {
                var extractOutput = false;
                ExeResult result = null;
                string exePath = null;
                Exception thrownException = null;

                Act(() =>
                {
                    try
                    {
                        result = ExeHelper.Execute(
                        exePath,
                        $"WriteOutput={extractOutput} ExitCode=1",
                        extractOutput,
                        string.Empty,
                        200);
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
                    {
                        thrownException = e;
                    }
                });

                And("executable file name is specified", () =>
                {
                    exePath = ExeSimulatorPath.ExePath;

                    And("output should be extracted", () =>
                    {
                        extractOutput = true;

                        Should("return result with extracted output", () =>
                        {
                            Assert.That(result.ExitCode, Is.EqualTo(1));
                            Assert.That(result.Error, Contains.Substring("-error-"));
                            Assert.That(result.Output, Contains.Substring("-output-"));
                        });
                    });

                    And("output should not be extracted", () =>
                    {
                        extractOutput = false;

                        Should("return result without extracted output", () =>
                        {
                            Assert.That(result.ExitCode, Is.EqualTo(1));
                            Assert.That(result.Error, Is.Null);
                            Assert.That(result.Output, Is.Null);
                        });
                    });
                });

                And("executable file name is not specified", () =>
                {
                    exePath = null;

                    Should("throw the InvalidOperationException", () =>
                    {
                        Assert.That(thrownException, Is.InstanceOf<InvalidOperationException>());
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ExeHelper.Execute(string,string,string)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ExeHelper.Execute) + "(string,string,string)")]
        public static void Execute2()
        {
            When("the method is executed", () =>
            {
                var result = 0;

                Act(() =>
                {
                    result = ExeHelper.Execute(
                        ExeSimulatorPath.ExePath,
                        $"WriteOutput=false ExitCode=0",
                        string.Empty);
                });

                Should("return success exit code", () =>
                {
                    Assert.That(result, Is.Zero);
                });
            });
        }
    }
}