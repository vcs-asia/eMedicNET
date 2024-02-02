using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Room
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int RomAutid { get; set; }

        [StringLength(10)]
        [Display(Name = "Room No")]
        [Required]
        public string RomStrno { get; set; } = null!;

        [StringLength(255)]
        [Display(Name = "Remarks")]
        public string RomRmrks { get; set; } = null!;

        [Display(Name = "Fee [Per Hour]")]
        [Required]
        public decimal RomHrfee { get; set; }

        [Display(Name = "Fee [Per Day]")]
        [Required]
        public decimal RomDyfee { get; set; }

        [Display(Name = "Fee [Per Month]")]
        [Required]
        public decimal RomMnfee { get; set; }

        [StringLength(10)]
        [Display(Name = "Status")]
        [Required]
        public string RomState { get; set; } = null!;

        [StringLength(200)]
        public string RomUsrid { get; set; } = null!;

        public DateTime RomCdate { get; set; }

        public DateTime RomUdate { get; set; }

    }
}
