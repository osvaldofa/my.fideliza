using Demo.Fideliza.Functions.Data;
using Demo.Fideliza.Functions.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using My.Fideliza.Functions.Data.Entities;
using My.Fideliza.Functions.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

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

