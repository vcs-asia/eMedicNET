using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class StockIssueDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SidAutid { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int SidRefid { get; set; }

        [ForeignKey("SidRefid")]
        public StockIssue StockIssue { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required")]
        public int SidStkid { get; set; }

        [ForeignKey("SidStkid")]
        public Product Drug { get; set; } = null!;

        [Display(Name = "Quantity"), Required(ErrorMessage = "{0} is required")]
        public int SidIsqty { get; set; }

        [Display(Name = "Packing"), Required(ErrorMessage = "{0} is required")]
        public int SidIpack { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string SidUsrid { get; set; } = null!;

        public DateTime SidCdate { get; set; }
        public DateTime SidUdate { get; set; }
    }

}
