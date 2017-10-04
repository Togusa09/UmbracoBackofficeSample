using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoBackofficeSample.Services
{
    public enum SortDirection
    {
        ASC,
        DESC
    }

    public class PagedRequest
    {
        public PagedRequest()
        {
            Page = 1;
            SortBy = string.Empty;
            SortDirection = SortDirection.ASC;
            PageSize = 50;
        }

        public int Page { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public int PageSize { get; set; }
    }
}