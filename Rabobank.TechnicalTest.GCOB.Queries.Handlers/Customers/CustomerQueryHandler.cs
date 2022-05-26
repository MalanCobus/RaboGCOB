using Rabobank.TechnicalTest.GCOB.Api.Models.Customers;
using Rabobank.TechnicalTest.GCOB.DataLayer.Repositories;
using Rabobank.TechnicalTest.GCOB.Queries.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Queries.Handlers.Customers
{
    public class CustomerQueryHandler
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerQueryHandler(IAddressRepository addressRepository, ICountryRepository countryRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
        }

        public async Task<CustomerDto> HandleAsync(CustomerQuery query)
        {
            var customer = await _customerRepository.GetAsync(query.CustomerId);
            var address = await _addressRepository.GetAsync(customer.AddressId);
            var country = await _countryRepository.GetAsync(address.CountryId);

            return new CustomerDto() { Id = customer.Id, City = address.City, Country = country.Name, FullName = customer.FullName, Postcode = address.Postcode, Street = address.Street };
        }
    }
}
