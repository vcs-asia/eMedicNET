using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PatientGuarantor
    {
        [Display(Name = "Patient ID"), Required(ErrorMessage = "{0} is required")]
        public int PgnPatid { get; set; }

        [ForeignKey("PgnPatid")]
        public Patient Patient { get; set; } = null!;

        [Display(Name = "Next Kin Name"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string PgnSname { get; set; } = null!;

        [Display(Name = "IC No."), StringLength(15), Required(ErrorMessage = "{0} is required")]
        public string PgnSicno { get; set; } = null!;

        [Display(Name = "Relation"), Required(ErrorMessage = "{0} is required")]
        public int PgnReltn { get; set; }

        [ForeignKey("PgnReltn")]
        public Parameter KinRelation { get; set; } = null!;

        [Display(Name = "Occupation"), StringLength(25), Required(ErrorMessage = "{0} is required")]
        public string PgnOccup { get; set; } = null!;

        [Display(Name = "Telephone"), StringLength(15), Required(ErrorMessage = "{0} is required")]
        public string PgnTelno { get; set; } = null!;

        [Display(Name = "Address"), StringLength(500), Required(ErrorMessage = "{0} is required")]
        public string PgnAddrs { get; set; } = null!;

        [Display(Name = "User ID"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string Pgnusrid { get; set; } = null!;

        public DateTime PgnCdate { get; set; }
        public DateTime PgnUdate { get; set; }
    }

}
