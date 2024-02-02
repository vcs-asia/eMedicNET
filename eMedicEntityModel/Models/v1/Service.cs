using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Service
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SerAutid { get; set; }

        [Display(Name = "Service Name"), StringLength(100)]
        public string SerSname { get; set; } = string.Empty;

        [Display(Name = "Charges")]
        public decimal SerCharg { get; set; }

        [Display(Name = "Service Type")]
        public int SerStype { get; set; }

        [ForeignKey("SerStype")]
        public Parameter? ServiceType { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string SerUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool SerState { get; set; }

        public DateTime SerCdate { get; set; }
        public DateTime? SerUdate { get; set; }
    }

}
