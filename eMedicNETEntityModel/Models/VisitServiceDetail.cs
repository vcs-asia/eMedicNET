using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class VisitServiceDetail
    {
        [Display(Name = "Visit ID"), Key, Column(Order = 0), Required(ErrorMessage = "{0} is required")]
        public int VsdVstid { get; set; }

        [ForeignKey("VsdVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Display(Name = "Service ID"), Required(ErrorMessage = "{0} is required")]
        public int VsdSerid { get; set; }

        [ForeignKey("VsdSerid")]
        public Service Service { get; set; } = null!;

        [Display(Name = "Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal VsdAmont { get; set; }

        [Display(Name = "Discount"), Required(ErrorMessage = "{0} is required")]
        public decimal VsdDcamt { get; set; }

        [Display(Name = "Net Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal VsdNtamt { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string VsdUsrid { get; set; } = null!;

        public DateTime VsdCdate { get; set; }
        public DateTime VsdUdate { get; set; }
    }

}
