// <copyright file="ExeResultTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2017-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution.Tests
{
    using Heleonix.Execution;
    using Heleonix.Testing.NUnit.Aaa;
    using NUnit.Framework;
    using static Heleonix.Testing.NUnit.Aaa.AaaSpec;

    /// <summary>
    /// Tests the <see cref="ExeResult"/>.
    /// </summary>
    [ComponentTest(Type = typeof(ExeResult))]
    public static class ExeResultTests
    {
        /// <summary>
        /// Tests the <see cref="ExeResult.ToString"/>.
        /// </summary>
        [MemberTest(Name = nameof(ExeResult.ToString))]
        public static void ToString1()
        {
            ExeResult exeResult = null;
            string result = null;

            Arrange(() =>
            {
                exeResult = new ExeResult();
            });

            Act(() =>
            {
                result = exeResult.ToString();
            });

            When("an exit code is provided", () =>
            {
                Arrange(() =>
                {
                    exeResult.ExitCode = 123;
                });

                Should("return a parameter name with the exit code", () =>
                {
                    Assert.That(result, Is.EqualTo("ExitCode: 123"));
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ExeResult.GetHashCode"/>.
        /// </summary>
        [MemberTest(Name = nameof(ExeResult.GetHashCode))]
        public static void GetHashCode1()
        {
            ExeResult exeResult = null;
            var result = 0;

            Arrange(() =>
            {
                exeResult = new ExeResult();
            });

            When("the hash code is retrieved", () =>
            {
                Act(() =>
                {
                    result = exeResult.GetHashCode();
                });

                Should("return a non-zero hash code", () =>
                {
                    Assert.That(result, Is.Not.Zero);
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ExeResult.Equals(object)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ExeResult.Equals))]
        public static void Equals1()
        {
            ExeResult exeResult1 = null;
            ExeResult exeResult2 = null;
            var exitCode1 = 0;
            var exitCode2 = 0;
            var areEqual = false;

            Arrange(() =>
            {
                exeResult1 = new ExeResult { ExitCode = exitCode1 };
                exeResult2 = new ExeResult { ExitCode = exitCode2 };
            });

            Act(() =>
            {
                areEqual = exeResult1.Equals(exeResult2);
            });

            When("the first exit code is 1", () =>
            {
                exitCode1 = 1;

                And("the second exit code is 1", () =>
                {
                    exitCode2 = 1;

                    Should("return true", () =>
                    {
                        Assert.That(areEqual, Is.True);
                    });
                });

                And("the second exit code is 2", () =>
                {
                    exitCode2 = 2;

                    Should("return false", () =>
                    {
                        Assert.That(areEqual, Is.False);
                    });
                });
            });
        }
    }
}