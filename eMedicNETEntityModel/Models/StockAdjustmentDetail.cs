using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockAdjustmentDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SadAutid { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int SaAdjid { get; set; }

        [ForeignKey("SadAdjid")]
        public StockAdjustment StockAdjustment { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required")]
        public int SadStkid { get; set; }

        [ForeignKey("SadStkid")]
        public Product Product { get; set; } = null!;

        [Display(Name = "Quantity"), Required(ErrorMessage = "{0} is required")]
        public int SadAdqty { get; set; }

        [Display(Name = "Packing"), Required(ErrorMessage = "{0} is required")]
        public int SadIpack { get; set; }

        [Display(Name = "Exp. Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime SadExpdt { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string SadUsrid { get; set; } = null!;

        public DateTime SadCdate { get; set; }
        public DateTime SadUdate { get; set; }
    }

}
