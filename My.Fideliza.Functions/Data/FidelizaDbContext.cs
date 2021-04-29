using Microsoft.EntityFrameworkCore;
using My.Fideliza.Functions.Data.Entities;

namespace Demo.Fideliza.Functions.Data
{
    public class FidelizaDbContext : DbContext
    {
        public FidelizaDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
