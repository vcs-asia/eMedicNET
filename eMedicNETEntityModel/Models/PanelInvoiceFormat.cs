using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PanelInvoiceFormat
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PifAutid { get; set; }

        [Display(Name = "Description"), StringLength(255), Required(ErrorMessage = "{0} is required")]
        public string PifDescr { get; set; } = null!;

        [Display(Name = "File"), StringLength(255), Required(ErrorMessage = "{0} is required")]
        public string PifFpath { get; set; } = null!;

        [Display(Name = "User ID"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string PifUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool PifState { get; set; }

        public DateTime PifCdate { get; set; }
        public DateTime PifUdate { get; set; }
    }

}
