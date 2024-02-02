using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PatientAllergy
    {
        [Display(Name = "Patient ID"), Required(ErrorMessage = "{0} is required")]
        public int PalPatid { get; set; }

        [ForeignKey("PalPatid")]
        public Patient Patient { get; set; } = null!;

        [Display(Name = "Allergy ID"), Required(ErrorMessage = "{0} is required")]
        public int PalAlrid { get; set; }

        [ForeignKey("PalAlrid")]
        public Allergy Allergy { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PalUsrid { get; set; } = null!;

        public DateTime PalCdate { get; set; }
        public DateTime PalUdate { get; set; }
    }
}
