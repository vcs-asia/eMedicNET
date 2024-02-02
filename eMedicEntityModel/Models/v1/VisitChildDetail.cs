using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class VisitChildDetail
    {
        [Key, Column(Order = 0), Display(Name = "Visit ID is required")]
        public int VcdVstid { get; set; }

        [ForeignKey("VcdVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "Weight")]
        public decimal VcdWight { get; set; }

        [Display(Name = "Length")]
        public decimal VcdLngth { get; set; }

        [Display(Name = "Age")]
        public decimal VcdChage { get; set; }

        [Display(Name = "Head Circumference")]
        public decimal VcdHeadc { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string VcdUsrid { get; set; } = string.Empty;

        public DateTime VcdCdate { get; set; }
        public DateTime? VcdUdate { get; set; }
    }

}
