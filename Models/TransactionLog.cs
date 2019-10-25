using System.ComponentModel.DataAnnotations;
namespace NoResume.Models
{
    public class TransactionLog
    {
	[Key]
        public string DevId { get; set; }
        public string OtpTransactionId { get; set; }
        public string PhoneNumber { get; set; }
        public string OtpTimeStamp { get; set; }
    }
}