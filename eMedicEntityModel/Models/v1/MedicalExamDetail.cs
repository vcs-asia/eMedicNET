using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class MedicalExamDetail
    {
        [Display(Name = "Visit ID")]
        public int MidVstid { get; set; }

        [Display(Name = "Param ID")]
        public int MidPrmid { get; set; }

        [ForeignKey("MidPrmid")]
        public Parameter? Paramerters { get; set; }

        [Display(Name = "Result"), StringLength(100)]
        public string MidRsult { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string MidUsrid { get; set; } = string.Empty;

        public DateTime MidCdate { get; set; }
        public DateTime? MidUdate { get; set; }
    }

}
