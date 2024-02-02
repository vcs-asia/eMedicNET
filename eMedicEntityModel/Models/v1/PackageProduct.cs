using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PackageProduct
    {
        [Key, Column(Order = 0)]
        public int MpiPakid { get; set; }

        [ForeignKey("MpiPakid")]
        public MedicalPackage? MedicalPackage { get; set; }

        [Key, Column(Order = 1)]
        public int MpiItmid { get; set; }

        [ForeignKey("MpiItmid")]
        public Product? StockItem { get; set; }

        [Display(Name = "Description"), StringLength(150)]
        public string MpiDescr { get; set; } = string.Empty;

        [Display(Name = "Amount")]
        public decimal MpiAmont { get; set; }

        [Display(Name = "Discount(%) Up to")]
        public decimal MpiDiscp { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string MpiUsrid { get; set; } = string.Empty;

        public DateTime MpiCdate { get; set; }
        public DateTime? MpiUdate { get; set; }
    }

}
