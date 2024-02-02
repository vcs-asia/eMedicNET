using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class VisitEyeFinding
    {
        [Column(Order = 0)]
        [Display(Name = "ID")]
        public int VefFndid { get; set; }

        [ForeignKey("VefFndid")]
        public EyeFinding? EyeFinding { get; set; }

        [Column(Order = 1)]
        [Display(Name = "Visit ID")]
        public int VefVstid { get; set; }

        [ForeignKey("VefVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "Left Eye")]
        public decimal VefLtres { get; set; }

        [Display(Name = "Right Eye")]
        public decimal VefRtres { get; set; }

        [StringLength(150)]
        public string VefUsrid { get; set; } = string.Empty;

        public DateTime VefCdate { get; set; }

        public DateTime? VefUdate { get; set; }
    }

}
