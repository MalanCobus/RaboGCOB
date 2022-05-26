using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.DataLayer.Models;
using System.Collections.Concurrent;

namespace Rabobank.TechnicalTest.GCOB.DataLayer.Repositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private ConcurrentDictionary<int, Customer> Customers { get; } = new ConcurrentDictionary<int, Customer>();
        private readonly ILogger _logger;

        public InMemoryCustomerRepository(ILogger<InMemoryCustomerRepository> logger)
        {
            _logger = logger;
        }
        
        public async Task<int> GenerateIdentityAsync()
        {
            _logger.LogDebug("Generating Customer identity");
            if (Customers.Count == 0) return 1;

            var x = Customers.Values.Max(c => c.Id);
            return ++x;
        }

        public async Task<Customer> InsertAsync(Customer customer)
        {
            customer.Id = await GenerateIdentityAsync();
            Customers.TryAdd(customer.Id, customer);
            _logger.LogDebug($"New customer inserted [ID:{customer.Id}]. " +
                          $"There are now {Customers.Count} legal entities in the store.");
            return customer;
        }

        public async Task<Customer> GetAsync(int identity)
        {
            _logger.LogDebug($"FindMany Customers with identity {identity}");

            if (!Customers.ContainsKey(identity)) throw new ArgumentException($"Customer not found with Id: {identity}");
            _logger.LogDebug($"Found Customer with identity {identity}");
            return Customers[identity];
        }

        public Task UpdateAsync(Customer customer)
        {
            if (!Customers.ContainsKey(customer.Id))
            {
                throw new Exception(
                    $"Cannot update customer with identity '{customer.Id}' " +
                    "as it doesn't exist");
            }

            Customers[customer.Id] = customer;
            _logger.LogDebug($"New customer updated [ID:{customer.Id}].");

            return Task.FromResult(customer);
        }
    }
}
