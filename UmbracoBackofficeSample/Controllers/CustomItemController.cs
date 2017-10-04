using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UmbracoBackofficeSample.Models;
using UmbracoBackofficeSample.Services;

namespace UmbracoBackofficeSample.Controllers
{
    public class CustomItemRequest : PagedRequest
    {
        public string searchProperty { get; set; }
    }

    public class CustomItemController : BackofficeApiController
    {
        private List<CustomItem> _items = new List<CustomItem>();


        public CustomItemController(IPagingService pagingService) : base(pagingService)
        {
            for (int i = 0; i < 100; i++)
            {
                _items.Add(new CustomItem
                {
                    Id = i,
                    Name = $"Item {i}"
                });
            }
        }

        [HttpGet]
        public PagedListResponse<CustomItem> GetCustomItems([FromUri]CustomItemRequest request)
        {
            IEnumerable<CustomItem> results = _items;
            if (!string.IsNullOrWhiteSpace(request.searchProperty))
            {
                results = results.Where(r => r.Name.Contains(request.searchProperty));
            }
            var totalItemCount = results.Count();
            var startAt = (request.Page - 1) * request.PageSize;

            var pageResult = results.Skip(startAt).Take(request.PageSize);

            return GetResponse(request, totalItemCount, pageResult);
        }

        [HttpGet]
        public CustomItem GetCustomItem(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }

        [HttpPost]
        public void UpdateCustomItem(int id, CustomItem model)
        {
            // Not actually updating
        }
    }
}