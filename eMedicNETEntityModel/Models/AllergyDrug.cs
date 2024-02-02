using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
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
        public Allergy Allergy { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required"), Display(Name = "Medical Item ID")]
        public int AlmStkid { get; set; }

        [ForeignKey("AlmStkid")]
        public Product Drug { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string AlmUsrid { get; set; } = null!;

        public DateTime AlmCdate { get; set; }
        public DateTime AlmUdate { get; set; }
    }

}
