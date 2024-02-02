using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class LabTest
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int LtsAutid { get; set; }

        [Display(Name = "Name"), StringLength(150)]
        public string LtsSdesc { get; set; } = string.Empty;

        [Display(Name = "Min Value")]
        public decimal LtsMnval { get; set; }

        [Display(Name = "Max Value")]
        public decimal LtsMxval { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string LtsUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool LtsState { get; set; }

        public DateTime LtsCdate { get; set; }
        public DateTime? LtsUdate { get; set; }
    }

}
