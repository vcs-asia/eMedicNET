using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockPurchaseOrderDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PdsAutid { get; set; }

        [Display(Name = "PO Id"), Required(ErrorMessage = "{0} is required")]
        public int PdsIpoid { get; set; }

        [ForeignKey("PdsIpoid")]
        public StockPurchaseOrder StockPurchaseOrder { get; set; } = null!;

        [Display(Name = "Stock ID"), Required(ErrorMessage = "{0} is required")]
        public int PdsStkid { get; set; }

        [Display(Name = "Ordered"), Required(ErrorMessage = "{0} is required")]
        public int PdsOrdqt { get; set; }

        [Display(Name = "Cost"), Required(ErrorMessage = "{0} is required")]
        public decimal PdsScost { get; set; }

        [Display(Name = "Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal PdsAmont { get; set; }

        [Display(Name = "Packing"), Required(ErrorMessage = "{0} is required"), StringLength(10)]
        public int PdsIpack { get; set; }

        [Display(Name = "Description"), Required(ErrorMessage = "{0} is required")]
        public string PdsSdesc { get; set; } = null!;

        [Display(Name = "Cancelled"), Required(ErrorMessage = "{0} is required")]
        public int PdsClqty { get; set; }

        [Display(Name = "Outstanding"), Required(ErrorMessage = "{0} is required")]
        public int PdsOtqty { get; set; }

        [Display(Name = "Ordered"), Required(ErrorMessage = "{0} is required")]
        public int PdsRcqty { get; set; }

        [Display(Name = "Post Flag"), Required(ErrorMessage = "{0} is required")]
        public bool PdsPflag { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PdsUsrid { get; set; } = null!;

        public DateTime PdsCdate { get; set; }
        public DateTime PdsUdate { get; set; }
    }

}
