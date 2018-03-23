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
        public int ID { get; set; }
        [MinLength(3)]
        [Required]
        [DisplayName("First Name")]
        public string Firstname { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Occupation")]
        public string Occupation { get; set; }
    }
}
