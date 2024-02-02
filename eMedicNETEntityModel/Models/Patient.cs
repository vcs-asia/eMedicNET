using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eMedicNETEntityModel.Models
{
    public class Patient
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PrnAutid { get; set; }

        [Display(Name = "Patient Name"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string PrnPname { get; set; } = null!;

        [Display(Name = "IC/Passport No."), StringLength(12), Required(ErrorMessage = "{0} is required")]
        public string PrnIcpno { get; set; } = null!;

        [Display(Name = "Folder No."), StringLength(10), Required(ErrorMessage = "{0} is required")]
        public string PrnFoldr { get; set; } = null!;

        [Display(Name = "Regn No."), StringLength(10), Required(ErrorMessage = "{0} is required")]
        public string PrnRegno { get; set; } = null!;

        [Display(Name = "Regn. Date"), Required(ErrorMessage = "{0} is required")]
        public DateTime PrnRegdt { get; set; }

        [Display(Name = "Date of Birth"), Required(ErrorMessage = "{0} is required")]
        public DateTime PrnDtdob { get; set; }

        [Display(Name = "Sex"), Required(ErrorMessage = "{0} is required")]
        public int PrnGendr { get; set; }

        [ForeignKey("PrnGendr")]
        public Parameter Gender { get; set; } = null!;

        [Display(Name = "Marital Status")]
        [Required(ErrorMessage = "{0} is required")]
        public int PrnMrgst { get; set; }

        [ForeignKey("PrnMrgst")]
        public Parameter MaritalStatus { get; set; } = null!;

        [Display(Name = "Patient Type")]
        [Required(ErrorMessage = "{0} is required")]
        public int PrnPtype { get; set; }

        [ForeignKey("PrnPtype")]
        public Parameter PatientType { get; set; } = null!;

        [Display(Name = "Father/Husband"), StringLength(3), Required(ErrorMessage = "{0} is required")]
        public string PrnFhnme { get; set; } = null!;

        [Display(Name = "Address 1"), StringLength(200), Required(ErrorMessage = "{0} is required")]
        public string PrnAddr1 { get; set; } = null!;

        [Display(Name = "Address 2"), StringLength(200), Required(ErrorMessage = "{0} is required")]
        public string PrnAddr2 { get; set; } = null!;

        [Display(Name = "Address 3"), StringLength(200), Required(ErrorMessage = "{0} is required")]
        public string PrnAddr3 { get; set; } = null!;

        [Display(Name = "Email"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string PrnEmail { get; set; } = null!;

        [Display(Name = "House Telephone"), StringLength(15), Required(ErrorMessage = "{0} is required")]
        public string PrnTeln1 { get; set; } = null!;

        [Display(Name = "Office Telephone"), StringLength(15)]
        public string PrnTeln2 { get; set; } = null!;

        [Display(Name = "Handphone"), StringLength(15), Required(ErrorMessage = "{0} is required")]
        public string PrnTelhp { get; set; } = null!;

        [Display(Name = "Occupation"), StringLength(25), Required(ErrorMessage = "{0} is required")]
        public string PrnOccup { get; set; } = null!;

        [Display(Name = "Race"), Required(ErrorMessage = "{0} is required")]
        public int PrnIrace { get; set; }

        [ForeignKey("PrnIrace")]
        public Parameter Race { get; set; } = null!;

        [Display(Name = "Nationality"), Required(ErrorMessage = "{0} is required")]
        public int PrnNtion { get; set; }

        [ForeignKey("PrnNtion")]
        public Parameter Nationality { get; set; } = null!;

        [Display(Name = "Blood Group"), Required(ErrorMessage = "{0} is required")]
        public int PrnBgrop { get; set; }

        [ForeignKey("PrnBgrop")]
        public Parameter BloodGroup { get; set; } = null!;

        [Display(Name = "Previous History"), StringLength(1000), Required(ErrorMessage = "{0} is required")]
        public string PrnPhist { get; set; } = null!;

        [Display(Name = "Panel")]
        public int PrnPanel { get; set; }

        [ForeignKey("PrnPanel")]
        public Panel Panel { get; set; } = null!;

        [Display(Name = "Status"), Required(ErrorMessage = "{0} is required")]
        public int PrnPstas { get; set; }

        [ForeignKey("PrnPstas")]
        public Parameter PanelStatus { get; set; } = null!;

        [Display(Name = "Employee No."), StringLength(10), Required(ErrorMessage = "{0} is required")]
        public string PrnEmpno { get; set; } = null!;

        [Display(Name = "Relationship"), Required(ErrorMessage = "{0} is required")]
        public int PrnRelat { get; set; }

        [ForeignKey("PrnRelat")]
        public Parameter Relationship { get; set; } = null!;

        [Display(Name = "Related To"), StringLength(150), Required(ErrorMessage = "{0} is required")]
        public string PrnRelto { get; set; } = null!;

        [Display(Name = "Panel Outlet"), StringLength(25)]
        public string PrnPnlol { get; set; } = null!;

        [Display(Name = "Cost Centre"), StringLength(25)]
        public string PrnCstcn { get; set; } = null!;

        [Display(Name = "Department"), StringLength(25)]
        public string PrnDeprt { get; set; } = null!;

        [Display(Name = "Panel Other Details"), StringLength(255)]
        public string PrnPodtl { get; set; } = null!;

        [Display(Name = "Remarks"), StringLength(1000)]
        public string PrnRmrks { get; set; } = null!;

        [Display(Name = "Allergies"), StringLength(1000)]
        public string PrnAlrgy { get; set; } = null!;

        [Display(Name = "Photo"), StringLength(255)]
        public string PrnPhoto { get; set; } = null!;

        [NotMapped]
        public IFormFile PrnPfile { get; set; } = null!;

        [Display(Name = "Referred By")]
        [StringLength(250)]
        public string? PrnRefby { get; set; }

        [Display(Name = "Clinic Name")]
        [StringLength(1000)]
        public string? PrnRefcl { get; set; }

        [Display(Name = "Treatment Plan")]
        public string? PrnTrpln { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PrnUsrid { get; set; } = null!;

        public DateTime PrnCdate { get; set; }
        public DateTime PrnUdate { get; set; }
    }

}
