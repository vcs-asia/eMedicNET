using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PackageService
    {
        [Key, Column(Order = 0), Required(ErrorMessage = "{0} is required")]
        public int PsePakid { get; set; }

        [ForeignKey("PsePakid")]
        public MedicalPackage MedicalPackage { get; set; } = null!;

        [Key, Column(Order = 1), Display(Name = "Service ID"), Required(ErrorMessage = "{0} is required")]
        public int PseSerid { get; set; }

        [ForeignKey("PseSerid")]
        public Service Service { get; set; } = null!;

        [Display(Name = "Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal PseAmont { get; set; }

        [Display(Name = "Discount(%) Up to")]
        public decimal PseDiscp { get; set; }

        [Display(Name = "User ID"), StringLength(150), Required(ErrorMessage = "{0} is requierd")]
        public string PseUsrid { get; set; } = null!;

        public DateTime PseCdate { get; set; }
        public DateTime PseUdate { get; set; }
    }

}
