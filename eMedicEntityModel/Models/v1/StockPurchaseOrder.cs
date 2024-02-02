using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockPurchaseOrder
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PosAutid { get; set; }

        [Display(Name = "PO No"), StringLength(10)]
        public string PosSpono { get; set; } = string.Empty;

        [Display(Name = "GRNs"), StringLength(100)]
        public string PosSgrns { get; set; } = string.Empty;

        [Display(Name = "Supplier ID")]
        public int PosSupid { get; set; }

        [ForeignKey("PosSupid")]
        public Supplier? Supplier { get; set; }

        [Display(Name = "Date")]
        public DateTime PosPdate { get; set; }

        [Display(Name = "Terms"), StringLength(50)]
        public string? PosTerms { get; set; }

        [Display(Name = "Amount")]
        public decimal PosAmont { get; set; }

        [Display(Name = "Remarks"), StringLength(255)]
        public string? PosRmrks { get; set; } = string.Empty;

        [Display(Name = "Post Flag")]
        public bool PosPflag { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PosUsrid { get; set; } = string.Empty;

        public DateTime PosCdate { get; set; }
        public DateTime? PosUdate { get; set; }
    }

}
