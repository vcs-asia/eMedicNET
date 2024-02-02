using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Panel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PnlAutid { get; set; }

        [Display(Name = "Name"), Required(ErrorMessage = "Name is required"), StringLength(150)]
        public string PnlSname { get; set; } = null!;

        [Display(Name = "Company No"), Required, StringLength(150)]
        public string PnlRegno { get; set; } = null!;

        [Display(Name = "Address"), StringLength(500)]
        public string PnlAddre { get; set; } = null!;

        [Display(Name = "Tel"), StringLength(20)]
        public string PnlTelno { get; set; } = null!;

        [Display(Name = "Fax"), StringLength(20)]
        public string PnlFaxno { get; set; } = null!;

        [Display(Name = "Email"), StringLength(150)]
        public string PnlEmail { get; set; } = null!;

        [Display(Name = "Contact Person"), StringLength(100)]
        public string PnlCntct { get; set; } = null!;

        [Display(Name = "Contact Person H/P"), StringLength(20)]
        public string PnlCnthp { get; set; } = null!;

        [Display(Name = "Account No."), StringLength(20)]
        public string PnlActno { get; set; } = null!;

        [Display(Name = "Format"), Required(ErrorMessage = "{0} is required")]
        public int PnlFrmat { get; set; }

        [ForeignKey("PnlFrmat")]
        public PanelInvoiceFormat PanelInvoiceFormat { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PnlUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool PnlState { get; set; }

        public DateTime PnlCdate { get; set; }
        public DateTime PnlUdate { get; set; }
    }

}
