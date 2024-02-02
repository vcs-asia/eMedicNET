using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StaffLeave
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int LevAutid { get; set; }

        [Display(Name = "Staff ID")]
        public int LevStfid { get; set; }

        [ForeignKey("LevStfid")]
        public Staff? Staff { get; set; }

        [Display(Name = "Reason")]
        [StringLength(500)]
        public string LevReasn { get; set; } = string.Empty;

        [StringLength(128)]
        public int LevCateg { get; set; }

        [ForeignKey("LevCateg")]
        public Parameter? LeaveType { get; set; }

        [Display(Name = "Applied Date")]
        public DateTime LevAdate { get; set; }

        [Display(Name = "From Date")]
        public DateTime LevFdate { get; set; }

        [Display(Name = "No. of Days")]
        public int LevNdays { get; set; }

        [Display(Name = "Status")]
        [StringLength(20)]
        public string LevState { get; set; } = string.Empty;

        [Display(Name = "Remarks (If any)")]
        [StringLength(128)]
        public string LevRmrks { get; set; } = string.Empty;

        [Display(Name = "Approved/Rejected Date")]
        public DateTime LevArdat { get; set; }

        [Display(Name = "Approved/Rejected by")]
        public int LevAprid { get; set; }

        [ForeignKey("LevAprid")]
        public Staff? Superior { get; set; }

        [StringLength(150)]
        public string LevUsrid { get; set; } = string.Empty;

        public DateTime? LevUdate { get; set; }

        public DateTime LevCdate { get; set; }
    }

}
