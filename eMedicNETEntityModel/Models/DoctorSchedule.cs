using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class DoctorSchedule
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int DshAutid { get; set; }

        [Display(Name = "Doctor")]
        [Required(ErrorMessage = "{0} is required")]
        public int DshStfid { get; set; }

        [ForeignKey("DshStfid")]
        public Staff Doctor { get; set; } = null!;

        [Display(Name = "Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime DshWdate { get; set; }

        [Display(Name = "Time In")]
        [Required(ErrorMessage = "{0} is required")]
        public TimeSpan DshTmein { get; set; }

        [Display(Name = "Time Out")]
        [Required(ErrorMessage = "{0} is required")]
        public TimeSpan DshTmeot { get; set; }

        [StringLength(150)]
        public string DshUsrid { get; set; } = null!;

        public DateTime DshCdate { get; set; }

        public DateTime DshUdate { get; set; }

    }

}
