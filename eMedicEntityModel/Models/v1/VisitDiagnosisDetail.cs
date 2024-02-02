using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class VisitDiagnosisDetail
    {
        [Key, Column(Order = 0), Display(Name = "Visit ID is required")]
        public int VddVstid { get; set; }

        [ForeignKey("VddVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Key, Column(Order = 1), Display(Name = "Diagnosis ID is required")]
        public int VddDgnid { get; set; }

        [ForeignKey("VddDgnid")]
        public Diagnosis? Diagnosis { get; set; }

        [Display(Name = "Remarks")]
        public string? VddRmrks { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string VddUsrid { get; set; } = string.Empty;

        public DateTime VddCdate { get; set; }
        public DateTime? VddUdate { get; set; }
    }

}
