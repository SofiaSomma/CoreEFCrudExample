using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEFCrud.DTOs.CustomerDto
{
    public class GetCustomerDto 
    {
        public string CustomerId { get; set;}
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
