using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class VisitProductDetail
    {
        [Display(Name = "Transaction ID")]
        public int VpdTrnid { get; set; }

        [Display(Name = "Visit ID"), Required(ErrorMessage = "{0} is required")]
        public int VpdVstid { get; set; }

        [ForeignKey("VpdVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Display(Name = "Drug ID"), Required(ErrorMessage = "{0} is required")]
        public int VpdStkid { get; set; }

        [ForeignKey("VpdStkid")]
        public Product Product { get; set; } = null!;

        [Display(Name = "Description"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string VpdSdesc { get; set; } = null!;

        [Display(Name = "Unit Price")]
        public decimal VpdUcost { get; set; }

        [Display(Name = "Quantity")]
        public decimal VpdStqty { get; set; }

        [Display(Name = "Amount")]
        public decimal VpdStamt { get; set; }

        [Display(Name = "Charged")]
        public decimal VpdStchg { get; set; }

        [Display(Name = "Discount")]
        public decimal VpdDcamt { get; set; }

        [Display(Name = "Net")]
        public decimal VpdNtamt { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string VpdUsrid { get; set; } = null!;

        public DateTime VpdCdate { get; set; }
        public DateTime VpdUdate { get; set; }
    }

}
