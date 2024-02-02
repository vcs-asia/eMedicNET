using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class OutletStockInfo
    {
        [Key, Column(Order = 0), Required(ErrorMessage = "{0} is required")]
        public int OsiOutid { get; set; }

        [ForeignKey("OsiOutid")]
        public Outlet Outlet { get; set; } = null!;

        [Key, Column(Order = 1), Required(ErrorMessage = "{0} is required")]
        public int OsiStkid { get; set; }

        [ForeignKey("OsiStkid")]
        public Product StockItem { get; set; } = null!;

        [Display(Name = "Reorder Level")]
        public int OsiRolvl { get; set; }

        [Display(Name = "Minimum Level")]
        public int OsiMllvl { get; set; }

        [Display(Name = "Quantity")]
        public int OsiCrqty { get; set; }

        [Display(Name = "Packing")]
        public int OsiIpack { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string OsiUsrid { get; set; } = null!;

        public DateTime OsiCdate { get; set; }
        public DateTime OsiUdate { get; set; }
    }

}
