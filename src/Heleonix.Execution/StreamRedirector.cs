// <copyright file="StreamRedirector.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution;

/// <summary>
/// Forwards data from a source stream into a destination stream using text writers.
/// </summary>
internal class StreamRedirector
{
    private readonly TextReader source;

    private readonly TextWriter destination;

    private readonly int bufferSize;

    /// <summary>
    /// Initializes a new instance of the <see cref="StreamRedirector"/> class.
    /// </summary>
    /// <param name="source">The stream to forward data from.</param>
    /// <param name="destination">The stream to forward data to.</param>
    /// <param name="bufferSize">The size of the buffer of <see cref="char"/> data to transfer read from the
    /// <paramref name="source"/> and write to the <paramref name="destination"/> at once.</param>
    public StreamRedirector(TextReader source, TextWriter destination, int bufferSize)
    {
        this.source = source;
        this.destination = destination;
        this.bufferSize = bufferSize;
    }

    /// <summary>
    /// Starts forwarding of the assigned stream on the background asynchronously.
    /// </summary>
    public void Start()
    {
        Task.Run(async () =>
        {
            var buffer = new char[this.bufferSize];

            while (true)
            {
                var count = await this.source.ReadAsync(buffer, 0, this.bufferSize);

                if (count <= 0)
                {
                    break;
                }

                await this.destination.WriteAsync(buffer, 0, count);
                await this.destination.FlushAsync();
            }
        });
    }
}
