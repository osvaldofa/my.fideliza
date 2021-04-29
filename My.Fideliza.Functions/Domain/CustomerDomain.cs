using Demo.Fideliza.Functions.Data;
using System.Collections.Generic;

namespace Demo.Fideliza.Functions.Domain
{
    public class CustomerDomain : ICustomerDomain
    {
        public ICustomerRepository _customerRepository;

        public CustomerDomain(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer GetCustomer(int CustomerId)
        {
            return _customerRepository.FindById(CustomerId);
        }

        public List<Customer> GetCustomers()
        {
            return _customerRepository.FindAll();
        }

        public void AddScorePoints(int customerId, int points)
        {
            _customerRepository.AddScorePointsToCustomer(customerId, points);
        }
    }
}
