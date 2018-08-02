using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Futbol.Common.Models.Pagination
{
    public class CollectionPage<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalPages => (int)(Math.Ceiling((decimal)this.TotalItems / this.ItemsPerPage));

        public KeyValuePair<string, StringValues> Header => new KeyValuePair<string, StringValues>("X-Pagination-Total-Count", this.TotalItems.ToString());

        public CollectionPage()
        {
        }

        public CollectionPage(IEnumerable<T> items, int totalItems, int pageSize)
        {
            this.Items = items;
            this.TotalItems = totalItems;
            this.ItemsPerPage = pageSize;
        }
    }
}