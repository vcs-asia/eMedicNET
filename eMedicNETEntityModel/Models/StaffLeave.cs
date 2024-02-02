using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StaffLeave
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int LevAutid { get; set; }

        [Display(Name = "Staff ID")]
        [Required(ErrorMessage = "{0} is required")]
        public int LevStfid { get; set; }

        [ForeignKey("LevStfid")]
        public Staff Staff { get; set; } = null!;

        [Display(Name = "Reason")]
        [StringLength(500)]
        [Required(ErrorMessage = "{0} is required")]
        public string LevReasn { get; set; } = null!;

        [StringLength(128)]
        [Required(ErrorMessage = "{0} is required")]
        public int LevCateg { get; set; }

        [ForeignKey("LevCateg")]
        public Parameter LeaveType { get; set; } = null!;

        [Display(Name = "Applied Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime LevAdate { get; set; }

        [Display(Name = "From Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime LevFdate { get; set; }

        [Display(Name = "No. of Days")]
        [Required(ErrorMessage = "{0} is required")]
        public int LevNdays { get; set; }

        [Display(Name = "Status")]
        [StringLength(20)]
        [Required(ErrorMessage = "{0} is required")]
        public string LevState { get; set; } = null!;

        [Display(Name = "Remarks (If any)")]
        [StringLength(128)]
        [Required(ErrorMessage = "{0} is required")]
        public string LevRmrks { get; set; } = null!;

        [Display(Name = "Approved/Rejected Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime LevArdat { get; set; }

        [Display(Name = "Approved/Rejected by")]
        [Required(ErrorMessage = "{0} is required")]
        public int LevAprid { get; set; }

        [ForeignKey("LevAprid")]
        public Staff Superior { get; set; } = null!;

        [StringLength(150)]
        public string LevUsrid { get; set; } = null!;

        public DateTime LevUdate { get; set; }

        public DateTime LevCdate { get; set; }
    }

}
