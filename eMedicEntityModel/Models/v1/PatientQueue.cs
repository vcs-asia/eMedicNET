using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PatientQueue
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PtqAutid { get; set; }

        
        [Display(Name = "Patient ID")]
        public int PtqPatid { get; set; }

        [ForeignKey("PtqPatid")]
        public Patient? Patient { get; set; } 
        
        [Display(Name = "Queue Date/Time")]
        public DateTime PtqQdttm { get; set; }
        
        [Display(Name = "Time In")]
        public DateTime PtqIntme { get; set; }
        
        [Display(Name = "Time Out")]
        public DateTime? PtqTmout { get; set; }
        
        [Display(Name = "Status")]
        public string PtqState { get; set; } = "Waiting";
        
        [Display(Name = "Discipline")]
        public int PtqDscid { get; set; }

        [ForeignKey("PtqDscid")]
        public Parameter? Discipline { get; set; }

        public string PtqUsrid { get; set; } = string.Empty;

        public DateTime PtqCdate { get; set; }
        public DateTime? PtqUdate { get; set; }
    }
}
