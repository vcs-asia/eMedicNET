using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class OutletStockMovement
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int OsmAutid { get; set; }

        [Display(Name = "Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime OsmTDate { get; set; }

        [Display(Name = "Ref No"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string OsmRefNo { get; set; } = null!;

        [Display(Name = "ID"), Required(ErrorMessage = "{0} is required")]
        public int OsmStkid { get; set; }

        [ForeignKey("OsmStkid")]
        public Product StockItem { get; set; } = null!;

        [Display(Name = "Type"), Required(ErrorMessage = "{0} is required"), StringLength(3)]
        public string OsmTtype { get; set; } = null!;

        [Display(Name = "In")]
        public int OsmQtyin { get; set; }

        [Display(Name = "Out")]
        public int OsmQtyot { get; set; }

        [Display(Name = "Balance")]
        public int OsmQtbal { get; set; }

        [Display(Name = "Outlet ID"), Required(ErrorMessage = "{0} is required")]
        public int OsmOutid { get; set; }

        [ForeignKey("OsmOutid")]
        public Outlet Outlet { get; set; } = null!;

        [Display(Name = "Packing")]
        public int OsmIpack { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string OsmUsrid { get; set; } = null!;

        public DateTime OsmCdate { get; set; }
        public DateTime OsmUdate { get; set; }
    }

}
