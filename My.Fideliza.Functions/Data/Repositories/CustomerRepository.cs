using System.Collections.Generic;
using System.Linq;

namespace Demo.Fideliza.Functions.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private FidelizaDbContext _fidelizaDbContext;

        public CustomerRepository(FidelizaDbContext dbContext)
        {
            _fidelizaDbContext = dbContext;
        }

        public List<Customer> FindAll()
        {
            return _fidelizaDbContext.Customers.ToList();
        }

        public Customer FindById(int Id)
        {
            return _fidelizaDbContext.Customers.First(c => c.Id == Id);
        }

        public void AddScorePointsToCustomer(int CustomerId, int Points)
        {
            Customer customer = _fidelizaDbContext.Customers.First(c => c.Id == CustomerId);
            if (customer != null)
            {
                customer.Score += Points;

                _fidelizaDbContext.Customers.Update(customer);
                _fidelizaDbContext.SaveChanges();
            }
        }
    }
}
