using System;
using System.Collections.Generic;
using System.Text;

namespace Common.PagedListRequest
{
    public class PagedListRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Search { get; set; }

        public string ColumnName { get; set; }

        public bool IsDesc { get; set; }

    }
}
