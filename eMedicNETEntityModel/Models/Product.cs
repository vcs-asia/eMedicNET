using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Product
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ProAutid { get; set; }

        [Display(Name = "Barcode/ Stock Code")]
        [StringLength(20)]
        [Required]
        public string ProBcode { get; set; } = null!;

        [Display(Name = "Name")]
        [StringLength(150)]
        [Required]
        public string ProSname { get; set; } = null!;

        [Display(Name = "Generic Name")]
        [StringLength(150)]
        [Required]
        public string ProGname { get; set; } = null!;

        [Display(Name = "Type")]
        [Required]
        public int ProDtype { get; set; }

        [ForeignKey("ProDtype")]
        public Parameter StockType { get; set; } = null!;

        [Display(Name = "Form")]
        [Required]
        public int ProIform { get; set; }

        [ForeignKey("ProIform")]
        public Parameter StockForm { get; set; } = null!;

        [Display(Name = "Cost Price")]
        [Required]
        public decimal ProScost { get; set; }

        [Display(Name = "Unit Cost")]
        [Required]
        public decimal ProUcost { get; set; }

        [Display(Name = "Big UOM")]
        [StringLength(10)]
        [Required]
        public string ProIbuom { get; set; } = null!;

        [Display(Name = "Small UOM")]
        [StringLength(10)]
        [Required]
        public string ProIsuom { get; set; } = null!;

        [Display(Name = "Selling Price (In)")]
        [Required]
        public decimal ProInsel { get; set; }

        [Display(Name = "Markup (In)")]
        [Required]
        public decimal ProInmrk { get; set; }

        [Display(Name = "Selling Price (Out)")]
        [Required]
        public decimal ProOtsel { get; set; }

        [Display(Name = "Markup (Out)")]
        [Required]
        public decimal ProOtmrk { get; set; }

        [Display(Name = "Packing")]
        [Required]
        public int ProIpack { get; set; }

        [Display(Name = "Reorder Level")]
        [Required]
        public int ProRolvl { get; set; }

        [Display(Name = "Minimum Level")]
        [Required]
        public int ProMnlvl { get; set; }

        [Display(Name = "Caution")]
        [StringLength(200)]
        [Required]
        public string ProCtion { get; set; } = null!;

        [Display(Name = "Status")]
        [Required]
        public bool ProStats { get; set; }

        [Display(Name = "Service")]
        [Required]
        public int ProSerid { get; set; }

        [ForeignKey("ProSerid")]
        public Service Service { get; set; } = null!;

        [Display(Name = "User ID")]
        [StringLength(150)]
        [Required]
        public string ProUsrid { get; set; } = null!;

        public DateTime ProCdate { get; set; }

        public DateTime ProUdate { get; set; }
    }
}
