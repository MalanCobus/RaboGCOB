using Rabobank.TechnicalTest.GCOB.DataLayer.Models;

namespace Rabobank.TechnicalTest.GCOB.DataLayer.Repositories
{
    public interface IAddressRepository
    {
        Task<int> GenerateIdentityAsync();
        Task<Address> InsertAsync(Address address);
        Task<Address> GetAsync(int identity);
        Task UpdateAsync(Address address);
    }
}
