using System.ComponentModel.DataAnnotations;

namespace NoResume.Models
{
    public class Dev
    {
        public ShortBio ShortBio { get; set; }

        public WorkingProfile WorkingProfile { get; set; }
    }
}