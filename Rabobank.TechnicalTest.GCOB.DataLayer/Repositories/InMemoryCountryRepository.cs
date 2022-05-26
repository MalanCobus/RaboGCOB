using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.DataLayer.Models;
using System.Collections.Concurrent;

namespace Rabobank.TechnicalTest.GCOB.DataLayer.Repositories
{
    public class InMemoryCountryRepository : ICountryRepository
    {
        private ConcurrentDictionary<int, Country> Countries { get; } = new ConcurrentDictionary<int, Country>();
        private ILogger _logger;

        public InMemoryCountryRepository(ILogger<InMemoryCountryRepository> logger)
        {
            _logger = logger;
            Countries.TryAdd(1, new Country { Id = 1, Name = "Netherlands" });
            Countries.TryAdd(2, new Country { Id = 2, Name = "Poland" });
            Countries.TryAdd(3, new Country { Id = 3, Name = "Ireland" });
            Countries.TryAdd(4, new Country { Id = 4, Name = "South Afrcia" });
            Countries.TryAdd(5, new Country { Id = 5, Name = "India" });
        }

        public Task<Country> GetAsync(int identity)
        {
            _logger.LogDebug($"Get Country with identity {identity}");

            if (!Countries.ContainsKey(identity)) throw new Exception($"Country not found with country Id : {identity}");
            _logger.LogDebug($"Found Country with identity {identity}");
            return Task.FromResult(Countries[identity]);
        }

        public Task<IEnumerable<Country>> GetAllAsync()
        {
            _logger.LogDebug($"Get all Countries");

            return Task.FromResult(Countries.Select(x => x.Value));
        }

    }

}
