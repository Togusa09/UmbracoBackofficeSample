using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoBackofficeSample.Services
{
    public class PagingService : IPagingService
    {
        public int GetLastPage(int totalItems, PagedRequest request)
        {
            return (int)Math.Ceiling((decimal)totalItems / request.PageSize);
        }

        public int BoundPageNumber(int totalItems, int page, int lastPage)
        {
            ;
            if (page > lastPage)
                page = lastPage;
            if (page < 1)
                page = 1;
            return page;
        }
    }
}