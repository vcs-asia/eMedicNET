using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PatientNextKin
    {
        [Display(Name = "Patient ID"), Required(ErrorMessage = "{0} is required")]
        public int PknPatid { get; set; }

        [ForeignKey("PknPatid")]
        public Patient Patient { get; set; } = null!;

        [Display(Name = "Next Kin Name"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string PknSname { get; set; } = null!;

        [Display(Name = "IC No."), StringLength(15), Required(ErrorMessage = "{0} is required")]
        public string PknSicno { get; set; } = null!;

        [Display(Name = "Relation"), Required(ErrorMessage = "{0} is required")]
        public int PknReltn { get; set; }

        [ForeignKey("PknReltn")]
        public Parameter KinRelation { get; set; } = null!;

        [Display(Name = "Occupation"), StringLength(25), Required(ErrorMessage = "{0} is required")]
        public string PknOccup { get; set; } = null!;

        [Display(Name = "Telephone"), StringLength(15), Required(ErrorMessage = "{0} is required")]
        public string PknTelno { get; set; } = null!;

        [Display(Name = "Address"), StringLength(500), Required(ErrorMessage = "{0} is required")]
        public string PknAddrs { get; set; } = null!;

        [Display(Name = "User ID"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string PknUsrid { get; set; } = null!;

        public DateTime PknCdate { get; set; }
        public DateTime PknUdate { get; set; }
    }

}
