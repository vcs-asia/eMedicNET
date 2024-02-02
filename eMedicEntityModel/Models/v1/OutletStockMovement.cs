using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class OutletStockMovement
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int OsmAutid { get; set; }

        [Display(Name = "Date")]
        public DateTime OsmTDate { get; set; }

        [Display(Name = "Ref No"), StringLength(10)]
        public string OsmRefNo { get; set; } = string.Empty;

        [Display(Name = "ID")]
        public int OsmStkid { get; set; }

        [ForeignKey("OsmStkid")]
        public Product? StockItem { get; set; }

        [Display(Name = "Type"), StringLength(3)]
        public string OsmTtype { get; set; } = string.Empty;

        [Display(Name = "In")]
        public int OsmQtyin { get; set; }

        [Display(Name = "Out")]
        public int OsmQtyot { get; set; } 

        [Display(Name = "Balance")]
        public int OsmQtbal { get; set; }

        [Display(Name = "Outlet ID")]
        public int OsmOutid { get; set; }

        [ForeignKey("OsmOutid")]
        public Outlet? Outlet { get; set; }

        [Display(Name = "Packing")]
        public int OsmIpack { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string OsmUsrid { get; set; } = string.Empty;

        public DateTime OsmCdate { get; set; }
        public DateTime? OsmUdate { get; set; }
    }

}
