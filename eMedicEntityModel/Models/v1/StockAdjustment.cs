using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockAdjustment
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int StaAutid { get; set; }

        [Display(Name = "Ref No"), StringLength(10)]
        public string StaAdjno { get; set; } = string.Empty;

        [Display(Name = "Date")]
        public DateTime StaAdate { get; set; }

        [Display(Name = "Post Status")]
        public byte StaPflag { get; set; }

        [Display(Name = "Remarks"), StringLength(255)]
        public string StaRmrks { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string StaUsrid { get; set; } = string.Empty;

        public DateTime StaCdate { get; set; }
        public DateTime? StaUdate { get; set; }
    }

}
