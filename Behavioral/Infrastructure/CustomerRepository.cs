using Behavioral.Core.Entities;

namespace Behavioral.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    public List<Customer> GetBlockedCustomers()
    {
        return new List<Customer>
        {
            new("Fulano 1", DateTime.Now.AddYears(-20), "luis1@mail.com"),
            new("Fulano 2", DateTime.Now.AddYears(-30), "luis2@mail.com"),
            new("Fulano 3", DateTime.Now.AddYears(-40), "luis3@mail.com")
        };
    }

    public Customer GetCustomerById(Guid id)
    {
        return new Customer("Luis", DateTime.Now.AddYears(-30), "luis@mail.com");
    }
}