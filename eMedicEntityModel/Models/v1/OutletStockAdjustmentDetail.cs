using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class OutletStockAdjustmentDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int OsdAutid { get; set; }

        public int OsdAdjid { get; set; }

        [ForeignKey("OsdAdjid")]
        public OutletStockAdjustment? OutletStockAdjustmentInfo { get; set; }

        public int OsdStkid { get; set; }

        [ForeignKey("OsdStkid")]
        public Product? StockItem { get; set; }

        [Display(Name = "Variance")]
        public int OsdAdqty { get; set; }

        [Display(Name = "Current Quantity")] 
        public int OsdCrqty { get; set; }

        [Display(Name = "Packing")] 
        public int OsdIpack { get; set; }

        [Display(Name = "Big UOM"), StringLength(10)]
        public string OsdBguom { get; set; } = string.Empty;

        [Display(Name = "Small UOM"), StringLength(10)]
        public string OsdSmuom { get; set; } = string.Empty;

        [Display(Name = "Expiry Date")]
        public DateTime OsdExpdt { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string OsdUsrid { get; set; } = string.Empty;

        public DateTime OsdCdate { get; set; }
        public DateTime? OsdUdate { get; set; }
    }

}
