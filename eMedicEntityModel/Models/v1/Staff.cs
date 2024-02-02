using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eMedicEntityModel.Models.v1
{
    public class Staff
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int StfAutid { get; set; }

        [Display(Name = "Name"), StringLength(150)]
        public string StfSname { get; set; } = string.Empty;

        [Display(Name = "IC/Passport"), StringLength(150)]
        public string StfIcppt { get; set; } = string.Empty;

        [Display(Name = "Specialization")]
        public int StfSpeid { get; set; }

        [ForeignKey("StfSpeid")]
        public Parameter? Specialization { get; set; }

        [Display(Name = "Sex")]
        public string StfStsex { get; set; } = string.Empty;

        [Display(Name = "Hand Phone"), StringLength(100)]
        public string StfTelhp { get; set; } = string.Empty;

        [Display(Name = "Email"), StringLength(150)]
        public string StfEmail { get; set; } = string.Empty;

        [Display(Name = "Qualification"), StringLength(150)]
        public string StfQuali { get; set; } = string.Empty;

        [Display(Name = "Remarks"), StringLength(1000)]
        public string StfRmrks { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int StfCatid { get; set; }

        [ForeignKey("StfCatid")]
        public Parameter? StaffCategory { get; set; }

        [Display(Name = "Photo"), StringLength(255)]
        public string StfPhoto { get; set; } = string.Empty;

        [Display(Name = "Signature")]
        public string StfSpath { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? StfSfile { get; set; }

        [NotMapped]
        public IFormFile? StfPfile { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string StfUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool StfState { get; set; }

        public DateTime StfCdate { get; set; }
        public DateTime? StfUdate { get; set; }
    }

}
