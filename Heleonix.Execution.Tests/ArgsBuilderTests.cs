// <copyright file="ArgsBuilderTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2017-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution.Tests
{
    using System.Collections.Generic;
    using Heleonix.Execution;
    using Heleonix.Testing.NUnit.Aaa;
    using NUnit.Framework;
    using static Heleonix.Testing.NUnit.Aaa.AaaSpec;

    /// <summary>
    /// Tests the <see cref="ArgsBuilder"/>.
    /// </summary>
    [ComponentTest(Type = typeof(ArgsBuilder))]
    public static class ArgsBuilderTests
    {
        /// <summary>
        /// Tests the <see cref="ArgsBuilder.By(string,string,string,string,string)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.By))]
        public static void By()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=", ";", "\"", " ");
            });

            When("added two paths and a key with a value", () =>
            {
                var condition = true;

                Act(() =>
                {
                    result = builder
                        .AddPaths("key1", new[] { "path1", "path2" }, false, condition)
                        .AddArgument("key2", "value2", condition)
                        .ToString();
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    Should("provide arguments with provided paths and the key with the value", () =>
                    {
                        Assert.That(result, Is.EqualTo("-key1=\"path1\";\"path2\" -key2=value2"));
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddKey(string,bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddKey))]
        public static void AddKey()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added a key", () =>
            {
                var condition = true;
                string key = null;

                Act(() =>
                {
                    result = builder.AddKey(key, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    And("the key is provided", () =>
                    {
                        key = "key1";

                        Should("provide the provided key", () =>
                        {
                            Assert.That(result, Is.EqualTo("-key1"));
                        });
                    });

                    And("the key is an empty string", () =>
                    {
                        key = string.Empty;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });

                    And("the key is null", () =>
                    {
                        key = null;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddKeys(IEnumerable{string},bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddKeys))]
        public static void AddKeys()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added two keys", () =>
            {
                var condition = true;
                string[] keys = null;

                Act(() =>
                {
                    result = builder.AddKeys(keys, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    And("keys are provided", () =>
                    {
                        keys = new[] { "key1", "key2" };

                        Should("provide the provided keys", () =>
                        {
                            Assert.That(result, Is.EqualTo("-key1 -key2"));
                        });
                    });

                    And("keys is null", () =>
                    {
                        keys = null;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddValue(object,bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddValue))]
        public static void AddValue()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added a value", () =>
            {
                var condition = true;
                string value = null;

                Act(() =>
                {
                    result = builder.AddValue(value, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    And("the value is provided", () =>
                    {
                        value = "value1";

                        Should("provide the provided value", () =>
                        {
                            Assert.That(result, Is.EqualTo(value));
                        });
                    });

                    And("the value is an empty string", () =>
                    {
                        value = string.Empty;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });

                    And("the value is null", () =>
                    {
                        value = null;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddValues(IEnumerable{object},bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddValues))]
        public static void AddValues()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added two values", () =>
            {
                var condition = true;
                string[] values = null;

                Act(() =>
                {
                    result = builder.AddValues(values, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    And("values are provided", () =>
                    {
                        values = new[] { "value1", "value2" };

                        Should("provide arguments with the provided values", () =>
                        {
                            Assert.That(result, Is.EqualTo("value1 value2"));
                        });
                    });

                    And("values is null", () =>
                    {
                        values = null;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddArgument(string,object,bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddArgument))]
        public static void AddArgument()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added an argument", () =>
            {
                var condition = true;
                string key = null;
                string value = null;

                Act(() =>
                {
                    result = builder.AddArgument(key, value, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    And("the key is provided", () =>
                    {
                        key = "key1";

                        And("the value is provided", () =>
                        {
                            value = "value1";

                            Should("provide the argument with the provided value", () =>
                            {
                                Assert.That(result, Is.EqualTo("-key1=value1"));
                            });
                        });

                        And("the value is an empty string", () =>
                        {
                            value = string.Empty;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the value is null", () =>
                        {
                            value = null;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the key is an empty string", () =>
                        {
                            key = string.Empty;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the key is null", () =>
                        {
                            key = null;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddArguments(string,IEnumerable{object},bool,bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddArguments))]
        public static void AddArguments()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added arguments", () =>
            {
                var condition = true;
                string key = null;
                string[] values = null;
                var multipleTimes = false;

                Act(() =>
                {
                    result = builder.AddArguments(key, values, multipleTimes, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    And("the key is provided", () =>
                    {
                        key = "key1";

                        And("the values are provided", () =>
                        {
                            values = new[] { "value1", "value2" };

                            Should("provide the argument with the provided key and values", () =>
                            {
                                Assert.That(result, Is.EqualTo("-key1=value1;value2"));
                            });
                        });

                        And("the key with values should be added multiple times", () =>
                        {
                            multipleTimes = true;

                            Should("provide the argument with the provided values with teir own keys", () =>
                            {
                                Assert.That(result, Is.EqualTo("-key1=value1 -key1=value2"));
                            });
                        });

                        And("the values is null", () =>
                        {
                            values = null;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the key is an empty string", () =>
                        {
                            key = string.Empty;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the key is null", () =>
                        {
                            key = null;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddPath(string,bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddPath) + "(string,bool)")]
        public static void AddPath1()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added path", () =>
            {
                var condition = true;
                var path = "path1";

                Act(() =>
                {
                    result = builder.AddPath(path, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    Should("provide the path", () =>
                    {
                        Assert.That(result, Is.EqualTo("\"path1\""));
                    });

                    And("the path is an empty string", () =>
                    {
                        path = string.Empty;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });

                    And("the path is null", () =>
                    {
                        path = null;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddPath(string,string,bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddPath) + "(string,string,bool)")]
        public static void AddPath2()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added a path", () =>
            {
                var condition = true;
                string key = null;
                string path = null;

                Act(() =>
                {
                    result = builder.AddPath(key, path, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    And("the key is provided", () =>
                    {
                        key = "key1";

                        And("the path is provided", () =>
                        {
                            path = "path1";

                            Should("provide the key with the provided path", () =>
                            {
                                Assert.That(result, Is.EqualTo("-key1=\"path1\""));
                            });
                        });

                        And("the path is an empty string", () =>
                        {
                            path = string.Empty;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the path is null", () =>
                        {
                            path = null;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the key is an empty string", () =>
                        {
                            key = string.Empty;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the key is null", () =>
                        {
                            key = null;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddPaths(IEnumerable{string},bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddPaths) + "(IEnumerable{string},bool)")]
        public static void AddPaths1()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added paths", () =>
            {
                var condition = true;
                var paths = new[] { "path1", "path2" };

                Act(() =>
                {
                    result = builder.AddPaths(paths, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    Should("provide the paths", () =>
                    {
                        Assert.That(result, Is.EqualTo("\"path1\" \"path2\""));
                    });

                    And("the paths is null", () =>
                    {
                        paths = null;

                        Should("provide an empty string", () =>
                        {
                            Assert.That(result, Is.Empty);
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.AddPaths(string,IEnumerable{string},bool,bool)"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.AddPaths) + "(string,IEnumerable{string},bool,bool)")]
        public static void AddPaths2()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("added paths", () =>
            {
                var condition = true;
                string key = null;
                string[] paths = null;
                var multipleTimes = false;

                Act(() =>
                {
                    result = builder.AddPaths(key, paths, multipleTimes, condition);
                });

                And("a condition is true", () =>
                {
                    condition = true;

                    And("the key is provided", () =>
                    {
                        key = "key1";

                        And("the paths are provided", () =>
                        {
                            paths = new[] { "path1", "path2" };

                            Should("provide the key with the provided paths", () =>
                            {
                                Assert.That(result, Is.EqualTo("-key1=\"path1\";\"path2\""));
                            });
                        });

                        And("the key with paths should be added multiple times", () =>
                        {
                            multipleTimes = true;

                            Should("provide the key with the provided paths with teir own keys", () =>
                            {
                                Assert.That(result, Is.EqualTo("-key1=\"path1\" -key1=\"path2\""));
                            });
                        });

                        And("the paths is null", () =>
                        {
                            paths = null;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the key is an empty string", () =>
                        {
                            key = string.Empty;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });

                        And("the key is null", () =>
                        {
                            key = null;

                            Should("provide an empty string", () =>
                            {
                                Assert.That(result, Is.Empty);
                            });
                        });
                    });
                });

                And("a condition is false", () =>
                {
                    condition = false;

                    Should("provide an empty string", () =>
                    {
                        Assert.That(result, Is.Empty);
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.op_Implicit"/>.
        /// </summary>
        [MemberTest(Name = "implicit string")]
        public static void OperatorString()
        {
            ArgsBuilder builder = null;
            string result = null;

            When("the builder is null", () =>
            {
                Act(() =>
                {
                    builder = null;

                    result = builder;
                });

                Should("provide null", () =>
                {
                    Assert.That(result, Is.Null);
                });

                And("the builder is not null and a value is added", () =>
                {
                    Act(() =>
                    {
                        builder = ArgsBuilder.By("-", "=");
                        builder.AddValue("value1");

                        result = builder;
                    });

                    Should("provide the added value", () =>
                    {
                        Assert.That(result, Is.EqualTo("value1"));
                    });
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="ArgsBuilder.ToString"/>.
        /// </summary>
        [MemberTest(Name = nameof(ArgsBuilder.ToString))]
        public static void ToString1()
        {
            ArgsBuilder builder = null;
            string result = null;

            Arrange(() =>
            {
                builder = ArgsBuilder.By("-", "=");
            });

            When("the builder is empty", () =>
            {
                Act(() =>
                {
                    builder.ToString();
                    builder.ToString();
                    result = builder.ToString();
                });

                Should("provide an empty string", () =>
                {
                    Assert.That(result, Is.Empty);
                });

                And("a value is added", () =>
                {
                    Act(() =>
                    {
                        builder.AddValue("value1");

                        builder.ToString();
                        builder.ToString();
                        result = builder.ToString();
                    });

                    Should("provide the added value", () =>
                    {
                        Assert.That(result, Is.EqualTo("value1"));
                    });
                });
            });
        }
    }
}