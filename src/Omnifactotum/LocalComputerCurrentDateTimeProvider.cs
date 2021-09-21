﻿#nullable enable

using System;
using Omnifactotum.Abstractions;

namespace Omnifactotum
{
    /// <summary>
    ///     Provides the current date and time on the local computer.
    /// </summary>
    public sealed class LocalComputerCurrentDateTimeProvider : ICurrentDateTimeProvider
    {
        /// <summary>
        ///     Gets a <see cref="DateTime"/> object that is set to the current <b>UTC</b> date and time on the local computer.
        /// </summary>
        /// <returns>
        ///     A <see cref="DateTime"/> object that is set to the current <b>UTC</b> date and time on the local computer.
        /// </returns>
        public DateTime GetUtcTime() => DateTime.UtcNow;
    }
}