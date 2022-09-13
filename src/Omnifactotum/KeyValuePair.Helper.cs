﻿#if NETFRAMEWORK

using System.Runtime.CompilerServices;
using Omnifactotum;

//// ReSharper disable once CheckNamespace :: Namespace is intentionally named so in order to simplify usage
namespace System.Collections.Generic;

/// <summary>
///     Provides helper functionality for creating instances of
///     the <see cref="KeyValuePair{TKey,TValue}"/> type using type inference in a friendly way.
/// </summary>
public static class KeyValuePair
{
    /// <summary>
    ///     Creates a new <see cref="KeyValuePair{TKey,TValue}"/> using the specified key and value.
    /// </summary>
    /// <typeparam name="TKey">
    ///     The type of the key.
    /// </typeparam>
    /// <typeparam name="TValue">
    ///     The type of the value.
    /// </typeparam>
    /// <param name="key">
    ///     The key to initialize a <see cref="KeyValuePair{TKey,TValue}"/> with.
    /// </param>
    /// <param name="value">
    ///     The value to initialize a <see cref="KeyValuePair{TKey,TValue}"/> with.
    /// </param>
    /// <returns>
    ///     A new <see cref="KeyValuePair{TKey,TValue}"/> having the specified key and value set.
    /// </returns>
    [MethodImpl(OmnifactotumConstants.MethodOptimizationOptions.Standard)]
    public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value) => OmnifactotumKeyValuePair.Create(key, value);
}

#endif