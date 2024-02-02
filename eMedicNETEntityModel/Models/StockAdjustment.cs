using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockAdjustment
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int StaAutid { get; set; }

        [Display(Name = "Ref No"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string StaAdjno { get; set; } = null!;

        [Display(Name = "Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime StaAdate { get; set; }

        [Display(Name = "Post Status"), Required(ErrorMessage = "{0} is required")]
        public byte StaPflag { get; set; }

        [Display(Name = "Remarks"), StringLength(255)]
        public string StaRmrks { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string StaUsrid { get; set; } = null!;

        public DateTime StaCdate { get; set; }
        public DateTime StaUdate { get; set; }
    }

}
