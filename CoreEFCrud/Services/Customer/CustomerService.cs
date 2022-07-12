using AutoMapper;
using CoreEFCrud.Data;
using CoreEFCrud.DTOs.CustomerDto;
using CoreEFCrud.Extensions;
using CoreEFCrud.Models;
using CoreEFCrud.Services.Response;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEFCrud.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CustomerService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetCustomerDto>> GetCustomer(int id)
        {
            ServiceResponse<GetCustomerDto> serviceResponse = new ServiceResponse<GetCustomerDto>();
            Customer dbCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            serviceResponse.Data = _mapper.Map<GetCustomerDto>(dbCustomer);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCustomerDto>>> GetCustomers()
        {
            ServiceResponse<List<GetCustomerDto>> serviceResponse = new ServiceResponse<List<GetCustomerDto>>();
            List<Customer> dbCustomers = await _context.Customers.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetCustomerDto>>(dbCustomers);
            return serviceResponse;
        }

        public async Task<int> AddCustomer(AddCustomerDto customer)
        {
            Customer customerDb = _mapper.Map<Customer>(customer);
            await _context.Customers.AddAsync(customerDb);
            await _context.SaveChangesAsync();
            int id = customerDb.CustomerId;
            return id;
        }

        public async Task<ServiceResponse<GetCustomerDto>> PartialUpdateJsonPatchDocument(int id, JsonPatchDocument<UpdateCustomerDto> patchDocument)
        {
            var customerDb = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
            var intermediate = _mapper.Map<UpdateCustomerDto>(customerDb);
            patchDocument.ApplyTo(intermediate);
            _mapper.Map(intermediate, customerDb);
            _context.Entry(customerDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            ServiceResponse<GetCustomerDto> serviceResponse = new ServiceResponse<GetCustomerDto>();
            serviceResponse.Data = _mapper.Map<GetCustomerDto>(customerDb);
            return serviceResponse;
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<GetCustomerDto>> CustomPartialUpdateForCustomer(UpdateCustomerDto customer)
        {

            var customerDb = _context.Customers.FirstOrDefault(x => x.CustomerId == customer.Id);
            customerDb = PatchMergeExtension.From<Customer, UpdateCustomerDto>(customerDb, customer);
            _context.Entry(customerDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            ServiceResponse<GetCustomerDto> serviceResponse = new ServiceResponse<GetCustomerDto>();
            serviceResponse.Data = _mapper.Map<GetCustomerDto>(customerDb);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCustomerDto>> UpdateCustomer(UpdateCustomerDto customer)
        {
            var customerDb = _context.Customers.FirstOrDefault(x => x.CustomerId == customer.Id);
            //copy customer in another. quick solution, detach the entity 'cause ef it is after the mapping 
            //is not going to be able to track the original instance anymore. Alternatively working on the 
            //original instance
            _context.Entry(customerDb).State = EntityState.Detached; 
            customerDb = _mapper.Map<Customer>(customer);
            _context.Entry(customerDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            ServiceResponse<GetCustomerDto> serviceResponse = new ServiceResponse<GetCustomerDto>();
            serviceResponse.Data = _mapper.Map<GetCustomerDto>(customerDb);
            return serviceResponse;
        }
    }
}
