using AutoMapper;
using CoreEFCrud.DTOs.CustomerDto;
using CoreEFCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEFCrud
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, GetCustomerDto>();
            CreateMap<AddCustomerDto, Customer>();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
        }
    }
}
