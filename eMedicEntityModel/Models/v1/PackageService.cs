using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PackageService
    {
        [Key, Column(Order = 0)]
        public int PsePakid { get; set; }

        [ForeignKey("PsePakid")]
        public MedicalPackage? MedicalPackage { get; set; }

        [Key, Column(Order = 1), Display(Name = "Service ID")]
        public int PseSerid { get; set; }

        [ForeignKey("PseSerid")]
        public Service? Service { get; set; }

        [Display(Name = "Amount")]
        public decimal PseAmont { get; set; }

        [Display(Name = "Discount(%) Up to")]
        public decimal PseDiscp { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PseUsrid { get; set; } = string.Empty;

        public DateTime PseCdate { get; set; }
        public DateTime? PseUdate { get; set; }
    }

}
