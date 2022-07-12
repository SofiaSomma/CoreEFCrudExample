
using CoreEFCrud.DTOs.CustomerDto;
using CoreEFCrud.Services.CustomerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEFCrud.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService; 
        }


        /// <summary>
        /// Get the whole list of customer items
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _customerService.GetCustomers());
        }

        /// <summary>
        /// Get a single customer item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
     
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _customerService.GetCustomer(id));
        }

        /// <summary>
        /// add a new customer 
        /// </summary>
        /// <returns>new customer id</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/customer
        ///     {        
        ///       "name": "Mike",
        ///       "surname": "Donegal"
        ///     }
        /// </remarks>
        /// <param name="customer"></param>
        [HttpPost]
        
        public async Task<IActionResult> Post([FromBody] AddCustomerDto customer)
        {
            return Ok(await _customerService.AddCustomer(customer));
        }


        /// <summary>
        /// Customer Update
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCustomerDto customer)
        {
            return Ok(await _customerService.UpdateCustomer(customer));
        }


        /// <summary>
        /// Patch = partial update based on JsonPatchDocument
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<UpdateCustomerDto> customer)
        {
            return Ok(await _customerService.PartialUpdateJsonPatchDocument(id,customer));
        }

        /// <summary>
        /// Patch = partial update based on sended fields
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("CustomPartialUpdate")]
        public async Task<IActionResult> CustomPartialUpdateForCustomer([FromBody] UpdateCustomerDto customer)
        {
            return Ok(await _customerService.CustomPartialUpdateForCustomer(customer));
        }

    }
}
