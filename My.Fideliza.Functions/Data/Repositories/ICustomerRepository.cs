namespace Demo.Fideliza.Functions.Data
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        void AddScorePointsToCustomer(int CustomerId, int Points);
    }
}
