using System.ComponentModel.DataAnnotations;

namespace NoResume.Models
{
    public class ShortBio
    {
        [Key]
        [Required]
        public string DeveloperId { get; set; }

        [Required]
        public string DeveloperName { get; set; }

        public string DeveloperPhoneNumber { get; set; }
        
        [DataType(DataType.Html)]
        [MinLength(25)]
        [MaxLength(2048)]
        [Display(Name = "Short Description about your goals")]
        public string ShortDescription { get; set; }
        
        [Display(Name = "Your Current State/Country")]
        public string CurrentCity { get; set; }
        
        [Display(Name = "Ready for Job? ")]
        public bool IsAvailableForJob { get; set; }
    }
}