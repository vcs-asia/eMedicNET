using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class MedicalPackage
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int MpcAutid { get; set; }

        [Display(Name = "Description"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string MpcDescr { get; set; } = null!;

        [Display(Name = "Amount"), Required(ErrorMessage = "{0} is required")]
        public decimal MpcAmont { get; set; }

        [Display(Name = "Discount(%) Up to")]
        public decimal MpcDiscp { get; set; }

        [Display(Name = "User ID"), StringLength(150), Required(ErrorMessage = "{0} is requierd")]
        public string MpcUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool MpcState { get; set; }

        public DateTime MpcCdate { get; set; }
        public DateTime MpcUdate { get; set; }
    }

}
