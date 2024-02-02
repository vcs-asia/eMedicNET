using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class MedicalPackage
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int MpcAutid { get; set; }

        [Display(Name = "Description"), StringLength(150)]
        public string MpcDescr { get; set; } = string.Empty;

        [Display(Name = "Amount")]
        public decimal MpcAmont { get; set; } = 0;

        [Display(Name = "Discount(%) Up to")]
        public decimal MpcDiscp { get; set; } = 0;

        [Display(Name = "User ID"), StringLength(150)]
        public string MpcUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool MpcState { get; set; }

        public DateTime MpcCdate { get; set; }
        public DateTime? MpcUdate { get; set; }
    }

}
