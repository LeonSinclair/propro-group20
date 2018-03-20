using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class Subordinates
    {
        [Required]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MinLength(3)]
        [Required]
        [DisplayName("First Name")]
        public string firstname { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string surname { get; set; }
        [Required]
        [DisplayName("Occupation")]
        public string occupation { get; set; }
    }
}
