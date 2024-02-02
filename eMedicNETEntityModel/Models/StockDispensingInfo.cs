using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockDispensingInfo
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SdiAutid { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int SdiStkid { get; set; }

        [ForeignKey("SdiStkid")]
        public Product Product { set; get; } = null!;

        [Display(Name = "Description"), StringLength(200)]
        public string SdiSdesc { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string SdiUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool SdiState { get; set; }

        public DateTime SdiCdate { get; set; }
        public DateTime SdiUdate { get; set; }
    }

}
