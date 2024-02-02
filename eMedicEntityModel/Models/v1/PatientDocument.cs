using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PatientDocument
    {
        [Key, Column(Order = 0), Display(Name = "File ID"), StringLength(128)]
        public int PtdFilid { get; set; }

        [Display(Name = "Patient ID")]
        public int PtdPatid { get; set; }

        [ForeignKey("PtdPatid")]
        public Patient? Patient { get; set; }

        [Display(Name = "File Path"), StringLength(1000)]
        public string PtdFpath { get; set; } = string.Empty;

        [Display(Name = "File Type"), StringLength(20)]
        public string PtdFtype { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string PtdUsrid { get; set; } = string.Empty;

        public DateTime PtdCdate { get; set; }
        public DateTime? PtdUdate { get; set; }
    }

}
