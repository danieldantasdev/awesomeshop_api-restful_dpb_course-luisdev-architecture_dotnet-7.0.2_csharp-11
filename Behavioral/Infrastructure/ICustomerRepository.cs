using Behavioral.Core.Entities;

namespace Behavioral.Infrastructure;

public interface ICustomerRepository
{
    List<Customer> GetBlockedCustomers();
    Customer GetCustomerById(Guid id);
}