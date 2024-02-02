using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockTransfer
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int StrAutid { get; set; }

        [Display(Name = "Ref No"), StringLength(10)]
        public string StrRefno { get; set; } = string.Empty;

        [Display(Name = "Date")]
        public DateTime StrTdate { get; set; }

        [Display(Name = "Outlet (From)")]
        public int StrOutfr { get; set; }

        [ForeignKey("StrOutfr")]
        public Outlet? OutletFrom { get; set; }

        [Display(Name = "Outlet (To)")]
        public int StrOutto { get; set; }

        [ForeignKey("StrOutto")]
        public Outlet? OutletTo { get; set; }

        [Display(Name = "Amount")]
        public string StrAmont { get; set; } = string.Empty;

        [Display(Name = "Remarks (if any)"), StringLength(255)]
        public string? StrRmrks { get; set; }

        [Display(Name = "Post Flag")]
        public bool StrPflag { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string StrUsrid { get; set; } = string.Empty;

        public DateTime StrCdate { get; set; }
        public DateTime? StrUdate { get; set; }
    }

}
