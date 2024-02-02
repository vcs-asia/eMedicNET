using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class MedicalExam
    {
        [Display(Name = "Visit ID"), Required(ErrorMessage = "{0} is required")]
        public int PmeVstid { get; set; }

        [ForeignKey("PmeVstid")]
        public PatientVisit PatientVisit { get; set; } = null!;

        [Display(Name = "Family History"), Required(ErrorMessage = "{0} is required"), StringLength(255)]
        public string PmeFhist { get; set; } = null!;

        [Display(Name = "Past History"), Required(ErrorMessage = "{0} is required"), StringLength(255)]
        public string PmePhist { get; set; } = null!;

        [Display(Name = "Allergy History"), Required(ErrorMessage = "{0} is required"), StringLength(100)]
        public string PmeAhist { get; set; } = null!;

        [Display(Name = "Present Complaints"), Required(ErrorMessage = "{0} is required"), StringLength(100)]
        public string PmePcomp { get; set; } = null!;

        [Display(Name = "Height"), Required(ErrorMessage = "{0} is required"), StringLength(5)]
        public string PmeHeigh { get; set; } = null!;

        [Display(Name = "Weight"), Required(ErrorMessage = "{0} is required"), StringLength(5)]
        public string PmeWeigh { get; set; } = null!;

        [Display(Name = "Right Vision (Corrected)"), Required(ErrorMessage = "{0} is required"), StringLength(5)]
        public string PmeRvisc { get; set; } = null!;

        [Display(Name = "Left History (Corrected)"), Required(ErrorMessage = "{0} is required"), StringLength(5)]
        public string PmeLvisc { get; set; } = null!;

        [Display(Name = "Right Vision (Uncorrected)"), Required(ErrorMessage = "{0} is required"), StringLength(5)]
        public string PmeRvisu { get; set; } = null!;

        [Display(Name = "Left History (Uncorrected)"), Required(ErrorMessage = "{0} is required"), StringLength(5)]
        public string PmeLvisu { get; set; } = null!;

        [Display(Name = "Color Vision"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeClvis { get; set; } = null!;

        [Display(Name = "Blood Pressure"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeBlpre { get; set; } = null!;

        [Display(Name = "Pulse"), Required(ErrorMessage = "{0} is required"), StringLength(5)]
        public string PmePulse { get; set; } = null!;

        [Display(Name = "CXR Remarks"), Required(ErrorMessage = "{0} is required"), StringLength(255)]
        public string PmeCxrrm { get; set; } = null!;

        [Display(Name = "Alb"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeUrnal { get; set; } = null!;

        [Display(Name = "pH"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeUrnph { get; set; } = null!;

        [Display(Name = "Sugar"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeUrnsr { get; set; } = null!;

        [Display(Name = "SG"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeUrnsg { get; set; } = null!;

        [Display(Name = "Micro"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeUrnmc { get; set; } = null!;

        [Display(Name = "Morphine/Heroin"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeUrnmh { get; set; } = null!;

        [Display(Name = "Others"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PmeUrnot { get; set; } = null!;

        [Display(Name = "Remarks"), Required(ErrorMessage = "{0} is required"), StringLength(255)]
        public string PmeRmrks { get; set; } = null!;

        [Display(Name = "Conclusion"), Required(ErrorMessage = "{0} is required"), StringLength(255)]
        public string PmeConcl { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PmeUsrid { get; set; } = null!;

        public DateTime PmeCdate { get; set; }
        public DateTime PmeUdate { get; set; }
    }

}
