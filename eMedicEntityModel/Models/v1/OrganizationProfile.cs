using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class OrganizationProfile
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ComAutid { get; set; }

        [Display(Name = "Name"), StringLength(150)]
        public string ComSname { get; set; } = string.Empty;

        [Display(Name = "Regn No"), StringLength(20)]
        public string? ComRegno { get; set; }

        [Display(Name = "Address"), StringLength(1000)]
        public string ComAddre { get; set; } = string.Empty;

        [Display(Name = "Telephone"), StringLength(20)]
        public string? ComTelno { get; set; } = string.Empty;

        [Display(Name = "Fax"), StringLength(20)]
        public string? ComFaxno { get; set; } = string.Empty;

        [Display(Name = "Email"), StringLength(20)]
        public string? ComEmail { get; set; } = string.Empty;

        [Display(Name = "Website"), StringLength(20)]
        public string? ComWebst { get; set; } = string.Empty;

        [Display(Name = "User ID"), StringLength(150)]
        public string ComUsrid { get; set; } = string.Empty;

        public DateTime ComCdate { get; set; }
        public DateTime? ComUdate { get; set; }
    }

}
