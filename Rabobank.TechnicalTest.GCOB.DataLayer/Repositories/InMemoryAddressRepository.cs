using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.DataLayer.Models;
using System.Collections.Concurrent;

namespace Rabobank.TechnicalTest.GCOB.DataLayer.Repositories
{
    public class InMemoryAddressRepository : IAddressRepository
    {
        private ConcurrentDictionary<int, Address> Addresses { get; } = new ConcurrentDictionary<int, Address>();
        private readonly ILogger _logger;

        public InMemoryAddressRepository(ILogger<InMemoryAddressRepository> logger)
        {
            _logger = logger;
        }

        public async Task<int> GenerateIdentityAsync()
        {
            _logger.LogDebug("Generating Address identity");
            if (Addresses.Count == 0) return 1;

            var x = Addresses.Values.Max(c => c.Id);
            return ++x;
        }

        public async Task<Address> GetAsync(int identity)
        {
            _logger.LogDebug($"Find Address with identity {identity}");

            if (!Addresses.ContainsKey(identity)) throw new Exception($"Address not found with address Id : {identity}");
            _logger.LogDebug($"Found Address with identity {identity}");
            return Addresses[identity];
        }

        public async Task<Address> InsertAsync(Address address)
        {
            address.Id = await GenerateIdentityAsync();

            Addresses.TryAdd(address.Id, address);
            _logger.LogDebug($"New address inserted [ID:{address.Id}]. " +
                          $"There are now {Addresses.Count} legal entities in the store.");
            
            return address;
        }

        public async Task UpdateAsync(Address address)
        {
            throw new System.NotImplementedException();
        }
    }
}
