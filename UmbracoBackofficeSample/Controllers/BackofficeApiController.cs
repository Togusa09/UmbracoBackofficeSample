using System.Collections.Generic;
using System.Linq;
using Umbraco.Web.Editors;
using UmbracoBackofficeSample.Services;

namespace UmbracoBackofficeSample.Controllers
{
    public abstract class BackofficeApiController : UmbracoAuthorizedJsonController
    {
        private readonly IPagingService _pagingService;

        protected BackofficeApiController(IPagingService pagingService)
        {
            _pagingService = pagingService;
        }

        protected PagedListResponse<T> GetResponse<T>(PagedRequest request, int totalItems, IEnumerable<T> results)
        {
            var lastPage = _pagingService.GetLastPage(totalItems, request);
            var list = results.ToList();
            var pageNumber = _pagingService.BoundPageNumber(totalItems, request.Page, lastPage);

            return new PagedListResponse<T>(list, pageNumber, lastPage, totalItems);
        }
    }
}