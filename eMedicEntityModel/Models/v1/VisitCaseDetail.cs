using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class VisitCaseDetail
    {
        [Key, Column(Order = 0), Display(Name = "Visit ID is required")]
        public int VcaVstid { get; set; }

        [ForeignKey("VcaVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "Symptoms")]
        public string? VcaSmtom { get; set; }

        [Display(Name = "Findings")]
        public string? VcaFinds { get; set; }

        [Display(Name = "Treatment Notes")]
        public string? VcaTreat { get; set; }

        [Display(Name = "Suregery Notes")]
        public string? VcaSurge { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string VcaUsrid { get; set; } = string.Empty;

        public DateTime VcaCdate { get; set; }
        public DateTime? VcaUdate { get; set; }
    }

}
