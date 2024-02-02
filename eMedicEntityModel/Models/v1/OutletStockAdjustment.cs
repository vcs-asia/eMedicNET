using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class OutletStockAdjustment
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int OsaAutid { get; set; }

        [Display(Name = "Reference No"), StringLength(20)]
        public string OsaRefno { get; set; } = string.Empty;

        [Display(Name = "Date")]
        public DateTime OsaTdate { get; set; }

        [Display(Name = "Outlet ID")]
        public int OsaOutid { get; set; }

        [ForeignKey("OsaOutid")]
        public Outlet? Outlet { get; set; }

        [Display(Name = "Remarks"), StringLength(255)]
        public string? OsaRmarks { get; set; }

        [Display(Name = "Status")]
        public int OsaStats { get; set; } = 0;

        [Display(Name = "User ID"), StringLength(150)]
        public string OsaUsrid { get; set; } = string.Empty;

        public DateTime OsaCdate { get; set; }
        public DateTime? OsaUdate { get; set; }
    }

}
