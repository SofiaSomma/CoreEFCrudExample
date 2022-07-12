using AutoMapper;
using CoreEFCrud.Data;
using CoreEFCrud.DTOs.ProductDto;
using CoreEFCrud.Services.Response;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreEFCrud.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ProductService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetProducts()
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            var dbProducts = await _context.Products.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetProductDto>>(dbProducts);
            return serviceResponse;
        }
    }
}
