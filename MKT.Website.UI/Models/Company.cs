using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MKT.Website.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Company Name") ]
        public string CompanyName { get; set; }
        [Required]
        [DisplayName("Company Phone")]
        public string CompanyPhone { get; set; }
        [Required]
        [DisplayName("Company Email")]
        public string CompanyEmail { get; set; }
        [Required]
        [DisplayName("Looking For")]
        public string LookingFor { get; set; }
        [Required]
        public string Machine { get; set; }
        [Required]
        [DisplayName("Rent Period")]
        [Range(1,100,ErrorMessage = "Rent Period must be between 1 and 999 only!")]
        public string RentPeriod { get; set; }
        [DisplayName("Company Description")]
        public string CompanyDescription { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;


    }
}

