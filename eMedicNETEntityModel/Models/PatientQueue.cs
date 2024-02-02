using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PatientQueue
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PtqAutid { get; set; }

        [Required]
        [Display(Name = "Patient ID")]
        public int PtqPatid { get; set; }

        [ForeignKey("PtqPatid")]
        public Patient Patient { get; set; } = null!;

        [Required]
        [Display(Name = "Queue Date/Time")]
        public DateTime PtqQdttm { get; set; }

        [Required]
        [Display(Name = "Time In")]
        public DateTime PtqIntme { get; set; }

        [Required]
        [Display(Name = "Time Out")]
        public DateTime PtqTmout { get; set; }

        [Required]
        [Display(Name = "Status")]
        public DateTime PtqState { get; set; }

        [Required]
        [Display(Name = "Discipline")]
        public int PtqDscid { get; set; }

        [ForeignKey("PtqDscid")]
        public Parameter Discipline { get; set; } = null!;

        public string PtqUsrid { get; set; } = null!;

        public DateTime PtqCdate { get; set; }
        public DateTime PtqUdate { get; set; }
    }
}
