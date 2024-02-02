using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockIssue
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SisAutid { get; set; }

        [Display(Name = "Any Details"), StringLength(10)]
        public string SisRefno { get; set; } = string.Empty;

        [Display(Name = "Date")]
        public DateTime SisIssdt { get; set; }

        [Display(Name = "Outlet ID")]
        public int SisOutid { get; set; }

        [ForeignKey("SisOutid")]
        public Outlet? Outlet { get; set; }

        [Display(Name = "Amount")]
        public decimal SisAmont { get; set; }

        [Display(Name = "Remarks (If any)"), StringLength(255)]
        public string? SisRmrks { get; set; }

        [Display(Name = "Post Flag")]
        public bool SisPflag { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string SisUsrid { get; set; } = string.Empty;

        public DateTime SisCdate { get; set; }
        public DateTime? SisUdate { get; set; }
    }

}
