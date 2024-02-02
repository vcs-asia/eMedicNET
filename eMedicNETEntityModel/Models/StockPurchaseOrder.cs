using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockPurchaseOrder
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PosAutid { get; set; }

        [Display(Name = "PO No"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public string PosSpono { get; set; } = null!;

        [Display(Name = "GRNs"), StringLength(100)]
        public string PosSgrns { get; set; } = null!;

        [Display(Name = "Supplier ID"), Required(ErrorMessage = "{0} is required")]
        public int PosSupid { get; set; }

        [ForeignKey("PosSupid")]
        public Supplier Supplier { get; set; } = null!;

        [Display(Name = "Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime PosPdate { get; set; }

        [Display(Name = "Terms"), Required(ErrorMessage = "{0} is required"), StringLength(50)]
        public string PosTerms { get; set; } = null!;

        [Display(Name = "Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal PosAmont { get; set; }

        [Display(Name = "Remarks"), Required(ErrorMessage = "{0} is required"), StringLength(100)]
        public string PosRmrks { get; set; } = null!;

        [Display(Name = "Post Flag"), Required(ErrorMessage = "{0} is required")]
        public bool PosPflag { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PosUsrid { get; set; } = null!;

        public DateTime PosCdate { get; set; }
        public DateTime PosUdate { get; set; }
    }

}
