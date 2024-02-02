using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class VisitLabDetail
    {
        [Column(Order = 0)]
        [Display(Name = "Visit ID")]
        [Required(ErrorMessage = "{0} is required")]
        public int VlbVstid { get; set; }

        [ForeignKey("VlbVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Column(Order = 1)]
        [Display(Name = "ID")]
        [StringLength(128)]
        [Required(ErrorMessage = "{0} is required")]
        public int VlbTstid { get; set; }

        [ForeignKey("VlbFndid")]
        public LabTest LabTest { get; set; } = null!;

        [Display(Name = "Result")]
        public decimal VlbReslt { get; set; }

        [StringLength(150)]
        public string VlbUsrid { get; set; } = null!;

        public DateTime VlbCdate { get; set; }

        public DateTime VlbUdate { get; set; }
    }

}
