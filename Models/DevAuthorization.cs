using System;
using System.ComponentModel.DataAnnotations;

namespace NoResume.Models
{
    public class DevAuthorization
    {
        [Key]
        public int AuthId { get; set; }

        public string AccessToken { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }
    }
}