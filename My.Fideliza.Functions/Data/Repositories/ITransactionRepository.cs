using Demo.Fideliza.Functions.Data;
using My.Fideliza.Functions.Data.Entities;
using System.Collections.Generic;

namespace My.Fideliza.Functions.Data.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        List<Transaction> FindByStatus(int status);

        void UpdateTransactionStatus(int TransactionId, int Status);
    }
}
