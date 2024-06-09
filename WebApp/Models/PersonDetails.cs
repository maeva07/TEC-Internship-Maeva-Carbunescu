using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class PersonDetails
    {
        public int Id { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        public string PersonCity { get; set; }

        public int PersonId { get; set; }
    }
}
