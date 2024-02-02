using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PanelInvoiceDetail
    {
        [Key, Column(Order = 0)]
        public int PidInvid { get; set; }

        [ForeignKey("PidInvid")]
        public PanelInvoice PanelInvoice { get; set; } = null!;

        [Display(Name = "Invoice Visit ID"), Required(ErrorMessage = "{0} is required")]
        public int PidVstid { get; set; }

        [ForeignKey("PidVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PidUsrid { get; set; } = null!;

        public DateTime PidCdate { get; set; }
        public DateTime PidUdate { get; set; }
    }

}
