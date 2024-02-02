using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PatientGuarantor
    {
        [Display(Name = "Patient ID")]
        public int PgnPatid { get; set; }

        [ForeignKey("PgnPatid")]
        public Patient? Patient { get; set; }

        [Display(Name = "Next Kin Name"), StringLength(150)]
        public string PgnSname { get; set; } = string.Empty;

        [Display(Name = "IC No."), StringLength(15)]
        public string? PgnSicno { get; set; }

        [Display(Name = "Relation")]
        public int PgnReltn { get; set; }

        [ForeignKey("PgnReltn")]
        public Parameter? KinRelation { get; set; }

        [Display(Name = "Occupation"), StringLength(50)]
        public string? PgnOccup { get; set; }

        [Display(Name = "Telephone"), StringLength(25)]
        public string? PgnTelno { get; set; }

        [Display(Name = "Address"), StringLength(500)]
        public string? PgnAddrs { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string Pgnusrid { get; set; } = string.Empty;

        public DateTime PgnCdate { get; set; }
        public DateTime? PgnUdate { get; set; }
    }

}
