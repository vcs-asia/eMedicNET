using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class ProductPrice
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PprAutid { get; set; }

        [Display(Name = "Product ID")]
        [Required]
        public int PprItmid { get; set; }

        [ForeignKey("PprItmid")]
        public Product Product { get; set; } = null!;

        [Display(Name = "Normal Price")]
        [Required]
        public decimal PprNpric { get; set; }

        [Display(Name = "Staff Price")]
        [Required]
        public decimal PprSpric { get; set; }

        [Display(Name = "Package Price")]
        [Required]
        public decimal PprPpric { get; set; }

        [Display(Name = "User ID")]
        [StringLength(150)]
        [Required]
        public string PprUsrid { get; set; } = null!;

        public DateTime PprCdate { get; set; }
        public DateTime PprUdate { get; set; }
    }

}
