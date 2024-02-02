using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Supplier
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SupAutid { get; set; }

        [Display(Name = "Name"), Required(ErrorMessage = "Name is required"), StringLength(150)]
        public string SupSname { get; set; } = null!;

        [Display(Name = "Regn No"), Required, StringLength(20)]
        public string SupRegno { get; set; } = null!;

        [Display(Name = "Address"), StringLength(500)]
        public string SupAddre { get; set; } = null!;

        [Display(Name = "Tel No"), StringLength(20)]
        public string SupTelno { get; set; } = null!;

        [Display(Name = "Fax No"), StringLength(20)]
        public string SupFaxno { get; set; } = null!;

        [Display(Name = "Email"), StringLength(150)]
        public string SupEmail { get; set; } = null!;

        [Display(Name = "Contact Person"), StringLength(100)]
        public string SupConct { get; set; } = null!;

        [Display(Name = "Contact Person H/P"), StringLength(20)]
        public string SupCnthp { get; set; } = null!;

        [Display(Name = "Account No."), StringLength(20)]
        public string SupActno { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string SupUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool SupState { get; set; }

        public DateTime SupCdate { get; set; }
        public DateTime SupUdate { get; set; }
    }

}
