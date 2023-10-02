using Elasticsearch.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ECommerceController : ControllerBase
{
    private readonly ECommerceRepository _repository;

    public ECommerceController(ECommerceRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> TermQuery(string customerFirstName)
    {
        return Ok(await _repository.TermQueryAsync(customerFirstName));
    }

    [HttpPost]
    public async Task<IActionResult> TermsQuery(List<string> customerFirstNameList)
    {
        return Ok(await _repository.TermsQueryAsync(customerFirstNameList));
    }

    [HttpGet]
    public async Task<IActionResult> PrefixQuery(string customerFullNameList)
    {
        return Ok(await _repository.PrefixQueryAsync(customerFullNameList));
    }

    [HttpGet]
    public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
    {
        return Ok(await _repository.RangeQueryAsync(fromPrice, toPrice));
    }

    [HttpGet]
    public async Task<IActionResult> MatchAllQuery()
    {
        return Ok(await _repository.MatchAllQueryAsync());
    }

    [HttpGet]
    public async Task<IActionResult> PaginationQuery(int page = 1, int pageSize = 3)
    {
        return Ok(await _repository.PaginationQueryAsync(page, pageSize));
    }

    [HttpGet]
    public async Task<IActionResult> WildCardQuery(string customerFullNameList)
    {
        return Ok(await _repository.WildCardQueryAsync(customerFullNameList));
    }

    [HttpGet]
    public async Task<IActionResult> FuzzyQuery(string customerNameList)
    {
        return Ok(await _repository.FuzzyQueryAsync(customerNameList));
    }
    [HttpGet]
    public async Task<IActionResult> MatchQueryFullText(string categoryName)
    {
        return Ok(await _repository.MatchQueryFullTextAsync(categoryName));
    }

    [HttpGet]
    public async Task<IActionResult> MatchBoolPrefixQueryFullText(string customerFullName)
    {
        return Ok(await _repository.MatchBoolPrefixQueryFullTextAsync(customerFullName));
    }

    [HttpGet]
    public async Task<IActionResult> MatchPhareQueryFullText(string customerFullName)
    {
        return Ok(await _repository.MatchPhraseQueryFullTextAsync(customerFullName));
    }
    [HttpGet]
    public async Task<IActionResult> CompoundQueryExampleOne(string cityName, double taxfulTotalPrice, string categoryName, string manufacturer)
    {
        return Ok(await _repository.CompoundQueryExampleOneAsync(cityName, taxfulTotalPrice, categoryName, manufacturer));
    }
    [HttpGet]
    public async Task<IActionResult> CompoundQueryExampleTwo(string customerFullName)
    {
        return Ok(await _repository.CompoundQueryExampleTwoAsync(customerFullName));
    }
    [HttpGet]
    public async Task<IActionResult> MultiMatchQueryFullTextAsync(string name)
    {
        return Ok(await _repository.MultiMatchQueryFullTextAsync(name));
    }

}
