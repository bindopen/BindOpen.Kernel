using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This class exposes extensions on exception.
    /// </summary>
    public static class TDataPageExtensions
    {
        /// <summary>
        /// Converts the specified response to a data page.
        /// </summary>
        /// <param name="request">The request to consider.</param>
        /// <param name="items">The items to consider.</param>
        /// <param name="totalCount">The total number of items to consider.</param>
        public static TDataPage<T> ToDataPage<T>(
            this IEnumerable<T> items,
            IDataPageRequest request,
            int? totalCount = null) where T : class
        {
            return new TDataPage<T>()
            {
                Items = items,
                PageIndex = request?.PageIndex,
                PageSize = request?.PageSize,
                TotalCount = totalCount == null ? null : Math.Min(request?.MaxCount ?? int.MaxValue, totalCount ?? 0)
            };
        }

        /// <summary>
        /// Converts the specified page to another page.
        /// </summary>
        /// <param name="page">The page to consider.</param>
        /// <param name="func">The function of item conversion to consider.</param>
        /// <typeparam name="P">The destination class to consider.</typeparam>
        /// <typeparam name="Q">The source class to consider.</typeparam>
        public static TDataPage<P> Map<P, Q>(
            this TDataPage<Q> page,
            Func<Q, P> func)
            where Q : class
            where P : class
        {
            return new TDataPage<P>()
            {
                Items = page?.Items?.Select(q => func?.Invoke(q)),
                PageIndex = page?.PageIndex,
                PageSize = page?.PageSize,
                TotalCount = page?.TotalCount
            };
        }
    }
}