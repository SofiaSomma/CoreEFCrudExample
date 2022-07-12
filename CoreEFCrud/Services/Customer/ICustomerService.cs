
using CoreEFCrud.DTOs.CustomerDto;
using CoreEFCrud.Services.Response;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreEFCrud.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<ServiceResponse<List<GetCustomerDto>>> GetCustomers();
        Task<ServiceResponse<GetCustomerDto>> GetCustomer(int id);
        Task<int> AddCustomer(AddCustomerDto customer);
        Task<ServiceResponse<GetCustomerDto>> PartialUpdateJsonPatchDocument(int id, JsonPatchDocument<UpdateCustomerDto> customer);
        Task<ServiceResponse<GetCustomerDto>> CustomPartialUpdateForCustomer(UpdateCustomerDto customer);
        Task<ServiceResponse<GetCustomerDto>> UpdateCustomer(UpdateCustomerDto customer);

       

    }
}
