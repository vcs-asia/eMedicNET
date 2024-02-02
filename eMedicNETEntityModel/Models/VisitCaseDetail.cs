using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class VisitCaseDetail
    {
        [Key, Column(Order = 0), Required(ErrorMessage = "{0} is required"), Display(Name = "Visit ID is required")]
        public int VcaVstid { get; set; }

        [ForeignKey("VcaVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Display(Name = "Symptoms")]
        public string VcaSmtom { get; set; } = null!;

        [Display(Name = "Findings")]
        public string VcaFinds { get; set; } = null!;

        [Display(Name = "Treatment Notes")]
        public string VcaTreat { get; set; } = null!;

        [Display(Name = "Suregery Notes")]
        public string VcaSurge { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string VcaUsrid { get; set; } = null!;

        public DateTime VcaCdate { get; set; }
        public DateTime VcaUdate { get; set; }
    }

}
