using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoBackofficeSample.Services
{
    public interface IPagingService
    {
        int BoundPageNumber(int totalItems, int page, int lastPage);
        int GetLastPage(int totalItems, PagedRequest request);
    }
}
