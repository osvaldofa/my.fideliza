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
    public class FFidelizaTransactionsHttp
    {
        private ITransactionDomain _transactionDomain;

        public FFidelizaTransactionsHttp(ITransactionDomain transactionDomain)
        {
            _transactionDomain = transactionDomain;
        }

        [FunctionName("FTransactions")]
        public async Task<List<Transaction>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v2/transaction/{*status}")] HttpRequest req,
            ILogger log, string status)
        {
            log.LogInformation("---> Function Clientes invocada...");

            int statusCode = ExtractStatus(status);

            if (statusCode == 0)
            {
                log.LogInformation("[REQ] Path simples.");
                return _transactionDomain.GetAllTransaction();
            }
            else 
            {
                log.LogInformation("[REQ] Path composto - Transaction Fidelized Status: " + statusCode);
                return _transactionDomain.GetTransactionsByStatus(statusCode);              
            }
        }

        private static int ExtractStatus(string status)
        {try
            {
                return int.Parse(status);
            }
            catch (System.Exception)
            {
                return 0;
            }
        }
    }
}

