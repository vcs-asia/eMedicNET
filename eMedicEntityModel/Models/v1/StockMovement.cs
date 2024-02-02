using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockMovement
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SmoAutid { get; set; }

        [Display(Name = "Date")]
        public DateTime SmoMdate { get; set; }

        [Display(Name = "Drug ID")]
        public int SmoStkid { get; set; }

        [ForeignKey("SmoStkid")]
        public Product? Stocktem { get; set; }

        [Display(Name = "Move Type"), StringLength(3)]
        public string SmoMtype { get; set; } = string.Empty;

        [Display(Name = "In")]
        public int SmoQtyin { get; set; }

        [Display(Name = "Out")]
        public int SmoQtyot { get; set; }

        [Display(Name = "Packing")]
        public int SmoIpack { get; set; }

        [Display(Name = "Balance")]
        public int SmoQtybl { get; set; }

        [Display(Name = "Batch ID")]
        public int SmoBatid { get; set; }

        [Display(Name = "Batch No"), StringLength(20)]
        public string SmoBatno { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string SmoUsrid { get; set; } = string.Empty;

        public DateTime SmoCdate { get; set; }
        public DateTime? SmoUdate { get; set; }
    }

}
