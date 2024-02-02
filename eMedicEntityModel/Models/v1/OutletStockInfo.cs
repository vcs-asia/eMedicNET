using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class OutletStockInfo
    {
        [Key, Column(Order = 0)]
        public int OsiOutid { get; set; }

        [ForeignKey("OsiOutid")]
        public Outlet? Outlet { get; set; }

        [Key, Column(Order = 1)]
        public int OsiStkid { get; set; }

        [ForeignKey("OsiStkid")]
        public Product? StockItem { get; set; }

        [Display(Name = "Reorder Level")]
        public int OsiRolvl { get; set; }

        [Display(Name = "Minimum Level")]
        public int OsiMllvl { get; set; }

        [Display(Name = "Quantity")]
        public int OsiCrqty { get; set; }

        [Display(Name = "Packing")]
        public int OsiIpack { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string OsiUsrid { get; set; } = string.Empty;

        public DateTime OsiCdate { get; set; }
        public DateTime? OsiUdate { get; set; }
    }

}
