using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockDispensingInfo
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SdiAutid { get; set; }

        public int SdiStkid { get; set; }

        [ForeignKey("SdiStkid")]
        public Product? Product { set; get; }

        [Display(Name = "Description"), StringLength(200)]
        public string SdiSdesc { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string SdiUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool SdiState { get; set; }

        public DateTime SdiCdate { get; set; }
        public DateTime? SdiUdate { get; set; }
    }

}
