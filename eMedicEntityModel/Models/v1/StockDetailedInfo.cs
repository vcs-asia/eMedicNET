using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockDetailedInfo
    {
        [Display(Name = "ID")]
        public int SdiMitid { get; set; }

        [Display(Name = "Batch ID")]
        public int SdiBatid { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime SdiDtexp { get; set; }

        [Display(Name = "Unit Cost")]
        public decimal SdiUcost { get; set; }

        [Display(Name = "Received")]
        public int SdiRcqty { get; set; }

        [Display(Name = "Balance")]
        public int SdiBlqty { get; set; }

        [Display(Name = "Current Balance")]
        public int SdiCblqt { get; set; }

        [Display(Name = "Any Details")]
        public byte SdiEflag { get; set; }

        [Display(Name = "Batch No"), StringLength(20)]
        public string SdiBatno { get; set; } = string.Empty;

        [Display(Name = "Supplier ID")]
        public int SdiSupid { get; set; }

        [ForeignKey("SdiSupid")]
        public Supplier? Supplier { get; set; }

        [Display(Name = "Grn ID")]
        public int SdiGrnid { get; set; }

        [ForeignKey("SdiGrnid")]
        public StockGoodsReceiveNote? StockGoodsReceiveNote { get; set; }

        [Display(Name = "PO ID")]
        public int SdiIpoid { get; set; }

        [ForeignKey("SdiIpoid")]
        public StockPurchaseOrder? DrugPurchaseOrder { get; set; }

        [Display(Name = "Packing")]
        public int SdiIpack { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string SdiUsrid { get; set; } = string.Empty;

        public DateTime SdiCdate { get; set; }
        public DateTime? SdiUdate { get; set; }
    }

}
