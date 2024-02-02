using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class OutletStockAdjustment
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int OsaAutid { get; set; }

        [Display(Name = "Reference No"), StringLength(20)]
        public string OsaRefno { get; set; } = null!;

        [Display(Name = "Date")]
        public DateTime OsaTdate { get; set; }

        [Display(Name = "Outlet ID"), Required(ErrorMessage = "{0} is required")]
        public int OsaOutid { get; set; }

        [ForeignKey("OsaOutid")]
        public Outlet Outlet { get; set; } = null!;

        [Display(Name = "Remarks"), StringLength(255)]
        public string OsaRmarks { get; set; } = null!;

        [Display(Name = "Status")]
        public int OsaStats { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string OsaUsrid { get; set; } = null!;

        public DateTime OsaCdate { get; set; }
        public DateTime OsaUdate { get; set; }
    }

}
