using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Supplier
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SupAutid { get; set; }

        [Display(Name = "Name"), StringLength(150)]
        public string SupSname { get; set; } = string.Empty;

        [Display(Name = "Regn No"), StringLength(20)]
        public string? SupRegno { get; set; }

        [Display(Name = "Address"), StringLength(500)]
        public string? SupAddre { get; set; }

        [Display(Name = "Tel No"), StringLength(20)]
        public string? SupTelno { get; set; }

        [Display(Name = "Fax No"), StringLength(20)]
        public string? SupFaxno { get; set; }

        [Display(Name = "Email"), StringLength(150)]
        public string? SupEmail { get; set; }

        [Display(Name = "Contact Person"), StringLength(100)]
        public string? SupConct { get; set; }

        [Display(Name = "Contact Person H/P"), StringLength(20)]
        public string? SupCnthp { get; set; }

        [Display(Name = "Account No."), StringLength(20)]
        public string? SupActno { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string SupUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool SupState { get; set; }

        public DateTime SupCdate { get; set; }

        public DateTime? SupUdate { get; set; }
    }

}
