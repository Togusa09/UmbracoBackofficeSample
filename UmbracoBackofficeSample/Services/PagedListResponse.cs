using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoBackofficeSample.Services
{
    public class PagedListResponse<T>
    {
        public PagedListResponse(List<T> list, int pageNumber, int pageCount, int totalItems)
        {
            List = list;
            PageNumber = pageNumber;
            PageCount = pageCount;
            TotalItems = totalItems;
        }

        public List<T> List { get; }
        public int PageNumber { get; }
        public int PageCount { get; }
        public int TotalItems { get; }
    }
}