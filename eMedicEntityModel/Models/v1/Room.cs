using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Room
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int RomAutid { get; set; }

        [StringLength(10)]
        [Display(Name = "Room No")]
        public string RomStrno { get; set; } = string.Empty;

        [StringLength(255)]
        [Display(Name = "Remarks")]
        public string? RomRmrks { get; set; }

        [Display(Name = "Fee [Per Hour]")]
        public decimal RomHrfee { get; set; }

        [Display(Name = "Fee [Per Day]")]
        public decimal RomDyfee { get; set; }

        [Display(Name = "Fee [Per Month]")]
        public decimal RomMnfee { get; set; }

        [Display(Name = "Discount Upto [%]")]
        public decimal RomDscup { get; set; } = 0;

        [StringLength(10)]
        [Display(Name = "Status")]
        public string RomState { get; set; } = "VACANT";

        [StringLength(200)]
        public string RomUsrid { get; set; } = string.Empty;

        public DateTime RomCdate { get; set; }

        public DateTime? RomUdate { get; set; }

    }
}
