
using Rabobank.TechnicalTest.GCOB.DataLayer.Models;

namespace Rabobank.TechnicalTest.GCOB.DataLayer.Repositories
{
    public interface ICountryRepository
    {
        Task<Country> GetAsync(int identity);
        Task<IEnumerable<Country>> GetAllAsync();

        Task<Country> FindAsync(string countryName);
    }
}
