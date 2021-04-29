using My.Fideliza.Functions.Data.Entities;
using My.Fideliza.Functions.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace My.Fideliza.Functions.Domain
{
    public class TransactionDomain : ITransactionDomain
    {
        private ITransactionRepository _transactionRepository;

        public TransactionDomain(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public List<Transaction> GetAllTransaction()
        {
            return _transactionRepository.FindAll();
        }

        public List<Transaction> GetTransactionsByStatus(int status)
        {
            return _transactionRepository.FindByStatus(status);
        }

        public void AdjustTransactionStatus(Transaction transaction)
        {
            _transactionRepository.UpdateTransactionStatus(transaction.TransactionId, transaction.Fidelized);
        }

        public void AdjustCustomerScoreByTransactions() 
        {
            // Identifica as transações não pontuadas
            List<Transaction> transactions = GetTransactionsByStatus(0);

            Parallel.ForEach(transactions, t =>
                    {
                        t.TransactionValue = CalculateScoreByTransactionValue(t);
                        t.Fidelized = 1;

                        SendTransactionEvent(t);
                        SendCustomerScoreEvent(t);
                    }
                );
        }

        private void SendTransactionEvent(Transaction transaction)
        {
            var httpClient = new HttpClient();
            string endpoint = Environment.GetEnvironmentVariable("TOPIC_TRANSACTION_ENDPOINT");
            httpClient.DefaultRequestHeaders.Add("aeg-sas-key", Environment.GetEnvironmentVariable("TOPIC_TRANSACTION_KEY"));

            TransactionEvent transactionEvent = new TransactionEvent();
            transactionEvent.Data = transaction;

            var json = JsonSerializer.Serialize(transactionEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.PostAsync(endpoint, content);
        }

        private void SendCustomerScoreEvent(Transaction transaction)
        {
            var httpClient = new HttpClient();
            string endpoint = Environment.GetEnvironmentVariable("TOPIC_CUSTOMER_ENDPOINT");
            httpClient.DefaultRequestHeaders.Add("aeg-sas-key", Environment.GetEnvironmentVariable("TOPIC_CUSTOMER_KEY"));

            TransactionEvent transactionEvent = new TransactionEvent();
            transactionEvent.Data = transaction;

            var json = JsonSerializer.Serialize(transactionEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.PostAsync(endpoint, content);
        }

        private int CalculateScoreByTransactionValue(Transaction transaction)
        {
            return Math.Abs(transaction.TransactionValue / 3);
        }
    }
}
