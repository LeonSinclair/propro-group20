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
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Occupation")]
        public string Occupation { get; set; }

        // Bank Details
        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }

        [DisplayName("IBAN")]
        public string IBAN { get; set; }

        [DisplayName("Sort Code")]
        public int SortCode { get; set; }

        public virtual ICollection<CheckboxViewModel> Modules { get; set; }

        public virtual ICollection<SubordinateModule> SubordinateModules { get; set; }
    }
}
