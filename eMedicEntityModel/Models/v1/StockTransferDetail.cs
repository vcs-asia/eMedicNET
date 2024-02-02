using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockTransferDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int StdAutid { get; set; }

        [Display(Name = "ID")]
        public int StdTrnid { get; set; }

        [ForeignKey("StdTrnid")]
        public StockTransfer? StockTransfer { get; set; }

        [Display(Name = "Stock ID")]
        public int StdStkid { get; set; }

        [ForeignKey("StdStkid")]
        public Product? Product { get; set; }

        [Display(Name = "Quantity")]
        public int StdTrqty { get; set; }

        [Display(Name = "Packing")]
        public int StdSpack { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string StdUsrid { get; set; } = string.Empty;

        public DateTime StdCdate { get; set; }
        public DateTime? StdUdate { get; set; }
    }

}
