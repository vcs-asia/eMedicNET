using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class VisitChildDetail
    {
        [Key, Column(Order = 0), Required(ErrorMessage = "{0} is required"), Display(Name = "Visit ID is required")]
        public int VcdVstid { get; set; }

        [ForeignKey("VcdVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Display(Name = "Weight")]
        public decimal VcdWight { get; set; }

        [Display(Name = "Length")]
        public decimal VcdLngth { get; set; }

        [Display(Name = "Age")]
        public decimal VcdChage { get; set; }

        [Display(Name = "Head Circumference")]
        public decimal VcdHeadc { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string VcdUsrid { get; set; } = null!;

        public DateTime VcdCdate { get; set; }
        public DateTime VcdUdate { get; set; }
    }

}
