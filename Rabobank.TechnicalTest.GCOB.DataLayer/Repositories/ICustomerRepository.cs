using Rabobank.TechnicalTest.GCOB.DataLayer.Models;

namespace Rabobank.TechnicalTest.GCOB.DataLayer.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> GenerateIdentityAsync();
        Task<Customer> InsertAsync(Customer customer);
        Task<Customer> GetAsync(int identity);
        Task UpdateAsync(Customer customer);
    }
}
