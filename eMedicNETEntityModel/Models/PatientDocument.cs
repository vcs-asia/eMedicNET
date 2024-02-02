using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class PatientDocument
    {
        [Key, Column(Order = 0), Display(Name = "File ID"), StringLength(128), Required(ErrorMessage = "{0} is required")]
        public int PtdFilid { get; set; }

        [Display(Name = "Patient ID"), Required(ErrorMessage = "{0} is required")]
        public int PtdPatid { get; set; }

        [ForeignKey("PtdPatid")]
        public Patient Patient { get; set; } = null!;

        [Display(Name = "File Path"), StringLength(1000), Required(ErrorMessage = "{0} is required")]
        public string PtdFpath { get; set; } = null!;

        [Display(Name = "File Type"), StringLength(20), Required(ErrorMessage = "{0} is required")]
        public string PtdFtype { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PtdUsrid { get; set; } = null!;

        public DateTime PtdCdate { get; set; }
        public DateTime PtdUdate { get; set; }
    }

}
