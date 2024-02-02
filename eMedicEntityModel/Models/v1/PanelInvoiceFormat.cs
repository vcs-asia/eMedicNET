using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class PanelInvoiceFormat
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PifAutid { get; set; }

        [Display(Name = "Description"), StringLength(255)]
        public string PifDescr { get; set; } = string.Empty;

        [Display(Name = "File"), StringLength(255)]
        public string PifFpath { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string PifUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool PifState { get; set; }

        public DateTime PifCdate { get; set; }
        public DateTime? PifUdate { get; set; }
    }

}
