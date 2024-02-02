using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class MedicalExamDetail
    {
        [Display(Name = "Visit ID"), Required(ErrorMessage = "{0} is required")]
        public int MidVstid { get; set; }

        [Display(Name = "Param ID"), Required(ErrorMessage = "{0} is required")]
        public int MidPrmid { get; set; }

        [ForeignKey("MidPrmid")]
        public Parameter Paramerters { get; set; } = null!;

        [Display(Name = "Result"), Required(ErrorMessage = "{0} is required"), StringLength(100)]
        public string MidRsult { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string MidUsrid { get; set; } = null!;

        public DateTime MidCdate { get; set; }
        public DateTime MidUdate { get; set; }
    }

}
