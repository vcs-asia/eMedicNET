using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eMedicNETEntityModel.Models
{
    public class Staff
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int StfAutid { get; set; }

        [Display(Name = "Name"), Required(ErrorMessage = "Name is required"), StringLength(150)]
        public string StfSname { get; set; } = null!;

        [Display(Name = "IC/Passport"), Required, StringLength(150)]
        public string StfIcppt { get; set; } = null!;

        [Display(Name = "Specialization"), Required(ErrorMessage = "{0} is required")]
        public int StfSpeid { get; set; }

        [ForeignKey("StfSpeid")]
        public Parameter Specialization { get; set; } = null!;

        [Display(Name = "Sex")]
        public string StfStsex { get; set; } = null!;

        [Display(Name = "Hand Phone"), Required(ErrorMessage = "Hand Phone is required"), StringLength(100)]
        public string StfTelhp { get; set; } = null!;

        [Display(Name = "Email"), Required(ErrorMessage = "Email is required"), StringLength(150)]
        public string StfEmail { get; set; } = null!;

        [Display(Name = "Qualification"), Required(ErrorMessage = "Qualification is required"), StringLength(150)]
        public string StfQuali { get; set; } = null!;

        [Display(Name = "Remarks"), Required(ErrorMessage = "Qualification is required"), StringLength(1000)]
        public string StfRmrks { get; set; } = null!;

        [Display(Name = "Category"), Required(ErrorMessage = "{0} is required")]
        public int StfCatid { get; set; }

        [ForeignKey("StfCatid")]
        public Parameter StaffCategory { get; set; } = null!;

        [Display(Name = "Photo"), StringLength(255)]
        public string StfPhoto { get; set; } = null!;

        [Display(Name = "Signature")]
        public string StfSpath { get; set; } = null!;

        [NotMapped]
        public IFormFile StfSfile { get; set; } = null!;

        [NotMapped]
        public IFormFile StfPfile { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string StfUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool StfState { get; set; }

        public DateTime StfCdate { get; set; }
        public DateTime StfUdate { get; set; }
    }

}
