using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomers(long? timestampFrom = null, long? timestampTo = null);
        Task<CustomerDto> GetCustomerById(int id);
        Task<CustomerDto> PutCustomer(int id, CustomerInput input);
        Task<CustomerDto> PostCustomer(CustomerInput input);
        Task<CustomerDto> DeleteCustomer(int id);
    }
}
