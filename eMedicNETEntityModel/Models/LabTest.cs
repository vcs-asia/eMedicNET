using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class LabTest
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int LtsAutid { get; set; }

        [Display(Name = "Name"), Required(ErrorMessage = "Description is required"), StringLength(150)]
        public string LtsSdesc { get; set; } = null!;

        [Display(Name = "Min Value")]
        public decimal LtsMnval { get; set; }

        [Display(Name = "Max Value")]
        public decimal LtsMxval { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string LtsUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool LtsState { get; set; }

        public DateTime LtsCdate { get; set; }
        public DateTime LtsUdate { get; set; }
    }

}
