using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class VisitParameterDetail
    {
        [Key, Column(Order = 0), Required(ErrorMessage = "{0} is required"), Display(Name = "Visit ID is required")]
        public int VprVstid { get; set; }

        [ForeignKey("VprVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Key, Column(Order = 1), Display(Name = "Parameter ID"), Required(ErrorMessage = "{0} ID is required")]
        public int VprPrmid { get; set; }

        [ForeignKey("VprPrmid")]
        public Parameter Parameters { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string VprUsrid { get; set; } = null!;

        public DateTime VprCdate { get; set; }
        public DateTime VprUdate { get; set; }
    }

}
