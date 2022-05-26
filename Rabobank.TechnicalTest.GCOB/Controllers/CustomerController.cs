using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rabobank.TechnicalTest.GCOB.Api.Models.Customers;
using Rabobank.TechnicalTest.GCOB.Commands.Customers;
using Rabobank.TechnicalTest.GCOB.Commands.Handlers.Commands;
using Rabobank.TechnicalTest.GCOB.Queries.Customers;
using Rabobank.TechnicalTest.GCOB.Queries.Handlers.Customers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerCommandHandler _customerCommandHandler;
        private readonly CustomerQueryHandler _customerQueryHandler;

        /// <summary>
        /// Controller to handle Customer requests.
        /// </summary>
        public CustomerController(
            ILogger<CustomerController> logger,
            CustomerCommandHandler commandHandler,
            CustomerQueryHandler queryHandler)
        {
            _logger = logger;
            _customerCommandHandler = commandHandler;
            _customerQueryHandler = queryHandler;
        }

        /// <summary>
        /// Handles requests to fetch a customer for a specific Id.
        /// </summary>
        /// <param name="customerId">The customer Id.</param>
        [HttpGet]
        [Route("{customerId:int}", Name = nameof(GetCustomerAsync))]
        [SwaggerOperation(OperationId = "Customers_GetCustomersAsync")]
        [SwaggerResponse(200, "Returned with the customer details for the given customer Id.")]
        [SwaggerResponse(400, "Returned when trying to retrieve a customer and it fails.")]
        [SwaggerResponse(404, "Returned when the customer Id could not be found.")]
        public async Task<ActionResult<CustomerDto>> GetCustomerAsync(int customerId)
        {
            try
            {
                _logger.LogDebug($"Get customer called with Id {customerId}");

                var query = new CustomerQuery(customerId);
                var queryResult = await _customerQueryHandler.HandleAsync(query);

                return Ok(queryResult);

            }
            catch (ArgumentException aex)
            {
                _logger.LogError("Get Customer failed with exception: {ex}", aex.Message);
                return NotFound(aex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get Customer failed with exception: {ex}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Handles requests to add a customer.
        /// </summary>
        /// <param name="newCustomer">The Customer details.</param>
        [HttpPost]
        [Route("")]
        [SwaggerOperation(OperationId = "Customers_AddCustomerAsync")]
        [SwaggerResponse(200, "Returned when the new customer was successfully added.")]
        [SwaggerResponse(404, "Returned when trying to create a customer with a country that does not exists.")]
        [SwaggerResponse(400, "Returned when trying to create a customer and it fails.")]
        public async Task<ActionResult<CustomerDto>> AddCustomer([FromBody] AddCustomerDto newCustomer)
        {
            try
            {
                _logger.LogDebug("Add customer called with object {newCustomer}", JsonConvert.SerializeObject(newCustomer));

                var command = new AddCustomerCommand(newCustomer.Name, newCustomer.Surname, newCustomer.Street, newCustomer.City, newCustomer.Postcode, newCustomer.Country);
                var commandResult = await _customerCommandHandler.HandleAsync(command);

                return Ok(commandResult);

            }
            catch (ArgumentException aex)
            {
                _logger.LogError("Add Customer failed with exception: {ex}", aex.Message);
                return NotFound(aex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Add Customer failed with exception: {ex}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
