using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Api.Models.Customers;
using Rabobank.TechnicalTest.GCOB.Commands.Handlers.Commands;
using Rabobank.TechnicalTest.GCOB.Controllers;
using Rabobank.TechnicalTest.GCOB.DataLayer.Models;
using Rabobank.TechnicalTest.GCOB.DataLayer.Repositories;
using Rabobank.TechnicalTest.GCOB.Queries.Handlers.Customers;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerControllerTest
    {
        public Mock<ILogger<CustomerController>> mockLogger = new Mock<ILogger<CustomerController>>();
        private Mock<IAddressRepository> _addressRepository = new Mock<IAddressRepository>();
        private Mock<ICountryRepository> _countryRepository = new Mock<ICountryRepository>();
        private Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();


        public CustomerCommandHandler commandHandler;
        public CustomerQueryHandler queryHandler;

        [TestInitialize]
        public void Initialize()
        {
            commandHandler = new CustomerCommandHandler(_addressRepository.Object, _countryRepository.Object, _customerRepository.Object);
            queryHandler = new CustomerQueryHandler(_addressRepository.Object, _countryRepository.Object, _customerRepository.Object);
        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsReturned()
        {
            // Arrange
            _customerRepository.Setup(cr => cr.GetAsync(It.IsAny<int>())).ReturnsAsync(GetCustomerResult());
            _addressRepository.Setup(ar => ar.GetAsync(It.IsAny<int>())).ReturnsAsync(GetAddressResult());
            _countryRepository.Setup(cr => cr.GetAsync(It.IsAny<int>())).ReturnsAsync(GetCountryResult());

            var controller = new CustomerController(mockLogger.Object, commandHandler, queryHandler);

            // Act

            var response = await controller.GetCustomerAsync(1);

            // Assert
            var result = response.Result as OkObjectResult;
            Assert.IsNotNull(result);

            var customerObject = result.Value as CustomerDto;
            Assert.AreEqual("Test Name Test surname", customerObject.FullName);
        }

        private Customer GetCustomerResult()
        {
            return new Customer() { AddressId = 1, FirstName = "Test Name", Id = 1, LastName = "Test surname" };
        }

        private Address GetAddressResult()
        {
            return new Address() { Id = 1, City = "Test City", CountryId = 1, Postcode = "1234 AB", Street = "Test Street" };
        }

        private Country GetCountryResult()
        {
            return new Country() { Id = 1, Name = "Netherlands" };
        }
    }
}