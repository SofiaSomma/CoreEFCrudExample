﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEFCrud.DTOs.CustomerDto
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
