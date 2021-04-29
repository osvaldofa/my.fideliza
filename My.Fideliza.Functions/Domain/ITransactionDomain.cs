using My.Fideliza.Functions.Data.Entities;
using System.Collections.Generic;

namespace My.Fideliza.Functions.Domain
{
    public interface ITransactionDomain
    {
        List<Transaction> GetAllTransaction();
        List<Transaction> GetTransactionsByStatus(int status);

        void AdjustCustomerScoreByTransactions();

        void AdjustTransactionStatus(Transaction transaction);
                
    }
}
