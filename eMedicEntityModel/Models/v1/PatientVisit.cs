using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace eMedicEntityModel.Models.v1
{
    public class PatientVisit
    {
        [Key, Column(Order = 0), Display(Name = "Visit ID")]
        public int PvtVstid { get; set; }

        [ForeignKey("PvtVstid")]
        public PatientQueue? PatientQueue { get; set; }

        [Display(Name = "Patient ID")]
        public int PvtPatid { get; set; }

        [ForeignKey("PvtPatid")]
        public Patient? Patient { get; set; }

        [Display(Name = "Visit Date")]
        public DateTime PvtVdate { get; set; }

        [Display(Name = "Visit Time")]
        public TimeSpan PvtVtime { get; set; }

        [Display(Name = "Doctor ID")]
        public int PvtDocid { get; set; }

        [Display(Name = "Remarks"), StringLength(255)]
        public string? PvtRmrks { get; set; }

        [Display(Name = "Panel ID")]
        public int PvtPnlid { get; set; }

        [ForeignKey("PvtPnlid")]
        public Panel? Panel { get; set; }

        [Display(Name = "Discipline")]
        public int PvtDscid { get; set; }

        [ForeignKey("PvtDscid")]
        public Parameter? Discipline { get; set; }

        [Display(Name = "Total Amount")]
        public decimal PvtTtamt { get; set; }

        [Display(Name = "Discount")]
        public decimal PvtDcamt { get; set; } = 0;

        [Display(Name = "Paid Amount")]
        public decimal PvtPdamt { get; set; }

        [Display(Name = "Bill No.")]
        public string? PvtBilno { get; set; }

        [Display(Name = "MC Days")]
        public int PvtMcday { get; set; }

        [Display(Name = "MC Date")]
        public DateTime PvtMcdat { get; set; }

        [Display(Name = "Weight")]
        public decimal PvtWeigh { get; set; } = 0;

        [Display(Name = "Height")]
        public decimal PvtHeigh { get; set; } = 0;

        [Display(Name = "BMI")]
        public decimal PvtDcbmi { get; set; } = 0;

        [Display(Name = "User ID"), StringLength(150)]
        public string PvtUsrid { get; set; } = string.Empty;

        public DateTime PvtCdate { get; set; }

        public DateTime? PvtUdate { get; set; }
    }

}
