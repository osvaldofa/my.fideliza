using Demo.Fideliza.Functions.Data;
using Demo.Fideliza.Functions.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Fideliza.Functions
{
    public class FFidelizaCustomersHttp
    {
        private ICustomerDomain _customerDomain;

        public FFidelizaCustomersHttp(ICustomerDomain customerDomain)
        {
            _customerDomain = customerDomain;
        }

        [FunctionName("FClientes")]
        public async Task<List<Customer>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v2/customer/{*customerId}")] HttpRequest req,
            ILogger log, string customerId)
        {
            log.LogInformation("---> Function Clientes invocada...");

            int id = ExtractId(customerId);

            if (id == 0)
            {
                log.LogInformation("[REQ] Path simples.");
                return _customerDomain.GetCustomers();
            }
            else 
            {
                log.LogInformation("[REQ] Path composto - Customer Id: " + id);
                Customer customer = _customerDomain.GetCustomer(id);
                if (customer != null)
                    return new List<Customer>() { customer };                
            }

            return null;
        }

        private static int ExtractId(string Id)
        {try
            {
                return int.Parse(Id);
            }
            catch (System.Exception)
            {
                return 0;
            }
        }
    }
}

