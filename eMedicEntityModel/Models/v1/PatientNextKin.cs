using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PatientNextKin
    {
        [Display(Name = "Patient ID")]
        public int PknPatid { get; set; }

        [ForeignKey("PknPatid")]
        public Patient? Patient { get; set; }

        [Display(Name = "Next Kin Name"), StringLength(150)]
        public string PknSname { get; set; } = string.Empty;

        [Display(Name = "IC No."), StringLength(15)]
        public string? PknSicno { get; set; }

        [Display(Name = "Relation")]
        public int PknReltn { get; set; }

        [ForeignKey("PknReltn")]
        public Parameter? KinRelation { get; set; }

        [Display(Name = "Occupation"), StringLength(50)]
        public string? PknOccup { get; set; }

        [Display(Name = "Telephone"), StringLength(15)]
        public string? PknTelno { get; set; }

        [Display(Name = "Address"), StringLength(500)]
        public string? PknAddrs { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PknUsrid { get; set; } = string.Empty;

        public DateTime PknCdate { get; set; }
        public DateTime? PknUdate { get; set; }
    }

}
