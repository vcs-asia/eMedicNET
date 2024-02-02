using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class VisitInvestigationDetail
    {
        [Display(Name = "Visit ID"), Required(ErrorMessage = "{0} is required")]
        public int VidVstid { get; set; }

        [ForeignKey("VidVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Display(Name = "Investigation ID"), Required(ErrorMessage = "{0} is required")]
        public int VidInvid { get; set; }

        [ForeignKey("VidInvid")]
        public Patient Patient { get; set; } = null!;

        [Display(Name = "Results"), StringLength(50)]
        public string VidReslt { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string VidUsrid { get; set; } = null!;

        public DateTime VidCdate { get; set; }
        public DateTime VidUdate { get; set; }
    }

}
