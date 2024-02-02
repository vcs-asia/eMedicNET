using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class DoctorSchedule
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int DshAutid { get; set; }

        [Display(Name = "Doctor")]
        public int DshStfid { get; set; }

        [ForeignKey("DshStfid")]
        public Staff? Doctor { get; set; }

        [Display(Name = "Date")]
        public DateTime DshWdate { get; set; }

        [Display(Name = "Time In")]
        public TimeSpan DshTmein { get; set; }

        [Display(Name = "Time Out")]
        public TimeSpan DshTmeot { get; set; }

        [StringLength(150)]
        public string DshUsrid { get; set; } = string.Empty;

        public DateTime DshCdate { get; set; }

        public DateTime? DshUdate { get; set; }

    }

}
