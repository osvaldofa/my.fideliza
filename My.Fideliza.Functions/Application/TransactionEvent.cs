using My.Fideliza.Functions.Data.Entities;
using System;

namespace My.Fideliza.Functions
{
    public class TransactionEvent
    {		
		public string Id { get; }
		public string Subject { get; set; }
		public string EventType { get; }
		public string EventTime { get; }
		public Transaction Data {get; set; }
				
		public TransactionEvent()
		{
			Id = Guid.NewGuid().ToString();
			EventType = "transaction.complete";
			Subject = "fideliza/transaction";
			EventTime = DateTime.UtcNow.ToString("o");
		}
		
	}
}
