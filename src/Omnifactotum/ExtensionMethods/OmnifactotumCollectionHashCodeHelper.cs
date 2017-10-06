﻿using System.Linq;
using Omnifactotum.Annotations;

//// Namespace is intentionally named so in order to simplify usage of extension methods
//// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    /// <summary>
    ///     Contains the extension methods for helping to compute the hash codes for collections of objects.
    /// </summary>
    public static class OmnifactotumCollectionHashCodeHelper
    {
        /// <summary>
        ///     Computes the hash code of the specified collection by combining hash codes of the elements
        ///     in the collection into a new hash code.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of elements in the collection.
        /// </typeparam>
        /// <param name="collection">
        ///     The collection to compute a hash code of.
        /// </param>
        /// <returns>
        ///     A hash code of the specified collection.
        /// </returns>
        public static int ComputeCollectionHashCode<T>([CanBeNull] this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return 0;
            }

            return collection.Aggregate(
                0,
                (current, item) => current.CombineHashCodeValues(item.GetHashCodeSafely()));
        }
    }
}