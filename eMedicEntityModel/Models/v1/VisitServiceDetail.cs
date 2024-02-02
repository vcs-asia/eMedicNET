using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class VisitServiceDetail
    {
        [Display(Name = "Visit ID"), Key, Column(Order = 0)]
        public int VsdVstid { get; set; }

        [ForeignKey("VsdVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "Service ID")]
        public int VsdSerid { get; set; }

        [ForeignKey("VsdSerid")]
        public Service? Service { get; set; }

        [Display(Name = "Amount")]
        public decimal VsdAmont { get; set; }

        [Display(Name = "Discount")]
        public decimal VsdDcamt { get; set; } = 0;

        [Display(Name = "Net Amount")]
        public decimal VsdNtamt { get; set; } = 0;

        [Display(Name = "User ID"), StringLength(150)]
        public string VsdUsrid { get; set; } = string.Empty;

        public DateTime VsdCdate { get; set; }
        public DateTime? VsdUdate { get; set; }
    }

}
