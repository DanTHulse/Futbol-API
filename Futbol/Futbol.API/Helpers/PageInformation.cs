using System.Collections.Generic;
using Futbol.API.DataModels;

namespace Futbol.API.Helpers
{
    public static class PageInformation
    {
        /// <summary>
        /// Builds the page header.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="itemsCount">The items count.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>A page header model for the response</returns>
        public static PageHeader<T> BuildPageHeader<T>(this IEnumerable<T> items, int page, int pageSize) where T : class
        {
            return new PageHeader<T>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize
            };
        }
    }
}
