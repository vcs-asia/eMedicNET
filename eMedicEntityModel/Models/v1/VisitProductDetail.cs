using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class VisitProductDetail
    {
        [Display(Name = "Transaction ID")]
        public int VpdTrnid { get; set; }

        [Display(Name = "Visit ID")]
        public int VpdVstid { get; set; }

        [ForeignKey("VpdVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "Drug ID")]
        public int VpdStkid { get; set; }

        [ForeignKey("VpdStkid")]
        public Product? Product { get; set; }

        [Display(Name = "Description"), StringLength(150)]
        public string VpdSdesc { get; set; } = string.Empty;

        [Display(Name = "Unit Price")]
        public decimal VpdUcost { get; set; }

        [Display(Name = "Quantity")]
        public decimal VpdStqty { get; set; }

        [Display(Name = "Amount")]
        public decimal VpdStamt { get; set; }

        [Display(Name = "Charged")]
        public decimal VpdStchg { get; set; } = 0;

        [Display(Name = "Discount")]
        public decimal VpdDcamt { get; set; } = 0;

        [Display(Name = "Net")]
        public decimal VpdNtamt { get; set; } = 0;

        [Display(Name = "User ID"), StringLength(150)]
        public string VpdUsrid { get; set; } = string.Empty;

        public DateTime VpdCdate { get; set; }
        public DateTime? VpdUdate { get; set; }
    }

}
