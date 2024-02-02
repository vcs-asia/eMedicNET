using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Outlet
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int OulAutid { get; set; }

        [StringLength(100), Display(Name = "Outlet Name")]
        public string OulSname { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string OulUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool OulState { get; set; }

        public DateTime OulCdate { get; set; }
        public DateTime OulUdate { get; set; }
    }

}
