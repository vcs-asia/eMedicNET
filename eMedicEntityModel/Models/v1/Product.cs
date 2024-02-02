using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Product
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ProAutid { get; set; }

        [Display(Name = "Barcode/ Stock Code")]
        [StringLength(100)]
        public string? ProBcode { get; set; }

        [Display(Name = "Name")]
        [StringLength(250)]
        public string ProSname { get; set; } = string.Empty;

        [Display(Name = "Generic Name")]
        [StringLength(250)]
        public string? ProGname { get; set; }

        [Display(Name = "Type")]
        public int ProDtype { get; set; }

        [ForeignKey("ProDtype")]
        public Parameter? StockType { get; set; }

        [Display(Name = "Form")]
        public int ProIform { get; set; }

        [ForeignKey("ProIform")]
        public Parameter? StockForm { get; set; }

        [Display(Name = "Cost Price")]
        public decimal ProScost { get; set; }

        [Display(Name = "Unit Cost")]
        public decimal ProUcost { get; set; }

        [Display(Name = "Big UOM")]
        [StringLength(20)]
        public string ProIbuom { get; set; } = string.Empty;

        [Display(Name = "Small UOM")]
        [StringLength(20)]
        public string ProIsuom { get; set; } = string.Empty;

        [Display(Name = "Selling Price")]
        public decimal ProOtsel { get; set; }

        [Display(Name = "Markup")]
        public decimal ProOtmrk { get; set; }

        [Display(Name = "Packing")]
        public int ProIpack { get; set; }

        [Display(Name = "Reorder Level")]
        public int ProRolvl { get; set; }

        [Display(Name = "Minimum Level")]
        public int ProMnlvl { get; set; }

        [Display(Name = "Caution")]
        [StringLength(255)]
        public string? ProCtion { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool ProStats { get; set; }

        [Display(Name = "Service")]
        public int ProSerid { get; set; }

        [ForeignKey("ProSerid")]
        public Service? Service { get; set; }

        [Display(Name = "User ID")]
        [StringLength(150)]
        public string ProUsrid { get; set; } = string.Empty;

        public DateTime ProCdate { get; set; }

        public DateTime? ProUdate { get; set; }
    }
}
