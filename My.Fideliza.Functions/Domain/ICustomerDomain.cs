using Demo.Fideliza.Functions.Data;
using System.Collections.Generic;

namespace Demo.Fideliza.Functions.Domain
{
    public interface ICustomerDomain
    {
        List<Customer> GetCustomers();
        Customer GetCustomer(int CustomerId);

        void AddScorePoints(int customerId, int Points);
    }
}
