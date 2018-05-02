using System.Collections.Generic;
using System.Linq;

namespace Futbol.API.DataModels
{
    public class PageHeader<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool HasNextPage => this.Items == null || this.Items.Any();

        public int NextPage => this.HasNextPage ? this.PageNumber + 1 : this.PageNumber;

        public IEnumerable<T> Items { get; set; }
    }
}
