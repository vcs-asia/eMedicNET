using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PackageProduct
    {
        [Key, Column(Order = 0), Required(ErrorMessage = "{0} is required")]
        public int MpiPakid { get; set; }

        [ForeignKey("MpiPakid")]
        public MedicalPackage MedicalPackage { get; set; } = null!;

        [Key, Column(Order = 1)]
        public int MpiItmid { get; set; }

        [ForeignKey("MpiItmid")]
        public Product StockItem { get; set; } = null!;

        [Display(Name = "Description"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string MpiDescr { get; set; } = null!;

        [Display(Name = "Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal MpiAmont { get; set; }

        [Display(Name = "Discount(%) Up to")]
        public decimal MpiDiscp { get; set; }

        [Display(Name = "User ID"), StringLength(150), Required(ErrorMessage = "{0} is requierd")]
        public string MpiUsrid { get; set; } = null!;

        public DateTime MpiCdate { get; set; }
        public DateTime MpiUdate { get; set; }
    }

}
