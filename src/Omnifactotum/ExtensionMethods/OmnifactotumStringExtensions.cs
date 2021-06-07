﻿#nullable enable

using System.Collections.Generic;
using System.Globalization;
#if (NETFRAMEWORK && !NET40) || NETSTANDARD || NETCOREAPP
using System.Runtime.CompilerServices;
#endif
using System.Text;
using Omnifactotum;
using Omnifactotum.Annotations;
using static Omnifactotum.FormattableStringFactotum;
//// Use NETSTANDARD2_1_OR_GREATER once AppVeyor updates their VM image with VS2019 16.10 or higher
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
using NotNullWhen = System.Diagnostics.CodeAnalysis.NotNullWhenAttribute;
#endif
using PureAttribute = System.Diagnostics.Contracts.PureAttribute;

//// ReSharper disable once CheckNamespace :: Namespace is intentionally named so in order to simplify usage of extension methods

namespace System
{
    /// <summary>
    ///     Contains extension methods for the <see cref="System.String"/> class.
    /// </summary>
    public static class OmnifactotumStringExtensions
    {
        private const string DoubleQuote = @"""";

        /// <summary>
        ///     Determines whether the specified string is <see langword="null"/> or an <see cref="String.Empty"/> string.
        /// </summary>
        /// <param name="value">
        ///     The string value to test.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified string is <see langword="null"/> or an <see cref="String.Empty"/> string;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        [Pure]
        [ContractAnnotation("false <= notnull", true)]
#if (NETFRAMEWORK && !NET40) || NETSTANDARD || NETCOREAPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsNullOrEmpty(
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
            [NotNullWhen(false)]
#endif
            [CanBeNull] this string? value)
            => string.IsNullOrEmpty(value);

        /// <summary>
        ///     Determines whether a specified string is <see langword="null"/>, <see cref="String.Empty"/>,
        ///     or consists only of white-space characters.
        /// </summary>
        /// <param name="value">
        ///     The string value to test.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified value is <see langword="null"/> or <see cref="String.Empty"/>, or if it consists
        ///     exclusively of white-space characters; otherwise, <see langword="false"/>.
        /// </returns>
        [Pure]
        [ContractAnnotation("false <= notnull", true)]
#if (NETFRAMEWORK && !NET40) || NETSTANDARD || NETCOREAPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsNullOrWhiteSpace(
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
            [NotNullWhen(false)]
#endif
            [CanBeNull] this string? value)
            => string.IsNullOrWhiteSpace(value);

        /// <summary>
        ///     Converts the specified string value to an equivalent <see cref="Boolean"/> value.
        ///     If the specified string value cannot be converted, <see langword="null"/> is returned.
        /// </summary>
        /// <param name="value">
        ///     The string value to convert. Can be <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="Nullable{Boolean}"/> representation of the specified string value.
        /// </returns>
        [CanBeNull]
        [Pure]
        public static bool? ToNullableBoolean([CanBeNull] this string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (bool.TryParse(value, out var booleanResult))
            {
                return booleanResult;
            }

            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intResult))
            {
                return intResult != 0;
            }

            return null;
        }

        /// <summary>
        ///     Converts the specified string value to an equivalent <see cref="Boolean"/> value.
        ///     If the specified string value cannot be converted, an exception is thrown.
        /// </summary>
        /// <param name="value">
        ///     The string value to convert.
        /// </param>
        /// <returns>
        ///     The <see cref="System.Boolean"/> representation of the specified string value.
        /// </returns>
        [Pure]
        public static bool ToBoolean([NotNull] this string value)
        {
            var proxyResult = ToNullableBoolean(value);
            if (!proxyResult.HasValue)
            {
                throw new ArgumentException(
                    AsInvariant($@"The specified value cannot be converted to {nameof(Boolean)}."),
                    nameof(value));
            }

            return proxyResult.Value;
        }

        /// <summary>
        ///     Concatenates a specified <see cref="System.String"/> separator between each element of
        ///     the specified <see cref="System.String"/> collection, yielding a single concatenated string.
        /// </summary>
        /// <param name="values">
        ///     The <see cref="System.String"/> collection whose items to concatenate.
        /// </param>
        /// <param name="separator">
        ///     The separator to insert between each element of the <see cref="System.String"/> collection.
        ///     Can be <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     A <see cref="System.String"/> consisting of the elements of <paramref name="values"/> delimited
        ///     by the <paramref name="separator"/> string.
        /// </returns>
        [NotNull]
#if (NETFRAMEWORK && !NET40) || NETSTANDARD || NETCOREAPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string Join(
            [NotNull] [ItemCanBeNull] [InstantHandle] this IEnumerable<string?> values,
            [CanBeNull] string? separator)
            => string.Join(separator, values ?? throw new ArgumentNullException(nameof(values)));

        /// <summary>
        ///     Avoids the specified string value to be a <see langword="null"/> reference: returns the specified string value
        ///     if it is not <see langword="null"/> or an empty string otherwise.
        /// </summary>
        /// <param name="source">
        ///     The string value to secure from a <see langword="null"/> reference.
        /// </param>
        /// <returns>
        ///     The source string value if it is not <see langword="null"/>; otherwise, empty string.
        /// </returns>
        [NotNull]
        [Pure]
#if (NETFRAMEWORK && !NET40) || NETSTANDARD || NETCOREAPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string AvoidNull([CanBeNull] this string? source) => source ?? string.Empty;

        /// <summary>
        ///     <para>
        ///         Converts the specified string to its UI representation.
        ///     </para>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>The input value</term>
        ///             <description>The result of the method</description>
        ///         </listheader>
        ///         <item>
        ///             <term><see langword="null"/></term>
        ///             <description>The literal: <b>null</b></description>
        ///         </item>
        ///         <item>
        ///             <term>not <see langword="null"/></term>
        ///             <description>
        ///                 An input value enclosed in the double quote characters ("). If the value
        ///                 contains one or more double quote characters, then each of them is
        ///                 duplicated in the result.
        ///             </description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <param name="value">
        ///     The string value to convert.
        /// </param>
        /// <returns>
        ///     The UI representation of the specified string.
        /// </returns>
        /// <example>
        ///     <code>
        /// <![CDATA[
        ///         string value1 = null;
        ///         Console.WriteLine("Value is {0}.", value1.ToUIString()); // Output: Value is null.
        /// ]]>
        ///     </code>
        ///     <code>
        /// <![CDATA[
        ///         string value2 = string.Empty;
        ///         Console.WriteLine("Value is {0}.", value2.ToUIString()); // Output: Value is "".
        /// ]]>
        ///     </code>
        ///     <code>
        /// <![CDATA[
        ///         string value3 = "Hello";
        ///         Console.WriteLine("Value is {0}.", value3.ToUIString()); // Output: Value is "Hello".
        /// ]]>
        ///     </code>
        ///     <code>
        /// <![CDATA[
        ///         string value4 = "Class 'MyClass' is found in project \"MyProject\".";
        ///         Console.WriteLine("Value is {0}.", value4.ToUIString()); // Output: Value is "Class 'MyClass' is found in project ""MyProject"".".
        /// ]]>
        ///     </code>
        /// </example>
        [NotNull]
        [Pure]
        public static string ToUIString([NotNull] this string? value)
        {
            const string DoubleDoubleQuote = DoubleQuote + DoubleQuote;

            return value is null
                ? OmnifactotumRepresentationConstants.NullValueRepresentation
                : DoubleQuote + value.Replace(DoubleQuote, DoubleDoubleQuote) + DoubleQuote;
        }

        /// <summary>
        ///     Removes all leading and trailing occurrences of a set of characters specified in an array
        ///     from the specified <see cref="System.String"/> object.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="System.String"/> value to trim. Can be <see langword="null"/>.
        /// </param>
        /// <param name="trimChars">
        ///     An array of Unicode characters to remove, or <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     The string that remains after all occurrences of the characters in the <paramref name="trimChars"/>
        ///     parameter are removed from the start and end of the specified string.
        ///     If <paramref name="trimChars"/> is <see langword="null"/> or an empty array, Unicode white-space characters
        ///     are removed instead.
        /// </returns>
        [NotNull]
        [Pure]
#if (NETFRAMEWORK && !NET40) || NETSTANDARD || NETCOREAPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string TrimSafely([CanBeNull] this string? value, [CanBeNull] params char[]? trimChars)
            => value?.Trim(trimChars) ?? string.Empty;

        /// <summary>
        ///     Removes all leading occurrences of a set of characters specified in an array
        ///     from the specified <see cref="System.String"/> object.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="System.String"/> value to trim. Can be <see langword="null"/>.
        /// </param>
        /// <param name="trimChars">
        ///     An array of Unicode characters to remove, or <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     The string that remains after all occurrences of the characters in the <paramref name="trimChars"/>
        ///     parameter are removed from the start of the specified string.
        ///     If <paramref name="trimChars"/> is <see langword="null"/> or an empty array, Unicode white-space characters
        ///     are removed instead.
        /// </returns>
        [NotNull]
        [Pure]
#if (NETFRAMEWORK && !NET40) || NETSTANDARD || NETCOREAPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string TrimStartSafely([CanBeNull] this string? value, [CanBeNull] params char[]? trimChars)
            => value?.TrimStart(trimChars) ?? string.Empty;

        /// <summary>
        ///     Removes all trailing occurrences of a set of characters specified in an array
        ///     from the specified <see cref="System.String"/> object.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="System.String"/> value to trim. Can be <see langword="null"/>.
        /// </param>
        /// <param name="trimChars">
        ///     An array of Unicode characters to remove, or <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     The string that remains after all occurrences of the characters in the <paramref name="trimChars"/>
        ///     parameter are removed from the end of the specified string.
        ///     If <paramref name="trimChars"/> is <see langword="null"/> or an empty array, Unicode white-space characters
        ///     are removed instead.
        /// </returns>
        [NotNull]
        [Pure]
#if (NETFRAMEWORK && !NET40) || NETSTANDARD || NETCOREAPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string TrimEndSafely([CanBeNull] this string? value, [CanBeNull] params char[]? trimChars)
            => value?.TrimEnd(trimChars) ?? string.Empty;

        /// <summary>
        ///     Shortens the specified <see cref="System.String"/> value if its length exceeds the specified length.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="System.String"/> value to shorten. Can be <see langword="null"/>.
        /// </param>
        /// <param name="maximumLength">
        ///     The maximum length of the resulting string.
        /// </param>
        /// <returns>
        ///     A <see cref="System.String"/> value containing the first <paramref name="maximumLength"/> characters
        ///     of the original string value if the latter is longer than <paramref name="maximumLength"/>;
        ///     otherwise, the original string value.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="maximumLength"/> is less than zero.
        /// </exception>
        [NotNull]
        [Pure]
        public static string Shorten([CanBeNull] this string? value, int maximumLength)
        {
            if (maximumLength < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maximumLength),
                    maximumLength,
                    "The length must be a non-negative value.");
            }

            return value is null || maximumLength == 0
                ? string.Empty
                : value.Length <= maximumLength ? value : value.Substring(0, maximumLength);
        }

        /// <summary>
        ///     Replicates the specified string value the specified number of times.
        /// </summary>
        /// <param name="value">
        ///     The value to replicate. Can be <see langword="null"/>.
        /// </param>
        /// <param name="count">
        ///     The number of times to replicate the specified string value.
        /// </param>
        /// <returns>
        ///     The specified value repeated the specified number of times, or <see cref="String.Empty"/> if
        ///     <paramref name="value"/> is <see langword="null"/> or an empty string or <paramref name="count"/> is zero.
        /// </returns>
        [NotNull]
        [Pure]
        public static string Replicate([CanBeNull] this string? value, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "The count must be greater than or equal to zero.");
            }

            return value is null || value.Length == 0 || count == 0
                ? string.Empty
                : new StringBuilder(checked(value.Length * count)).Insert(0, value, count).ToString();
        }
    }
}