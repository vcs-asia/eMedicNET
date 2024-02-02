using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eMedicEntityModel.Models.v1
{
    public class Patient
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PrnAutid { get; set; }

        [Display(Name = "Patient Name"), StringLength(150)]
        public string PrnPname { get; set; } = string.Empty;

        [Display(Name = "IC/Passport No."), StringLength(12)]
        public string PrnIcpno { get; set; } = string.Empty;

        [Display(Name = "Folder No."), StringLength(20)]
        public string PrnFoldr { get; set; } = string.Empty;

        [Display(Name = "Regn No."), StringLength(20)]
        public string PrnRegno { get; set; } = string.Empty;

        [Display(Name = "Regn. Date")]
        public DateTime PrnRegdt { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime PrnDtdob { get; set; }

        [Display(Name = "Sex")]
        public int PrnGendr { get; set; }

        [ForeignKey("PrnGendr")]
        public Parameter? Gender { get; set; }

        [Display(Name = "Marital Status")]
        public int PrnMrgst { get; set; }

        [ForeignKey("PrnMrgst")]
        public Parameter? MaritalStatus { get; set; }

        [Display(Name = "Patient Type")]
        public int PrnPtype { get; set; }

        [ForeignKey("PrnPtype")]
        public Parameter? PatientType { get; set; }

        [Display(Name = "Father/Husband"), StringLength(255)]
        public string? PrnFhnme { get; set; }

        [Display(Name = "Address 1"), StringLength(200)]
        public string? PrnAddr1 { get; set; }

        [Display(Name = "Address 2"), StringLength(200)]
        public string? PrnAddr2 { get; set; }

        [Display(Name = "Address 3"), StringLength(200)]
        public string? PrnAddr3 { get; set; }

        [Display(Name = "Email"), StringLength(150)]
        public string? PrnEmail { get; set; }

        [Display(Name = "House Telephone"), StringLength(15)]
        public string? PrnTeln1 { get; set; }

        [Display(Name = "Office Telephone"), StringLength(15)]
        public string? PrnTeln2 { get; set; }

        [Display(Name = "Handphone"), StringLength(15)]
        public string PrnTelhp { get; set; } = string.Empty;

        [Display(Name = "Occupation"), StringLength(25)]
        public string? PrnOccup { get; set; }

        [Display(Name = "Race")]
        public int PrnIrace { get; set; }

        [ForeignKey("PrnIrace")]
        public Parameter? Race { get; set; }

        [Display(Name = "Nationality")]
        public int PrnNtion { get; set; }

        [ForeignKey("PrnNtion")]
        public Parameter? Nationality { get; set; }

        [Display(Name = "Blood Group")]
        public int PrnBgrop { get; set; }

        [ForeignKey("PrnBgrop")]
        public Parameter? BloodGroup { get; set; }

        [Display(Name = "Previous History"), StringLength(1000)]
        public string? PrnPhist { get; set; }

        [Display(Name = "Panel")]
        public int PrnPanel { get; set; }

        [ForeignKey("PrnPanel")]
        public Panel? Panel { get; set; }

        [Display(Name = "Status")]
        public string? PrnPstas { get; set; }

        [Display(Name = "Employee No."), StringLength(10)]
        public string? PrnEmpno { get; set; }

        [Display(Name = "Relationship")]
        public int PrnRelat { get; set; }

        [ForeignKey("PrnRelat")]
        public Parameter? Relationship { get; set; }

        [Display(Name = "Related To"), StringLength(255)]
        public string? PrnRelto { get; set; }

        [Display(Name = "Panel Outlet"), StringLength(25)]
        public string? PrnPnlol { get; set; }

        [Display(Name = "Cost Centre"), StringLength(25)]
        public string? PrnCstcn { get; set; }

        [Display(Name = "Department"), StringLength(25)]
        public string? PrnDeprt { get; set; }

        [Display(Name = "Panel Other Details"), StringLength(255)]
        public string? PrnPodtl { get; set; }

        [Display(Name = "Remarks"), StringLength(1000)]
        public string? PrnRmrks { get; set; }

        [Display(Name = "Allergies"), StringLength(1000)]
        public string? PrnAlrgy { get; set; }

        [Display(Name = "Photo"), StringLength(255)]
        public string? PrnPhoto { get; set; }

        [NotMapped]
        public IFormFile? PrnPfile { get; set; }

        [Display(Name = "Referred By")]
        [StringLength(250)]
        public string? PrnRefby { get; set; }

        [Display(Name = "Clinic Name")]
        [StringLength(1000)]
        public string? PrnRefcl { get; set; }

        [Display(Name = "Treatment Plan")]
        public string? PrnTrpln { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string PrnUsrid { get; set; } = string.Empty;

        public DateTime PrnCdate { get; set; }
        public DateTime? PrnUdate { get; set; }
    }

}
