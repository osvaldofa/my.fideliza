using Demo.Fideliza.Functions.Data;
using My.Fideliza.Functions.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace My.Fideliza.Functions.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private FidelizaDbContext _fidelizaDbContext;
                
        public TransactionRepository(FidelizaDbContext dbContext)
        {
            _fidelizaDbContext = dbContext;
        }

        public List<Transaction> FindAll()
        {
            return _fidelizaDbContext.Transactions.ToList();
        }

        public Transaction FindById(int Id)
        {
            return _fidelizaDbContext.Transactions.First(t => t.TransactionId == Id);
        }

        public List<Transaction> FindByStatus(int status)
        {
            return _fidelizaDbContext.Transactions.Where(t => t.Fidelized == status).ToList();
        }

        public void UpdateTransactionStatus(int TransactionId, int Status)
        {
            Transaction transaction = _fidelizaDbContext.Transactions.First(t => t.TransactionId == TransactionId);
            if (transaction != null)
            {
                transaction.Fidelized = Status;

                _fidelizaDbContext.Transactions.Update(transaction);
                _fidelizaDbContext.SaveChanges();
            }
        }
    }
}
