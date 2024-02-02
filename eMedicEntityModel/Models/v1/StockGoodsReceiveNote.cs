using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockGoodsReceiveNote
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int GrsAutid { get; set; }

        [Display(Name = "Supplier ID")]
        public int GrsSupid { get; set; }

        [ForeignKey("GrsSupid")]
        public Supplier? Supplier { get; set; }

        [Display(Name = "Ref No")]
        [StringLength(10)]
        public string GrsRefno { get; set; } = string.Empty;

        [Display(Name = "Received Date")]
        public DateTime GrsTdate { get; set; }

        [Display(Name = "Invoice No"), StringLength(20)]
        public string GrsInvno { get; set; } = string.Empty;

        [Display(Name = "Invoice Date")]
        public DateTime GrsInvdt { get; set; }

        [Display(Name = "Amount")]
        public decimal GrsAmont { get; set; }

        [Display(Name = "Remarks"), StringLength(255)]
        public string? GrsRmrks { get; set; }

        [Display(Name = "PO ID")]
        public int GrsIpoid { get; set; }

        [ForeignKey("GrsIpoid")]
        public StockPurchaseOrder? StockPurchaseOrder { get; set; }

        public byte GrsPflag { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string GrsUsrid { get; set; } = string.Empty;

        public DateTime GrsCdate { get; set; }
        public DateTime? GrsUdate { get; set; }
    }

}
