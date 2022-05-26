using Rabobank.TechnicalTest.GCOB.Api.Models.Customers;
using Rabobank.TechnicalTest.GCOB.Commands.Customers;
using Rabobank.TechnicalTest.GCOB.DataLayer.Models;
using Rabobank.TechnicalTest.GCOB.DataLayer.Repositories;

namespace Rabobank.TechnicalTest.GCOB.Commands.Handlers.Commands
{
    public class CustomerCommandHandler
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(IAddressRepository addressRepository, ICountryRepository countryRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
        }

        public async Task<CustomerDto> HandleAsync(AddCustomerCommand command)
        {
            var countryList = await _countryRepository.GetAllAsync();
            var country = countryList.FirstOrDefault(country => String.Equals(country.Name, command.Country, StringComparison.CurrentCultureIgnoreCase));

            if (country == null) throw new ArgumentException($"Specified country was not found.");

            //Validate address details here before inserting
            var address = await _addressRepository.InsertAsync(new Address { City = command.City, Postcode = command.PostalCode, CountryId = country.Id, Street = command.Street });

            //validate customer details here before inserting
            var customer = await _customerRepository.InsertAsync(new Customer { AddressId = address.Id, FirstName = command.FirstName, LastName = command.LastName });


            return new CustomerDto() { Id = customer.Id, City = address.City, Country = country.Name, FullName = customer.FullName, Postcode = address.Postcode, Street = address.Street };
        }
    }
}
