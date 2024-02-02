using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class EyeFinding
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int EfiAutid { get; set; }

        [Display(Name = "Name"), Required(ErrorMessage = "Description is required"), StringLength(150)]
        public string EfiSdesc { get; set; } = null!;

        [Display(Name = "Min Value")]
        public decimal EfiMnval { get; set; }

        [Display(Name = "Max Value")]
        public decimal EfiMxval { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string EfiUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool EfiState { get; set; }

        public DateTime EfiCdate { get; set; }
        public DateTime EfiUdate { get; set; }
    }

}
