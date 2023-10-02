using Elasticsearch.WEB.Services;
using Elasticsearch.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.WEB.Controllers;

public class ECommerceController : Controller
{
    private readonly ECommerceService _service;

    public ECommerceController(ECommerceService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Search([FromQuery]SearchPageViewModel searchViewModel)
    {
        var (eCommerceList, totalCount, pageLinkCount) = await _service.SearchAsync(searchViewModel.ECommerceSearchViewModel, searchViewModel.Page, searchViewModel.PageSize);

        searchViewModel.List = eCommerceList;
        searchViewModel.TotalCount = totalCount;
        searchViewModel.PageLinkCount = pageLinkCount;

        return View(searchViewModel);
    }
}
