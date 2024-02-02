using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class VisitParameterDetail
    {
        [Key, Column(Order = 0), Display(Name = "Visit ID is required")]
        public int VprVstid { get; set; }

        [ForeignKey("VprVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Key, Column(Order = 1), Display(Name = "Parameter ID")]
        public int VprPrmid { get; set; }

        [ForeignKey("VprPrmid")]
        public Parameter? Parameters { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string VprUsrid { get; set; } = string.Empty;

        public DateTime VprCdate { get; set; }
        public DateTime? VprUdate { get; set; }
    }

}
