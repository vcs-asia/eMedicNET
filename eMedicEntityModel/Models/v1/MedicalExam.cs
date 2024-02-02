using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class MedicalExam
    {
        [Display(Name = "Visit ID")]
        public int PmeVstid { get; set; }

        [ForeignKey("PmeVstid")]
        public PatientVisit? PatientVisit { get; set; }

        [Display(Name = "Family History"), StringLength(255)]
        public string PmeFhist { get; set; } = string.Empty;

        [Display(Name = "Past History"), StringLength(255)]
        public string PmePhist { get; set; } = string.Empty;

        [Display(Name = "Allergy History"), StringLength(100)]
        public string PmeAhist { get; set; } = string.Empty;

        [Display(Name = "Present Complaints"), StringLength(100)]
        public string PmePcomp { get; set; } = string.Empty;

        [Display(Name = "Height"), StringLength(5)]
        public string PmeHeigh { get; set; } = string.Empty;

        [Display(Name = "Weight"), StringLength(5)]
        public string PmeWeigh { get; set; } = string.Empty;

        [Display(Name = "Right Vision (Corrected)"), StringLength(5)]
        public string PmeRvisc { get; set; } = string.Empty;

        [Display(Name = "Left History (Corrected)"), StringLength(5)]
        public string PmeLvisc { get; set; } = string.Empty;

        [Display(Name = "Right Vision (Uncorrected)"), StringLength(5)]
        public string PmeRvisu { get; set; } = string.Empty;

        [Display(Name = "Left History (Uncorrected)"), StringLength(5)]
        public string PmeLvisu { get; set; } = string.Empty;

        [Display(Name = "Color Vision"), StringLength(10)]
        public string PmeClvis { get; set; } = string.Empty;

        [Display(Name = "Blood Pressure"), StringLength(10)]
        public string PmeBlpre { get; set; } = string.Empty;

        [Display(Name = "Pulse"), StringLength(5)]
        public string PmePulse { get; set; } = string.Empty;

        [Display(Name = "CXR Remarks"), StringLength(255)]
        public string PmeCxrrm { get; set; } = string.Empty;

        [Display(Name = "Alb"), StringLength(10)]
        public string PmeUrnal { get; set; } = string.Empty;

        [Display(Name = "pH"), StringLength(10)]
        public string PmeUrnph { get; set; } = string.Empty;

        [Display(Name = "Sugar"), StringLength(10)]
        public string PmeUrnsr { get; set; } = string.Empty;

        [Display(Name = "SG"), StringLength(10)]
        public string PmeUrnsg { get; set; } = string.Empty;

        [Display(Name = "Micro"), StringLength(10)]
        public string PmeUrnmc { get; set; } = string.Empty;

        [Display(Name = "Morphine/Heroin"), StringLength(10)]
        public string PmeUrnmh { get; set; } = string.Empty;

        [Display(Name = "Others"), StringLength(10)]
        public string PmeUrnot { get; set; } = string.Empty;

        [Display(Name = "Remarks"), StringLength(255)]
        public string PmeRmrks { get; set; } = string.Empty;

        [Display(Name = "Conclusion"), StringLength(255)]
        public string PmeConcl { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string PmeUsrid { get; set; } = string.Empty;

        public DateTime PmeCdate { get; set; }
        public DateTime? PmeUdate { get; set; }
    }

}
