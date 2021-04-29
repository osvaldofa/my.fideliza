using Demo.Fideliza.Functions.Data;
using System;

namespace My.Fideliza.Functions.Application
{
    public class CustomerScoreEvent
    {
		public string Id { get; }
		public string Subject { get; set; }
		public string EventType { get; }
		public string EventTime { get; }
		public Customer Data { get; set; }

		public CustomerScoreEvent()
		{
			Id = Guid.NewGuid().ToString();
			EventType = "customer.score";
			Subject = "fideliza/customer";
			EventTime = DateTime.UtcNow.ToString("o");
		}
	}
}
