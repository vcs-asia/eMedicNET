using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Receipt
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int RecAutid { get; set; }

        [Display(Name = "Receipt No."), StringLength(10)]
        public string RecRecno { get; set; } = string.Empty;

        [Display(Name = "Patient ID")]
        public int RecPatid { get; set; }

        [ForeignKey("RecPatid")]
        public Patient? Patient { get; set; }

        [Display(Name = "Visit ID")]
        public int RecVstid { get; set; }

        [ForeignKey("RecVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "Date")]
        public DateTime RecRecdt { get; set; }

        [Display(Name = "Total Amount")]
        public decimal RecTtamt { get; set; }

        [Display(Name = "Paid Amount")]
        public decimal RecPdamt { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string RecUsrid { get; set; } = string.Empty;

        public DateTime RecCdate { get; set; }
        public DateTime? RecUdate { get; set; }
    }

}
