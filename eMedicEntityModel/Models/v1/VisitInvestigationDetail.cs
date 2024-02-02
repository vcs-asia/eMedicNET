using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class VisitInvestigationDetail
    {
        [Display(Name = "Visit ID")]
        public int VidVstid { get; set; }

        [ForeignKey("VidVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "Investigation ID")]
        public int VidInvid { get; set; }

        [ForeignKey("VidInvid")]
        public Investigation? Investigation { get; set; }

        [Display(Name = "Results"), StringLength(50)]
        public string VidReslt { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string VidUsrid { get; set; } = string.Empty;

        public DateTime VidCdate { get; set; }
        public DateTime? VidUdate { get; set; }

    }

}
