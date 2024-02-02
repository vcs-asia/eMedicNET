using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockPurchaseOrderDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PdsAutid { get; set; }

        [Display(Name = "PO Id")]
        public int PdsIpoid { get; set; }

        [ForeignKey("PdsIpoid")]
        public StockPurchaseOrder? StockPurchaseOrder { get; set; }

        [Display(Name = "Stock ID")]
        public int PdsStkid { get; set; }

        [Display(Name = "Ordered")]
        public int PdsOrdqt { get; set; }

        [Display(Name = "Cost")]
        public decimal PdsScost { get; set; }

        [Display(Name = "Amount")]
        public decimal PdsAmont { get; set; }

        [Display(Name = "Packing"), StringLength(10)]
        public int PdsIpack { get; set; }

        [Display(Name = "Description")]
        public string PdsSdesc { get; set; } = string.Empty;

        [Display(Name = "Cancelled")]
        public int PdsClqty { get; set; } = 0;

        [Display(Name = "Outstanding")]
        public int PdsOtqty { get; set; } = 0;

        [Display(Name = "Ordered")]
        public int PdsOrqty { get; set; }

        [Display(Name = "Ordered")]
        public int PdsRcqty { get; set; } = 0;

        [Display(Name = "Post Flag")]
        public bool PdsPflag { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PdsUsrid { get; set; } = string.Empty;

        public DateTime PdsCdate { get; set; }
        public DateTime? PdsUdate { get; set; }
    }

}
