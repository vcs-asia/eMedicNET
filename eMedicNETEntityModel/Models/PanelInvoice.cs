using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PanelInvoice
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PniAutid { get; set; }

        [Display(Name = "Invoice No"), StringLength(12), Required(ErrorMessage = "{0} is required")]
        public string PniInvno { get; set; } = null!;

        [Display(Name = "Panel ID"), Required(ErrorMessage = "{0} is required")]
        public int PniPnlid { get; set; }

        [ForeignKey("PniPnlid")]
        public Panel Panel { get; set; } = null!;

        [Display(Name = "Format")]
        public int PniPnfid { get; set; }

        [ForeignKey("PniPnfid")]
        public PanelInvoiceFormat PanelInvoiceFormat { get; set; } = null!;

        [Display(Name = "Type")]
        public int PniIType { get; set; }

        [Display(Name = "No. of Cases")]
        public int PniNocas { get; set; }

        [Display(Name = "Amount")]
        public decimal PniAmont { get; set; }

        [Display(Name = "Date")]
        public DateTime PniIdate { get; set; }

        [Display(Name = "From Date")]
        public DateTime PniFdate { get; set; }

        [Display(Name = "To Date")]
        public DateTime PniTdate { get; set; }

        [Display(Name = "Print Flag")]
        public bool PniPflag { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PniUsrid { get; set; } = null!;

        public DateTime PnICdate { get; set; }
        public DateTime PnIUdate { get; set; }
    }

}
