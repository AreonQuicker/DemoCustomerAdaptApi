using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCustomerAdaptApi.Domain.Models;
using DemoCustomerAdaptApi.Domain.Models.DomainModels;

namespace DemoCustomerAdaptApi.Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDomainModel> GetAsync(int id);
        Task<IEnumerable<CustomerDomainModel>> GetAllAsync();
        Task<int> CreateAsync(CustomerCreateRequest customerCreateRequest);
        Task<int> UpdateAsync(int id, CustomerUpdateRequest customerUpdateRequest);
        Task<int> DeleteAsync(int id);
    }
}