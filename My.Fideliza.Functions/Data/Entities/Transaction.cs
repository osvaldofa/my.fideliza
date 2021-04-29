namespace My.Fideliza.Functions.Data.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public int TransactionValue { get; set; }
        public int Fidelized { get; set; }
    }
}
