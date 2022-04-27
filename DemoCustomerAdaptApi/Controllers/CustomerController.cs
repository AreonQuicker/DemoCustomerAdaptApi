using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DemoCustomerAdaptApi.Domain.Interfaces;
using DemoCustomerAdaptApi.Domain.Models;
using DemoCustomerAdaptApi.Filters;
using DemoCustomerAdaptApi.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DemoCustomerAdaptApi.Controllers
{
    [TypeFilter(typeof(ApiValidateModelAttribute))]
    public class CustomerController : ApiControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiActionResult<CustomerResult>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiActionResult<CustomerResult>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Customer"}, OperationId = "GetCustomer",
            Description = "GetCustomer")]
        [TypeFilter(typeof(ApiActionResultFilterAttribute<CustomerResult>))]
        public async Task<CustomerResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetAsync(id);

            return customer.Adapt<CustomerResult>();
        }

        [HttpGet()]
        [ProducesResponseType(typeof(ApiActionResult<IEnumerable<CustomerResult>>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiActionResult<IEnumerable<CustomerResult>>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Customer"}, OperationId = "GetAllCustomers",
            Description = "GetAllCustomers")]
        [TypeFilter(typeof(ApiActionResultFilterAttribute<IEnumerable<CustomerResult>>))]
        public async Task<IEnumerable<CustomerResult>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();

            return customers.Adapt<IEnumerable<CustomerResult>>();
        }

        [HttpPost()]
        [ProducesResponseType(typeof(ApiActionResult<int>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiActionResult<int>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Customer"}, OperationId = "CreateCustomer",
            Description = "CreateCustomer")]
        [TypeFilter(typeof(ApiActionResultFilterAttribute<int>))]
        public async Task<int> CreateCustomer(
            [FromBody] CustomerCreateRequest customerCreateRequest)
        {
            var id = await _customerService.CreateAsync(customerCreateRequest);

            return id;
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiActionResult<int>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiActionResult<int>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Customer"}, OperationId = "UpdateCustomer",
            Description = "UpdateCustomer")]
        [TypeFilter(typeof(ApiActionResultFilterAttribute<int>))]
        public async Task<int> UpdateCustomer(
            int id,
            [FromBody] CustomerUpdateRequest customerUpdateRequest)
        {
            var updatedId = await _customerService.UpdateAsync(id, customerUpdateRequest);

            return updatedId;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiActionResult<int>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiActionResult<int>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Customer"}, OperationId = "DeleteCustomer",
            Description = "DeleteCustomer")]
        [TypeFilter(typeof(ApiActionResultFilterAttribute<int>))]
        public async Task<int> DeleteCustomer(
            int id)
        {
            var deletedId = await _customerService.DeleteAsync(id);

            return deletedId;
        }
    }
}