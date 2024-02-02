using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockAdjustmentDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SadAutid { get; set; }

        public int SaAdjid { get; set; }

        [ForeignKey("SadAdjid")]
        public StockAdjustment? StockAdjustment { get; set; }

        public int SadStkid { get; set; }

        [ForeignKey("SadStkid")]
        public Product? Product { get; set; }

        [Display(Name = "Quantity")]
        public int SadAdqty { get; set; }

        [Display(Name = "Packing")]
        public int SadIpack { get; set; }

        [Display(Name = "Exp. Date")]
        public DateTime SadExpdt { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string SadUsrid { get; set; } = string.Empty;

        public DateTime SadCdate { get; set; }
        public DateTime? SadUdate { get; set; }
    }

}
