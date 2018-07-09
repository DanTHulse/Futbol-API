using System.Collections.Generic;
using Futbol.API.DataModels;

namespace Futbol.API.Helpers
{
    public static class PageInformation
    {
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
