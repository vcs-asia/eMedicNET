using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Parameter
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PrmAutid { get; set; }

        [Display(Name = "Description"), StringLength(100)]
        public string PrmPdesc { get; set; } = string.Empty;

        [Display(Name = "Type")]
        [StringLength(100)]
        public string PrmPtype { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool PrmState { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PrmUsrid { get; set; } = string.Empty;

        public DateTime PrmCdate { get; set; }
        public DateTime? PrmUdate { get; set; }
    }

}
