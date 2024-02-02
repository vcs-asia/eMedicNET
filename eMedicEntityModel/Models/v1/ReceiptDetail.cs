using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class ReceiptDetail
    {
        [Key, Column(Order = 0)]
        public int RcdRecid { get; set; }

        [ForeignKey("RcdRecid")]
        public Receipt? Receipt { get; set; }

        [Display(Name = "Mode of Pay")]
        public int RcdPmode { get; set; }

        [ForeignKey("RcdPmode")]
        public Parameter? ModeOfPay { get; set; }

        [Display(Name = "Paid")]
        public int RcdPdamt { get; set; }

        [Display(Name = "Any Details"), StringLength(100)]
        public string RcdPdtls { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string RcdUsrid { get; set; } = string.Empty;

        public DateTime RcdCdate { get; set; }
        public DateTime? RcdUdate { get; set; }
    }

}
