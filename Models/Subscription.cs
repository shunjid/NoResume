namespace NoResume.Models
{
    public class Subscription
    {
        public string DevId { get; set; }
        public string ServeReferenceCode { get; set; }
        public float AmountPaid { get; set; }
        public string TransactionId { get; set; }
        public string TimeStamp { get; set; }
    }
}