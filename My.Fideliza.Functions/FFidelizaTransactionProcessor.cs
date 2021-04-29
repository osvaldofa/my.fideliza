using Microsoft.AspNetCore.Http;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using My.Fideliza.Functions.Data.Entities;
using My.Fideliza.Functions.Domain;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Fideliza.Functions
{
    public class FFidelizaTransactionProcessor
    {
        private ITransactionDomain _transactionDomain;

        public FFidelizaTransactionProcessor(ITransactionDomain transactionDomain)
        {
            _transactionDomain = transactionDomain;
        }

        [FunctionName("FTransactionsProcessor")]
        public void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation("---> Function EventGridTrigger running...!");

            Transaction transaction = ExtractTransaction(eventGridEvent);

            if (transaction != null)
            {
                log.LogInformation("[REQ] Path simples.");
                _transactionDomain.AdjustTransactionStatus(transaction);
            }
        }

        private Transaction ExtractTransaction(EventGridEvent eventGridevent)
        {
            try
            {
                return JsonSerializer.Deserialize<Transaction>(eventGridevent.Data.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}

