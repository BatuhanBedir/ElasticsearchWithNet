using System.ComponentModel.DataAnnotations;

namespace Elasticsearch.WEB.ViewModels;

public class BlogCreateViewModel
{
    [Display(Name = "Blog Title")]
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    [Display(Name = "Blog Content")]
    public string Content { get; set; } = null!;

    [Display(Name = "Blog Tags")]
    public string Tags { get; set; } = null!;
}
