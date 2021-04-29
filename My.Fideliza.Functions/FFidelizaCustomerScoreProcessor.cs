using Demo.Fideliza.Functions.Domain;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using My.Fideliza.Functions.Data.Entities;
using System;
using System.Text.Json;

namespace Demo.Fideliza.Functions
{
    public class FFidelizaCustomerScoreProcessor
    {
        private ICustomerDomain _customerDomain;

        public FFidelizaCustomerScoreProcessor(ICustomerDomain customerDomain)
        {
            _customerDomain = customerDomain;
        }

        [FunctionName("FCustomerScoreProcessor")]
        public void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation("---> Function EventGridTrigger running...!");

            Transaction transaction = ExtractTransaction(eventGridEvent);

            if (transaction != null)
            {
                log.LogInformation("[Customer Processor] Atualização de pontuação");
                _customerDomain.AddScorePoints(transaction.CustomerId, transaction.TransactionValue);
            }
            log.LogInformation("[Customer Processor] Falha na atualização de pontuação");
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

