using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockDetailedInfo
    {
        [Display(Name = "ID"), Required(ErrorMessage = "{0} is required")]
        public int SdiMitid { get; set; }

        [Display(Name = "Batch ID"), Required(ErrorMessage = "{0} is required")]
        public int SdiBatid { get; set; }

        [Display(Name = "Expiry Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime SdiDtexp { get; set; }

        [Display(Name = "Unit Cost"), Required(ErrorMessage = "{0} is required")]
        public decimal SdiUcost { get; set; }

        [Display(Name = "Received"), Required(ErrorMessage = "{0} is required")]
        public int SdiRcqty { get; set; }

        [Display(Name = "Balance"), Required(ErrorMessage = "{0} is required")]
        public int SdiBlqty { get; set; }

        [Display(Name = "Current Balance"), Required(ErrorMessage = "{0} is required")]
        public int SdiCblqt { get; set; }

        [Display(Name = "Any Details"), Required(ErrorMessage = "{0} is required")]
        public byte SdiEflag { get; set; }

        [Display(Name = "Batch No"), Required(ErrorMessage = "{0} is required"), StringLength(20)]
        public string SdiBatno { get; set; } = null!;

        [Display(Name = "Supplier ID"), Required(ErrorMessage = "{0} is required")]
        public int SdiSupid { get; set; }

        [ForeignKey("SdiSupid")]
        public Supplier Supplier { get; set; } = null!;

        [Display(Name = "Grn ID"), Required(ErrorMessage = "{0} is required")]
        public int SdiGrnid { get; set; }

        [ForeignKey("SdiGrnid")]
        public StockGoodsReceiveNote StockGoodsReceiveNote { get; set; } = null!;

        [Display(Name = "PO ID"), Required(ErrorMessage = "{0} is required")]
        public int SdiIpoid { get; set; }

        [ForeignKey("SdiIpoid")]
        public StockPurchaseOrder DrugPurchaseOrder { get; set; } = null!;

        [Display(Name = "Packing"), Required(ErrorMessage = "{0} is required")]
        public int SdiIpack { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string SdiUsrid { get; set; } = null!;

        public DateTime SdiCdate { get; set; }
        public DateTime SdiUdate { get; set; }
    }

}
