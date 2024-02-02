using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class ProductPrice
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PprAutid { get; set; }

        [Display(Name = "Product ID")]
        
        public int PprItmid { get; set; }

        [ForeignKey("PprItmid")]
        public Product? Product { get; set; }

        [Display(Name = "Normal Price")]
        
        public decimal PprNpric { get; set; }

        [Display(Name = "Staff Price")]
        
        public decimal PprSpric { get; set; }

        [Display(Name = "Package Price")]
        
        public decimal PprPpric { get; set; }

        [Display(Name = "User ID")]
        [StringLength(150)]
        
        public string PprUsrid { get; set; } = string.Empty;

        public DateTime PprCdate { get; set; }
        public DateTime? PprUdate { get; set; }
    }

}
