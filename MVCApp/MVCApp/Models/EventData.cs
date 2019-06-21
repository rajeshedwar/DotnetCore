using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class EventData:Baseentity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override int Id { get; set; }

        [Required(ErrorMessage ="Title cannot be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Location cannot be empty")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}")]
        [Required(ErrorMessage = "Start Date cannot be empty")]
        public DateTime StartDate { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        [Required(ErrorMessage = "End Date cannot be empty")]
        public DateTime EndDate  { get; set; }

        [Required(ErrorMessage = "Speaker cannot be empty")]
        public string Speaker { get; set; }

        [Required(ErrorMessage = "Registration link cannot be empty")]
        [Display(Name ="Registration Link")]
        [DataType(DataType.Url,ErrorMessage ="Invalid url format")]
        public string Url { get; set; }
    }
}
