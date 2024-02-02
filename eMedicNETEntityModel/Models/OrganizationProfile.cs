using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class OrganizationProfile
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ComAutid { get; set; }

        [Display(Name = "Name"), StringLength(150), Required(ErrorMessage = "Name is required")]
        public string ComSname { get; set; } = null!;

        [Display(Name = "Regn No"), StringLength(20), Required(ErrorMessage = "Regn No is required")]
        public string ComRegno { get; set; } = null!;

        [Display(Name = "Address"), StringLength(1000), Required(ErrorMessage = "Address is required")]
        public string ComAddre { get; set; } = null!;

        [Display(Name = "Telephone"), StringLength(20), Required(ErrorMessage = "Telephone is required")]
        public string ComTelno { get; set; } = null!;

        [Display(Name = "Fax"), StringLength(20), Required(ErrorMessage = "Fax is required")]
        public string ComFaxno { get; set; } = null!;

        [Display(Name = "Email"), StringLength(20), Required(ErrorMessage = "Email is required")]
        public string ComEmail { get; set; } = null!;

        [Display(Name = "Website"), StringLength(20), Required(ErrorMessage = "Website is required")]
        public string ComWebst { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string ComUsrid { get; set; } = null!;

        public DateTime ComCdate { get; set; }
        public DateTime ComUdate { get; set; }
    }

}
