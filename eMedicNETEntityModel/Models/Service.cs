using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Service
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SerAutid { get; set; }

        [Display(Name = "Service Name"), Required(ErrorMessage = "{0} is required"), StringLength(100)]
        public string SerSname { get; set; } = null!;

        [Display(Name = "Charges"), Required(ErrorMessage = "{0} is required")]
        public decimal SerCharg { get; set; }

        [Display(Name = "Service Type"), Required(ErrorMessage = "{0} is required")]
        public int SerStype { get; set; }

        [ForeignKey("SerStype")]
        public Parameter ServiceType { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string SerUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool SerState { get; set; }

        public DateTime SerCdate { get; set; }
        public DateTime SerUdate { get; set; }
    }

}
