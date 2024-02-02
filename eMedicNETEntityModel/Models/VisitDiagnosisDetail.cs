using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class VisitDiagnosisDetail
    {
        [Key, Column(Order = 0), Required(ErrorMessage = "{0} is required"), Display(Name = "Visit ID is required")]
        public int VddVstid { get; set; }

        [ForeignKey("VddVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Key, Column(Order = 1), Required(ErrorMessage = "{0} is required"), Display(Name = "Diagnosis ID is required")]
        public int VddDgnid { get; set; }

        [ForeignKey("VddDgnid")]
        public Diagnosis Diagnosis { get; set; } = null!;

        [Display(Name = "Remarks")]
        public string VddRmrks { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string VddUsrid { get; set; } = null!;

        public DateTime VddCdate { get; set; }
        public DateTime VddUdate { get; set; }
    }

}
