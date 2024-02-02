using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockIssueDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SidAutid { get; set; }

        public int SidRefid { get; set; }

        [ForeignKey("SidRefid")]
        public StockIssue? StockIssue { get; set; }

        public int SidStkid { get; set; }

        [ForeignKey("SidStkid")]
        public Product? Drug { get; set; }

        [Display(Name = "Quantity")]
        public int SidIsqty { get; set; }

        [Display(Name = "Packing")]
        public int SidIpack { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string SidUsrid { get; set; } = string.Empty;

        public DateTime SidCdate { get; set; }
        public DateTime? SidUdate { get; set; }
    }

}
