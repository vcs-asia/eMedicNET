using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Receipt
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int RecAutid { get; set; }

        [Display(Name = "Receipt No."), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string RecRecno { get; set; } = null!;

        [Display(Name = "Patient ID"), Required(ErrorMessage = "{0} is required")]
        public int RecPatid { get; set; }

        [ForeignKey("RecPatid")]
        public Patient Patient { get; set; } = null!;

        [Display(Name = "Visit ID"), Required(ErrorMessage = "{0} is required")]
        public int RecVstid { get; set; }

        [ForeignKey("RecVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Display(Name = "Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime RecRecdt { get; set; }

        [Display(Name = "Total Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal RecTtamt { get; set; }

        [Display(Name = "Paid Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal RecPdamt { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string RecUsrid { get; set; } = null!;

        public DateTime RecCdate { get; set; }
        public DateTime RecUdate { get; set; }
    }

}
