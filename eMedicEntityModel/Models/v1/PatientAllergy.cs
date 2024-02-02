using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PatientAllergy
    {
        [Display(Name = "Patient ID")]
        public int PalPatid { get; set; }

        [ForeignKey("PalPatid")]
        public Patient? Patient { get; set; }

        [Display(Name = "Allergy ID")]
        public int PalAlrid { get; set; }

        [ForeignKey("PalAlrid")]
        public Allergy? Allergy { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PalUsrid { get; set; } = string.Empty;

        public DateTime PalCdate { get; set; }
        public DateTime? PalUdate { get; set; }
    }
}
