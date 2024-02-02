using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockTransferDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int StdAutid { get; set; }

        [Display(Name = "ID"), Required(ErrorMessage = "{0} is required")]
        public int StdTrnid { get; set; }

        [ForeignKey("StdTrnid")]
        public StockTransfer StockTransfer { get; set; } = null!;

        [Display(Name = "Stock ID"), Required(ErrorMessage = "{0} is required")]
        public int StdStkid { get; set; }

        [ForeignKey("StdStkid")]
        public Product Product { get; set; } = null!;

        [Display(Name = "Quantity")]
        public int StdTrqty { get; set; }

        [Display(Name = "Packing")]
        public int StdSpack { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string StdUsrid { get; set; } = null!;

        public DateTime StdCdate { get; set; }
        public DateTime StdUdate { get; set; }
    }

}
