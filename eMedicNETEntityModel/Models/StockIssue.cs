using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockIssue
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SisAutid { get; set; }

        [Display(Name = "Any Details"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string SisRefno { get; set; } = null!;

        [Display(Name = "Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime DisIssdt { get; set; }

        [Display(Name = "Outlet ID"), Required(ErrorMessage = "{0} is required")]
        public int SisOutid { get; set; }

        [ForeignKey("SisOutid")]
        public Outlet Outlet { get; set; } = null!;

        [Display(Name = "Amount"), Required(ErrorMessage = "{0} is required")]
        public string SisAmont { get; set; } = null!;

        [Display(Name = "Remarks (If any)"), StringLength(255)]
        public string SisRmrks { get; set; } = null!;

        [Display(Name = "Post Flag"), Required(ErrorMessage = "{0} is required")]
        public bool SisPflag { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string SisUsrid { get; set; } = null!;

        public DateTime SisCdate { get; set; }
        public DateTime SisUdate { get; set; }
    }

}
