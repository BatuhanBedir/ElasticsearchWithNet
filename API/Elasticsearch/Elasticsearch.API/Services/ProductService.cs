using Elastic.Clients.Elasticsearch;
using Elasticsearch.API.DTOs;
using Elasticsearch.API.Repositories;
using System.Net;

namespace Elasticsearch.API.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
    {
        var responseProduct = await _productRepository.SaveAsync(request.CreateProduct());

        if (responseProduct is null) return ResponseDto<ProductDto>.Fail(new List<string> { "kayıt esnasında hata meydana geldi" }, HttpStatusCode.InternalServerError);

        return ResponseDto<ProductDto>.Success(responseProduct.CreateDto(), HttpStatusCode.Created);
    }
    public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productListDto = new List<ProductDto>();

        foreach (var x in products)
        {
            if (x.Feature is null)
            {
                productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, null));
                continue;
            }

            productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto(x.Feature.Width, x.Feature.Height, x.Feature!.Color.ToString())));

        }

        return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
    }
    public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
    {
        var hasProduct = await _productRepository.GetByIdAsync(id);
        if (hasProduct is null) return ResponseDto<ProductDto>.Fail("ürün bulunamadı", HttpStatusCode.NotFound);

        return ResponseDto<ProductDto>.Success(hasProduct.CreateDto(), HttpStatusCode.OK);
    }
    public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto updateProduct)
    {
        var isSuccess = await _productRepository.UpdateAsync(updateProduct);

        if (!isSuccess) return ResponseDto<bool>.Fail("update esnasında hata meydana geldi", HttpStatusCode.InternalServerError);

        return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
    }
    public async Task<ResponseDto<bool>> DeleteAsync(string id)
    {
        var deleteResposne = await _productRepository.DeleteAsync(id);

        if (!deleteResposne.IsValidResponse && deleteResposne.Result == Result.NotFound)
            return ResponseDto<bool>.Fail("silmeye çalıştıgınız ürün bulunamamıştır", HttpStatusCode.NotFound);

        if (!deleteResposne.IsValidResponse)
        {
            deleteResposne.TryGetOriginalException(out Exception? exception);
            _logger.LogError(exception, deleteResposne.ElasticsearchServerError.Error.ToString());

            return ResponseDto<bool>.Fail("silme işlemi esnasında hata meydana geldi", HttpStatusCode.InternalServerError);
        }


        return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
    }
}
