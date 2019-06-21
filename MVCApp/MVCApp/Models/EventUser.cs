using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class EventUser : Baseentity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override int Id { get; set; }

        [Microsoft.AspNetCore.Mvc.HiddenInput(DisplayValue =false)]
        public int EventId { get; set; }

        [ReadOnly(true)]
        public string EventName { get; set; }

        [Required(ErrorMessage = "FirstName cannot be empty")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName cannot be empty")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Company cannot be empty")]
        public string Company { get; set; }

        [Required(ErrorMessage = "ContactNo cannot be empty")]
        [Display(Name = "contact No")]
        [DataType(DataType.PhoneNumber)]
        public int ContactNo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
