using Elasticsearch.WEB.Models;
using Elasticsearch.WEB.Repositories;
using Elasticsearch.WEB.ViewModels;

namespace Elasticsearch.WEB.Services;

public class BlogService
{
    private readonly BlogRepository _repository;

    public BlogService(BlogRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> SaveAsync(BlogCreateViewModel model)
    {
        var newBlog = new Blog
        {
            Title = model.Title,
            Content = model.Content,
            UserId = Guid.NewGuid(),
            Tags = model.Tags.Split(","),
        };

        var isCreatedBlog = await _repository.SaveAsync(newBlog);

        return isCreatedBlog is not null;
    }

    public async Task<List<BlogViewModel>> SearchAsync(string searchText)
    {
        var blogList = await _repository.SearchAsync(searchText);

        return blogList.Select(b => new BlogViewModel
        {
            Id = b.Id,
            Title = b.Title,
            Content = b.Content,
            UserId = b.UserId.ToString(),
            Created = b.Created.ToShortDateString(),
            Tags=String.Join(",", b.Tags)

        }).ToList();
    }
}
