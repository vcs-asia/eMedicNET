using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PatientVisit
    {
        [Key, Column(Order = 0), Display(Name = "Visit ID"), Required(ErrorMessage = "{0} is required")]
        public int PvtVstid { get; set; }

        [ForeignKey("PvtVstid")]
        public PatientQueue PatientQueue { get; set; } = null!;

        [Display(Name = "Patient ID"), Required(ErrorMessage = "{0} is required")]
        public int PvtPatid { get; set; }

        [ForeignKey("PvtPatid")]
        public Patient Patient { get; set; } = null!;

        [Display(Name = "Queue Date & Time")]
        public DateTime PvtQdate { get; set; }

        [Display(Name = "Queue Status")]
        [StringLength(20)]
        public string PvtQstat { get; set; } = null!;

        [Display(Name = "Visit Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime PvtVdate { get; set; }

        [Display(Name = "Visit Time"), Required(ErrorMessage = "{0} is required")]
        public TimeSpan PvtVtime { get; set; }

        [Display(Name = "Doctor ID"), Required(ErrorMessage = "{0} is required")]
        public int PvtDocid { get; set; }

        [Display(Name = "Remarks"), Required(ErrorMessage = "{0} is required"), StringLength(255)]
        public string PvtRmrks { get; set; } = null!;

        [Display(Name = "Total Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal PvtTtamt { get; set; }

        [Display(Name = "Panel ID"), Required(ErrorMessage = "{0} is required")]
        public int PvtPnlid { get; set; }

        [ForeignKey("PvtPnlid")]
        public Panel Panel { get; set; } = null!;

        [Display(Name = "Discipline"), Required(ErrorMessage = "{0} is required")]
        public int PvtDscid { get; set; }

        [ForeignKey("PvtDscid")]
        public Parameter Discipline { get; set; } = null!;

        [Display(Name = "Paid Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal PvtPdamt { get; set; }

        [Display(Name = "Discount"), Required(ErrorMessage = "{0} is required")]
        public decimal PvtDcamt { get; set; }

        [Display(Name = "Bill No."), Required(ErrorMessage = "{0} is required")]
        public string PvtBilno { get; set; } = null!;

        [Display(Name = "Status"), Required(ErrorMessage = "{0} is required")]
        public int PvtState { get; set; }

        [Display(Name = "Time Out")]
        public DateTime PvtTimot { get; set; }

        [Display(Name = "Appointment Date & Time"), Required(ErrorMessage = "{0} is required")]
        public DateTime PvtAptdt { get; set; }

        [Display(Name = "MC Days")]
        public int PvtMcday { get; set; }

        [Display(Name = "MC Date")]
        public DateTime PvtMcdat { get; set; }

        [Display(Name = "Weight"), Required(ErrorMessage = "{0} is required")]
        public int PvtWeigh { get; set; }

        [Display(Name = "Height"), Required(ErrorMessage = "{0} is required")]
        public int PvtHeigh { get; set; }

        [Display(Name = "BMI"), Required(ErrorMessage = "{0} is required")]
        public decimal PvtDcbmi { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PvtUsrid { get; set; } = null!;

        [StringLength(150)]
        public string PvtLuser { get; set; } = null!;

        public DateTime PvtCdate { get; set; }
        public DateTime PvtUdate { get; set; }
    }

}
