using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Outlet
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int OulAutid { get; set; }

        [StringLength(100), Display(Name = "Outlet Name")]
        public string OulSname { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string OulUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool OulState { get; set; }

        public DateTime OulCdate { get; set; }
        public DateTime? OulUdate { get; set; }
    }

}
