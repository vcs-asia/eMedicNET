using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Panel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PnlAutid { get; set; }

        [Display(Name = "Name"), StringLength(150)]
        public string PnlSname { get; set; } = string.Empty;

        [Display(Name = "Regn No"), StringLength(150)]
        public string? PnlRegno { get; set; }

        [Display(Name = "Address"), StringLength(500)]
        public string? PnlAddre { get; set; }

        [Display(Name = "Tel"), StringLength(20)]
        public string? PnlTelno { get; set; }

        [Display(Name = "Fax"), StringLength(20)]
        public string? PnlFaxno { get; set; }

        [Display(Name = "Email"), StringLength(150)]
        public string? PnlEmail { get; set; }

        [Display(Name = "Contact Person"), StringLength(100)]
        public string? PnlCntct { get; set; }

        [Display(Name = "Contact Person H/P"), StringLength(20)]
        public string? PnlCnthp { get; set; }

        [Display(Name = "Account No."), StringLength(20)]
        public string? PnlActno { get; set; }

        [Display(Name = "Format")]
        public int PnlFrmat { get; set; }

        [ForeignKey("PnlFrmat")]
        public PanelInvoiceFormat? PanelInvoiceFormat { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PnlUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool PnlState { get; set; }

        public DateTime PnlCdate { get; set; }
        public DateTime? PnlUdate { get; set; }
    }

}
