using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PanelInvoiceDetail
    {
        [Key, Column(Order = 0)]
        public int PidInvid { get; set; }

        [ForeignKey("PidInvid")]
        public PanelInvoice? PanelInvoice { get; set; }

        [Display(Name = "Invoice Visit ID")]
        public int PidVstid { get; set; }

        [ForeignKey("PidVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PidUsrid { get; set; } = string.Empty;

        public DateTime PidCdate { get; set; }
        public DateTime? PidUdate { get; set; }
    }

}
