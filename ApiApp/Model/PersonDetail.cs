using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiApp.Model
{
    public class PersonDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        public string PersonCity { get; set; }
      
        [ForeignKey("Person")]
        public int PersonId { get; set; }
    }
}