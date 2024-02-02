using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockGoodsReceiveNote
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int GrsAutid { get; set; }

        [Display(Name = "Supplier ID"), Required(ErrorMessage = "{0} is required")]
        public int GrsSupid { get; set; }

        [ForeignKey("GrsSupid")]
        public Supplier Supplier { get; set; } = null!;

        [Display(Name = "Ref No")]
        [StringLength(10)]
        public string GrsRefno { get; set; } = null!;

        [Display(Name = "Received Date")]
        public DateTime GrsTdate { get; set; }

        [Display(Name = "Invoice No"), StringLength(20)]
        public string GrsInvno { get; set; } = null!;

        [Display(Name = "Invoice Date")]
        public DateTime GrsInvdt { get; set; }

        [Display(Name = "Amount")]
        public decimal GrsAmont { get; set; }

        [Display(Name = "Remarks"), StringLength(255)]
        public string GrsRmrks { get; set; } = null!;

        [Display(Name = "PO ID"), Required(ErrorMessage = "{0} is required")]
        public int GrsIpoid { get; set; }

        [ForeignKey("GrsIpoid")]
        public StockPurchaseOrder StockPurchaseOrder { get; set; } = null!;

        public byte GrsPflag { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string GrsUsrid { get; set; } = null!;

        public DateTime GrsCdate { get; set; }
        public DateTime GrsUdate { get; set; }
    }

}
