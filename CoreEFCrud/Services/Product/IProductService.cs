using CoreEFCrud.DTOs.ProductDto;
using CoreEFCrud.Services.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreEFCrud.Services.Product
{
    public interface IProductService
    {
        public Task<ServiceResponse<List<GetProductDto>>> GetProducts();
    }
}
