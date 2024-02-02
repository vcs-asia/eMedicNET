using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Appointment
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int AptAutid { get; set; }

        [Display(Name = "Doctor")]
        public int AptStfid { get; set; }

        [ForeignKey("AptStfid")]
        public Staff? Doctor { get; set; }

        [Display(Name = "Patient")]
        public int AptPatid { get; set; }

        [ForeignKey("AptPatid")]
        public Patient? Patient { get; set; }

        [Display(Name = "Appointment Date & Time")]
        public DateTime AptDattm { get; set; }

        [StringLength(150)]
        public string AptUsrid { get; set; } = string.Empty;

        public DateTime AptCdate { get; set; }

        public DateTime? AptUdate { get; set; }

    }

}
