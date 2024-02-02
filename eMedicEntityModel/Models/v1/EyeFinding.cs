using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class EyeFinding
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int EfiAutid { get; set; }

        [Display(Name = "Name"), StringLength(150)]
        public string EfiSdesc { get; set; } = string.Empty;

        [Display(Name = "Min Value")]
        public decimal EfiMnval { get; set; }

        [Display(Name = "Max Value")]
        public decimal EfiMxval { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string EfiUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool EfiState { get; set; }

        public DateTime EfiCdate { get; set; }
        public DateTime? EfiUdate { get; set; }
    }

}
