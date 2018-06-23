// <copyright file="ArgsBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2017-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Helps to build command line arguments to be passed in to executables.
    /// Each method accepts a <c>condition</c> argument to determine whether to add an argument or not,
    /// and returns <c>this</c> instance of the <see cref="ArgsBuilder"/> for chaining.
    /// </summary>
    public sealed class ArgsBuilder
    {
        /// <summary>
        /// The <see cref="StringBuilder"/>.
        /// </summary>
        private readonly StringBuilder builder = new StringBuilder();

        /// <summary>
        /// The key prefix.
        /// </summary>
        private readonly string keyPrefix;

        /// <summary>
        /// The key/value separator.
        /// </summary>
        private readonly string keyValueSeparator;

        /// <summary>
        /// The value separator.
        /// </summary>
        private readonly string valueSeparator;

        /// <summary>
        /// The path wrapper.
        /// </summary>
        private readonly string pathWrapper;

        /// <summary>
        /// The arguments separator.
        /// </summary>
        private readonly string argsSeparator;

        /// <summary>
        /// Determines whether this instance was modified.
        /// </summary>
        private bool wasModified;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgsBuilder" /> class.
        /// </summary>
        /// <param name="keyPrefix">The key prefix.</param>
        /// <param name="keyValueSeparator">The key/value separator.</param>
        /// <param name="valueSeparator">The value separator.</param>
        /// <param name="pathWrapper">The path wrapper.</param>
        /// <param name="argsSeparator">The arguments separator.</param>
        private ArgsBuilder(
            string keyPrefix,
            string keyValueSeparator,
            string valueSeparator,
            string pathWrapper,
            string argsSeparator)
        {
            this.keyPrefix = keyPrefix;
            this.keyValueSeparator = keyValueSeparator;
            this.valueSeparator = valueSeparator;
            this.pathWrapper = pathWrapper;
            this.argsSeparator = argsSeparator;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ArgsBuilder"/> to <see cref="string"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(ArgsBuilder builder) => builder?.ToString();

        /// <summary>
        /// Creates a new <see cref="ArgsBuilder"/> with the specified separators.
        /// </summary>
        /// <param name="keyPrefix">Prepends each key.</param>
        /// <param name="keyValueSeparator">Separates keys and values.</param>
        /// <param name="valueSeparator">Separates multiple values or paths under a single key.</param>
        /// <param name="pathWrapper">Wraps paths using `AddPath` or `AddPaths` when paths have spaces etc.</param>
        /// <param name="argsSeparator">Separates key/value pairs (arguments).</param>
        /// <returns>A newly created <see cref="ArgsBuilder"/> instance.</returns>
        public static ArgsBuilder By(
            string keyPrefix,
            string keyValueSeparator,
            string valueSeparator = ";",
            string pathWrapper = "\"",
            string argsSeparator = " ")
            => new ArgsBuilder(keyPrefix, keyValueSeparator, valueSeparator, pathWrapper, argsSeparator);

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This <see cref="ArgsBuilder"/>.</returns>
        /// <example>
        /// var canAddKey = true;
        ///
        /// var args = ArgsBuilder.By("--", "=").AddKey("key1", canAddKey);
        ///
        /// // args: "--key1"
        /// </example>
        public ArgsBuilder AddKey(string key, bool condition = true)
        {
            if (!condition || string.IsNullOrEmpty(key))
            {
                return this;
            }

            return this.Append($"{this.keyPrefix}{key}");
        }

        /// <summary>
        /// Adds the specified list of keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddKeys = true;
        ///
        /// var args = ArgsBuilder.By("--", "=").AddKeys(new[] { "key1", "key2" }, canAddKeys);
        ///
        /// // args: "--key1 --key2"
        /// </example>
        public ArgsBuilder AddKeys(IEnumerable<string> keys, bool condition = true)
        {
            if (!condition || keys == null)
            {
                return this;
            }

            var add = string.Join(
                this.argsSeparator,
                from k in keys where !string.IsNullOrEmpty(k) select $"{this.keyPrefix}{k}");

            return string.IsNullOrEmpty(add) ? this : this.Append(add);
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddValue = true;
        ///
        /// var args = ArgsBuilder.By("--", "=").AddValue("12345", canAddValue);
        ///
        /// // args: "12345"
        /// </example>
        public ArgsBuilder AddValue(object value, bool condition = true)
        {
            var val = value?.ToString();

            if (!condition || string.IsNullOrEmpty(val))
            {
                return this;
            }

            return this.Append(val);
        }

        /// <summary>
        /// Adds the specified list of values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddValues = true;
        ///
        /// var args = ArgsBuilder.By("--", "=").AddValues(new [] { "111", "222", canAddValues);
        ///
        /// // args: "111 222"
        /// </example>
        public ArgsBuilder AddValues(IEnumerable<object> values, bool condition = true)
        {
            if (!condition || values == null)
            {
                return this;
            }

            var add = string.Join(
                this.argsSeparator,
                from v in values let vs = v?.ToString() where !string.IsNullOrEmpty(vs) select vs);

            return string.IsNullOrEmpty(add) ? this : this.Append(add);
        }

        /// <summary>
        /// Adds the specified key and value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddArgument = true;
        ///
        /// var args = ArgsBuilder.By("--", "=").AddArgument("arg1", "value1", canAddArgument);
        ///
        /// // args: "--arg1=value1"
        /// </example>
        public ArgsBuilder AddArgument(string key, object value, bool condition = true)
        {
            var val = value?.ToString();

            if (!condition || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(val))
            {
                return this;
            }

            return this.Append($"{this.keyPrefix}{key}{this.keyValueSeparator}{val}");
        }

        /// <summary>
        /// Adds the specified list of values with the specified key, repeating key/value pair if <paramref name="multipleTimes"/> is <c>true</c>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="values">The values.</param>
        /// <param name="multipleTimes">Determines whether to add each argument with a separate key.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddArguments = true;
        ///
        /// var args = ArgsBuilder.By("--", "=", valueSeparator: ",").AddArguments("key", new[] { "111", "222", false, canAddArguments);
        ///
        /// var multipleArgs = ArgsBuilder.By("--", "=").AddArguments("key", new[] { "111", "222", true, canAddArguments);
        ///
        /// // args: "--key=111,222"
        ///
        /// // multipleArgs: "--key=111 --key=222"
        /// </example>
        public ArgsBuilder AddArguments(
            string key,
            IEnumerable<object> values,
            bool multipleTimes = false,
            bool condition = true)
        {
            if (!condition || string.IsNullOrEmpty(key) || values == null)
            {
                return this;
            }

            if (multipleTimes)
            {
                foreach (var value in values)
                {
                    this.Append($"{this.keyPrefix}{key}{this.keyValueSeparator}{value}");
                }

                return this;
            }

            var add = string.Join(
                this.valueSeparator,
                from v in values let vs = v?.ToString() where !string.IsNullOrEmpty(vs) select vs);

            return string.IsNullOrEmpty(add)
                ? this
                : this.Append($"{this.keyPrefix}{key}{this.keyValueSeparator}{add}");
        }

        /// <summary>
        /// Adds the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddPath = true;
        ///
        /// var args = ArgsBuilder.By("--", "=").AddPath("C:\My Folder\my file.txt", canAddPath);
        ///
        /// // args: ""C:\My Folder\my file.txt""
        /// </example>
        public ArgsBuilder AddPath(string path, bool condition = true)
        {
            if (!condition || string.IsNullOrEmpty(path))
            {
                return this;
            }

            return this.Append($"{this.pathWrapper}{path}{this.pathWrapper}");
        }

        /// <summary>
        /// Adds the specified path with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="path">The path.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddPath = true;
        ///
        /// var args = ArgsBuilder.By("--", "=").AddPath("key", "C:\My Folder\my file.txt", canAddPath);
        ///
        /// // args: "--key="C:\My Folder\my file.txt""
        /// </example>
        public ArgsBuilder AddPath(string key, string path, bool condition = true)
        {
            if (!condition || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(path))
            {
                return this;
            }

            return this.Append(
                $"{this.keyPrefix}{key}{this.keyValueSeparator}{this.pathWrapper}{path}{this.pathWrapper}");
        }

        /// <summary>
        /// Adds the specified list of paths.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddPaths = true;
        ///
        /// var args = ArgsBuilder.By("--", "=").AddPaths(new [] { "C:\my file 1.txt", "C:\my file 2.txt", canAddPaths);
        ///
        /// // args: ""C:\my file 1.txt" "C:\my file 2.txt""
        /// </example>
        public ArgsBuilder AddPaths(IEnumerable<string> paths, bool condition = true)
        {
            if (!condition || paths == null)
            {
                return this;
            }

            var nonEmptyPaths = from p in paths
                                where !string.IsNullOrEmpty(p)
                                select $"{this.pathWrapper}{p}{this.pathWrapper}";

            var add = string.Join(this.argsSeparator, nonEmptyPaths);

            return string.IsNullOrEmpty(add) ? this : this.Append(add);
        }

        /// <summary>
        /// Adds the specified list of paths with the specified key, repeating key/path pair if <paramref name="multipleTimes"/> is <c>true</c>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="paths">The paths.</param>
        /// <param name="multipleTimes">Determines whether to add each path with separate key.</param>
        /// <param name="condition">A condition to add.</param>
        /// <returns>This instance.</returns>
        /// <example>
        /// var canAddPaths = true;
        ///
        /// var args = ArgsBuilder.By("--", "=", valueSeparator: ",").AddPaths("key", new[] { "C:\my file 1.txt", "C:\my file 2.txt", false, canAddPaths);
        ///
        /// var multipleArgs = ArgsBuilder.By("--", "=").AddPaths("key", new[] { "C:\my file 1.txt", "C:\my file 2.txt", true, canAddPaths);
        ///
        /// // args: "--key="C:\my file 1.txt","C:\my file 2.txt""
        ///
        /// // multipleArgs: "--key="C:\my file 1.txt" --key="C:\my file 2.txt""
        /// </example>
        public ArgsBuilder AddPaths(
            string key,
            IEnumerable<string> paths,
            bool multipleTimes = false,
            bool condition = true)
        {
            if (!condition || string.IsNullOrEmpty(key) || paths == null)
            {
                return this;
            }

            if (multipleTimes)
            {
                foreach (var path in paths)
                {
                    this.Append(
                        $"{this.keyPrefix}{key}{this.keyValueSeparator}{this.pathWrapper}{path}{this.pathWrapper}");
                }

                return this;
            }

            var nonEmptyPaths = from p in paths
                                where !string.IsNullOrEmpty(p)
                                select $"{this.pathWrapper}{p}{this.pathWrapper}";

            var add = string.Join(this.valueSeparator, nonEmptyPaths);

            return string.IsNullOrEmpty(add)
                ? this
                : this.Append($"{this.keyPrefix}{key}{this.keyValueSeparator}{add}");
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
            => this.wasModified ? this.builder.ToString().Remove(this.builder.Length - 1, 1) : this.builder.ToString();

        /// <summary>
        /// Appends the <paramref name="arg"/>.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>This <see cref="ArgsBuilder"/>.</returns>
        private ArgsBuilder Append(string arg)
        {
            this.builder.Append(arg).Append(this.argsSeparator);

            this.wasModified = true;

            return this;
        }
    }
}