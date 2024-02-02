using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class AllergyDrug
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int AlmAutid { get; set; }

        [Display(Name = "Allergy ID")]
        public int AlmAllid { get; set; }

        [ForeignKey("AlmAllid")]
        public Allergy? Allergy { get; set; }

        [Display(Name = "Medical Item ID")]
        public int AlmStkid { get; set; }

        [ForeignKey("AlmStkid")]
        public Product? Drug { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string AlmUsrid { get; set; } = string.Empty;

        public DateTime AlmCdate { get; set; }
        public DateTime? AlmUdate { get; set; }
    }

}
