using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using Calculation.Client;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly CalculationClient _calculationClient;

        public CustomerController(ICustomerService customerService, CalculationClient client)
        {
            _customerService = customerService;
            _calculationClient = client;
        }

        /// <summary>
        /// Gets customers
        /// </summary>
        /// <param name="timestampFrom">Timestamp from which the customers are searched.</param>
        /// <param name="timestamptTo">Timestampt to which the customers are searched.</param>
        /// <returns>The customers</returns>
        [HttpGet]
        // TODO: This method is unnecessary since there is another with default null values several lines below.
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            // TODO: Create unit and integration tests, container, move to Azure and make services communicating to each other
            var customers = await _customerService.GetCustomers();
            return Ok(customers);
        }

        /// <summary>
        /// Gets customer by id
        /// </summary>
        /// <param name="id">The customer id</param>
        /// <returns>Customer by id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet("from/{timestamptFrom}/to/{timestamptTo}")]
        public async Task<ActionResult<CalculationResult>> GetCalculationResult(
            long? timestamptFrom = null, 
            long? timestamptTo = null)
        {
            var customers = await _customerService.GetCustomers(timestamptFrom, timestamptTo);
            var values = customers.Select(customer => customer.V);
            var results = await _calculationClient.CalculationAsync(values);
            return Ok(results);
        }

        /// <summary>
        /// Updates customer
        /// </summary>
        /// <param name="id">The customer id</param>
        /// <param name="input">The customer input model for update</param>
        /// <returns>The updated customer</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> PutCustomer(int id, CustomerInput input)
        {
            var customer = await _customerService.PutCustomer(id, input);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /// <summary>
        /// Creates new customer
        /// </summary>
        /// <param name="input">The customer input model for creation</param>
        /// <returns>Created customer</returns>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerInput input)
        {
            var customer = await _customerService.PostCustomer(input);
            return Ok(customer);
        }

        /// <summary>
        /// Deletes customer
        /// </summary>
        /// <param name="id">The customer id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.DeleteCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
