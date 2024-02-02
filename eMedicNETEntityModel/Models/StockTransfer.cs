using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockTransfer
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int StrAutid { get; set; }

        [Display(Name = "Ref No"), StringLength(10), Required(ErrorMessage = "Ref No is required"),]
        public string StrRefno { get; set; } = null!;

        [Display(Name = "Date"), Required(ErrorMessage = "Date is required")]
        public DateTime StrTdate { get; set; }

        [Display(Name = "Outlet (From)"), Required(ErrorMessage = "{0} is required")]
        public int StrOutfr { get; set; }

        [ForeignKey("StrOutfr")]
        public Outlet OutletFrom { get; set; } = null!;

        [Display(Name = "Outlet (To)"), Required(ErrorMessage = "{0} is required")]
        public int StrOutto { get; set; }

        [ForeignKey("StrOutto")]
        public Outlet OutletTo { get; set; } = null!;

        [Display(Name = "Amount"), Required(ErrorMessage = "{0} is required")]
        public string StrAmont { get; set; } = null!;

        [Display(Name = "Remarks (if any)"), StringLength(255)]
        public string StrRmrks { get; set; } = null!;

        [Display(Name = "Post Flag")]
        public bool StrPflag { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string StrUsrid { get; set; } = null!;

        public DateTime StrCdate { get; set; }
        public DateTime StrUdate { get; set; }
    }

}
