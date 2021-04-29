using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using My.Fideliza.Functions.Domain;

namespace Demo.Fideliza.Functions
{
    public class FFidelizaTransactionsAdjustJob
    {
        private ITransactionDomain _transactionDomain;

        public FFidelizaTransactionsAdjustJob(ITransactionDomain transactionDomain)
        {
            _transactionDomain = transactionDomain;
        }

        [FunctionName("FTransactionsAdjustTimer")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log, string status)
        {
            log.LogInformation("---> Time Trigger Function Adjusting Transactions");

            if (myTimer.IsPastDue)
                log.LogInformation("---> Function is running late!");

            _transactionDomain.AdjustCustomerScoreByTransactions();
        }
    }
}

