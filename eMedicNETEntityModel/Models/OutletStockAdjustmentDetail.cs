using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class OutletStockAdjustmentDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int OsdAutid { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int OsdAdjid { get; set; }

        [ForeignKey("OsdAdjid")]
        public OutletStockAdjustment OutletStockAdjustmentInfo { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required")]
        public int OsdStkid { get; set; }

        [ForeignKey("OsdStkid")]
        public Product StockItem { get; set; } = null!;

        [Display(Name = "Variance")]
        public int OsdAdqty { get; set; }

        [Display(Name = "Current Quantity")]
        public int OsdCrqty { get; set; }

        [Display(Name = "Packing")]
        public int OsdIpack { get; set; }

        [Display(Name = "Big UOM"), StringLength(10)]
        public string OsdBguom { get; set; } = null!;

        [Display(Name = "Small UOM"), StringLength(10)]
        public string OsdSmuom { get; set; } = null!;

        [Display(Name = "Expiry Date")]
        public DateTime OsdExpdt { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string OsdUsrid { get; set; } = null!;

        public DateTime OsdCdate { get; set; }
        public DateTime OsdUdate { get; set; }
    }

}
