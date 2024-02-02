using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class ReceiptDetail
    {
        [Key, Column(Order = 0), Required(ErrorMessage = "{0} is required")]
        public int RcdRecid { get; set; }

        [ForeignKey("RcdRecid")]
        public Receipt Receipt { get; set; } = null!;

        [Display(Name = "Mode of Pay"), Required(ErrorMessage = "{0} is required")]
        public int RcdPmode { get; set; }

        [ForeignKey("RcdPmode")]
        public Parameter ModeOfPay { get; set; } = null!;

        [Display(Name = "Paid"), Required(ErrorMessage = "{0} is required")]
        public int RcdPdamt { get; set; }

        [Display(Name = "Any Details"), Required(ErrorMessage = "{0} is required"), StringLength(100)]
        public string RcdPdtls { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string RcdUsrid { get; set; } = null!;

        public DateTime RcdCdate { get; set; }
        public DateTime RcdUdate { get; set; }
    }

}
