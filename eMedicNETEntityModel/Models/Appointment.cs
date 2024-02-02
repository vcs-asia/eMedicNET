using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Appointment
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int AptAutid { get; set; }

        [Display(Name = "Doctor")]
        [Required(ErrorMessage = "{0} is required")]
        public int AptStfid { get; set; }

        [ForeignKey("AptStfid")]
        public Staff Doctor { get; set; } = null!;

        [Display(Name = "Patient")]
        [Required(ErrorMessage = "{0} is required")]
        public int AptPatid { get; set; }

        [ForeignKey("AptPatid")]
        public Patient Patient { get; set; } = null!;

        [Display(Name = "Appointment Date & Time")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime AptDattm { get; set; }

        [StringLength(150)]
        public string AptUsrid { get; set; } = null!;

        public DateTime AptCdate { get; set; }

        public DateTime AptUdate { get; set; }

    }

}
